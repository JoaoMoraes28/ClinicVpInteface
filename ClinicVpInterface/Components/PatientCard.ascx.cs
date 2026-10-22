using System;
using System.Web.UI;

namespace ClinicVpInterface.Components
{
    public partial class PatientCard : UserControl
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }

        // Evento customizado
        public event EventHandler<int> OnPatientSelected;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Atribuição básica caso precise no Load
        }


        protected void BtnSelectPatient_Click(object sender, EventArgs e)
        {
            OnPatientSelected?.Invoke(this, PatientId);
        }
    }
}