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
            name.InnerText = Name;
            cpf.InnerText = $"CPF: {Cpf}";
            phone.InnerText = $"Telefone: {Phone}";

            var photoProfile = Photo == "" || Photo == null ? "/Photo_patient.png" : Photo;

            photo.Src = photoProfile;

        }
    }
}