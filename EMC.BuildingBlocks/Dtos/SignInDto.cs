namespace EMC.BuildingBlocks.Dtos
{
    public class SignInDto
    {
        private bool _succeeded;

        public bool Succeeded
        {
            get => !IsLockedOut && !IsNotAllowed && !RequiresTwoFactor && _succeeded;
            set => _succeeded = value;
        }

        private bool _isLockedOut;
        public bool IsLockedOut
        {
            get => _isLockedOut;
            set
            {
                _isLockedOut = value;
                if (value) _succeeded = false;
            }
        }

        private bool _isNotAllowed;
        public bool IsNotAllowed
        {
            get => _isNotAllowed;
            set
            {
                _isNotAllowed = value;
                if (value) _succeeded = false;
            }
        }

        private bool _requiresTwoFactor;
        public bool RequiresTwoFactor
        {
            get => _requiresTwoFactor;
            set
            {
                _requiresTwoFactor = value;
                if (value) _succeeded = false;
            }
        }

        public string Menssage => Succeeded ? "Éxito" :
            IsLockedOut ? "Ha superado el máximo número de intentos, su cuenta está bloqueada, intente de nuevo en 5 minutos." :
            IsNotAllowed ? "El usuario no ha sido habilitado, debes de seguir las instrucciones del correo enviado para poder habilitar el usuario." :
            RequiresTwoFactor ? "Necesita doble factor de Autenticación" :
            "Usuario o contraseña incorrectos.";
    }
}
