using System;

namespace ClinicVpInterface.Components
{
    public partial class PatientCard : System.Web.UI.UserControl
    {

        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // Garante que roda após as propriedades estarem populadas pelo Repeater
            name.InnerText = Name ?? "N/A";
            cpf.InnerText = $"CPF: {Cpf}";
            phone.InnerText = $"Telefone: {Phone}";

            var photoProfile = string.IsNullOrEmpty(Photo) ? "/Photo_patient.png" : Photo;
            photo.Src = photoProfile;
        }
    }
}