using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EMC.BuildingBlocks.Http
{
    /// <summary>
    /// Wrapper genérico para llamadas HTTP entre microservicios EMC.
    ///
    /// Responsabilidades:
    ///   - Maneja el envelope ExternalApiResponse de forma uniforme
    ///   - Propaga X-CompanyId y Authorization en cada request
    ///   - Loguea request y response para trazabilidad
    ///   - Lanza MsHttpException si IsSuccess = false (negocio) o si hay error de red
    ///
    /// Uso en un ExternalService:
    ///   var result = await _msClient.PostAsync<CreateSubsidiaryRequest, CreateSubsidiaryResponse>(
    ///       "api/Admin/CreateSubsidiary", request, companyId, token);
    ///
    /// No necesita DelegatingHandler ni filtros — los headers se agregan por llamada,
    /// lo que da control total y trazabilidad clara.
    /// </summary>
    public class MsHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MsHttpClient> _logger;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public MsHttpClient(IHttpClientFactory httpClientFactory, ILogger<MsHttpClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        // ── POST ─────────────────────────────────────────────────────────────────

        public async Task<TResponse> PostAsync<TRequest, TResponse>(
            string clientName,
            string endpoint,
            TRequest body,
            Guid companyId,
            string? bearerToken = null,
            CancellationToken ct = default)
        {
            var client = BuildClient(clientName, companyId, bearerToken);
            _logger.LogInformation("[MsHttpClient] POST {Client}/{Endpoint} companyId={CompanyId}",
                clientName, endpoint, companyId);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await client.PostAsJsonAsync(endpoint, body, JsonOptions, ct);
            }
            catch (TaskCanceledException) when (!ct.IsCancellationRequested)
            {
                throw new MsHttpException($"Timeout en POST {clientName}/{endpoint}");
            }
            catch (HttpRequestException ex)
            {
                throw new MsHttpException($"Error de red en POST {clientName}/{endpoint}: {ex.Message}");
            }

            return await ParseResponse<TResponse>(httpResponse, endpoint, ct);
        }

        // ── GET ──────────────────────────────────────────────────────────────────

        public async Task<TResponse> GetAsync<TResponse>(
            string clientName,
            string endpoint,
            Guid companyId,
            string? bearerToken = null,
            CancellationToken ct = default)
        {
            var client = BuildClient(clientName, companyId, bearerToken);
            _logger.LogInformation("[MsHttpClient] GET {Client}/{Endpoint} companyId={CompanyId}",
                clientName, endpoint, companyId);

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await client.GetAsync(endpoint, ct);
            }
            catch (TaskCanceledException) when (!ct.IsCancellationRequested)
            {
                throw new MsHttpException($"Timeout en GET {clientName}/{endpoint}");
            }
            catch (HttpRequestException ex)
            {
                throw new MsHttpException($"Error de red en GET {clientName}/{endpoint}: {ex.Message}");
            }

            return await ParseResponse<TResponse>(httpResponse, endpoint, ct);
        }

        // ── Helpers privados ─────────────────────────────────────────────────────

        private HttpClient BuildClient(string clientName, Guid companyId, string? bearerToken)
        {
            var client = _httpClientFactory.CreateClient(clientName);

            // Headers por llamada — trazable, sin magia de filtros
            client.DefaultRequestHeaders.Remove("X-CompanyId");
            client.DefaultRequestHeaders.Add("X-CompanyId", companyId.ToString());

            if (!string.IsNullOrWhiteSpace(bearerToken))
            {
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization",
                    bearerToken.StartsWith("Bearer ") ? bearerToken : $"Bearer {bearerToken}");
            }

            return client;
        }

        private async Task<TResponse> ParseResponse<TResponse>(
            HttpResponseMessage httpResponse,
            string endpoint,
            CancellationToken ct)
        {
            // Error de infraestructura real (red caída, 5xx del servidor, etc.)
            if (!httpResponse.IsSuccessStatusCode)
            {
                var raw = await httpResponse.Content.ReadAsStringAsync(ct);
                _logger.LogError("[MsHttpClient] HTTP {Status} en {Endpoint}: {Body}",
                    (int)httpResponse.StatusCode, endpoint, raw);
                throw new MsHttpException(
                    $"HTTP {(int)httpResponse.StatusCode} desde {endpoint}: {raw}");
            }

            var envelope = await httpResponse.Content
                .ReadFromJsonAsync<ExternalApiResponse<TResponse>>(JsonOptions, ct);

            if (envelope is null)
                throw new MsHttpException($"Respuesta vacía desde {endpoint}");

            // Error de negocio (MS devuelve 200 pero IsSuccess = false)
            if (!envelope.IsSuccess || !envelope.HasResponse)
            {
                var errorMsg = envelope.Errors.FirstOrDefault()?.Message ?? envelope.Message;
                _logger.LogWarning("[MsHttpClient] IsSuccess=false en {Endpoint}: {Error}",
                    endpoint, errorMsg);
                throw new MsHttpException(errorMsg, isBusinessError: true);
            }

            return envelope.Response!;
        }
    }

    /// <summary>
    /// Se lanza cuando un MS externo devuelve IsSuccess = false o hay error de red.
    /// IsBusinessError = true → error de negocio (el MS lo rechazó explícitamente).
    /// IsBusinessError = false → error de infraestructura (red, timeout, etc.).
    /// El controller decide el status code según esto.
    /// </summary>
    public class MsHttpException : Exception
    {
        public bool IsBusinessError { get; }

        public MsHttpException(string message, bool isBusinessError = false)
            : base(message)
        {
            IsBusinessError = isBusinessError;
        }
    }
}
