using ClinicVpInterface.Components;
using ClinicVpInterface.Services.Patient;
using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicVpInterface
{
    public partial class PatientList : Page
    {
        private readonly Patient _servicePatient = new Patient();

        // Armazena o ID do paciente selecionado no ViewState da página
        private int SelectedPatientId
        {
            get => (int)(ViewState["SelectedPatientId"] ?? 0);
            set => ViewState["SelectedPatientId"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadPatientsAsync));
            }
        }

        private async Task LoadPatientsAsync()
        {
            var (patients, statusCode) = await _servicePatient.GetPatientsPreview();
            rptPatients.DataSource = patients;
            rptPatients.DataBind();
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            modalOverlay.Visible = false;
        }

        // Método chamado ao clicar em "Atualizar dados"
        // Redireciona para o formulário passando o ID na QueryString
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (SelectedPatientId > 0)
            {
                Response.Redirect($"/PatientForm.aspx?id={SelectedPatientId}");
            }
        }

        protected async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (SelectedPatientId > 0)
            {
                // await _servicePatient.DeletePatient(SelectedPatientId);

                // Esconde o modal e recarrega a lista atualizada
                modalOverlay.Visible = false;
                await LoadPatientsAsync();
            }
        }
    }
}