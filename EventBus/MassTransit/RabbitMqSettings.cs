namespace EMC.BuildingBlocks.EventBus.MassTransit
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }
        public string VHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
