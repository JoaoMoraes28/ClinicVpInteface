using ClinicVpInterface.Services.Auth;
using System;
using System.Net;

namespace ClinicVpInterface
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void PostLogin(object sender, EventArgs e)
        {
            Auth _serviceAuth = new Auth();

            if (string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(password.Text))
            {
                resultMessage.Text = "Email ou senha inválidos!";
                return;
            }

            var credentials = new AuthDto(email.Text, password.Text);

            var (login, statusCode) = await _serviceAuth.Login(credentials);

            if (statusCode != HttpStatusCode.OK && statusCode != HttpStatusCode.NotFound)
            {
                resultMessage.Text = "Ocorreu um erro ao tentar realizar o login!";
                return;
            }

            if (!(login.Authorized) || statusCode == HttpStatusCode.NotFound)
            {
                resultMessage.Text = "Email e/ou senha incorretos!";
                return;
            }
            else
            {
                Response.Redirect("/PatientList.aspx");
            }
        }
    }
}