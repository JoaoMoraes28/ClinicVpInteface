namespace ClinicVpInterface.Services.Auth
{
    public class AuthDto
    {

        public string Email { get; set; }
        public string Pass { get; set; }

        public AuthDto(string email, string pass)
        {
            Email = email;
            Pass = pass;
        }

    }

    public class ResponseLogin
    {
        public bool Authorized { get; set; }

        public ResponseLogin(bool authorized)
        {
            Authorized = authorized;
        }

    }
}