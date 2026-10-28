using ClinicVpInterface.Services.Patient;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicVpInterface
{
    public partial class PatientList : Page
    {
        private readonly Patient _servicePatient = new Patient();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadPatientsAsync();
            }
            else
            {
                await FilterPatients();
            }
        }


        //Métodos que manipulam dados e interações no HTML
        protected async void BtnSearch_Click(object sender, EventArgs e)
        {

            await FilterPatients();
        }

        private async Task FilterPatients()
        {
            string searchTerm = search.Text;
            var (patients, statusCode) = await _servicePatient.GetPatientsPreview();

            if (statusCode == HttpStatusCode.OK && patients != null)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    string cleanSearchTerm = new string(searchTerm.Where(char.IsDigit).ToArray());

                    string searchLower = searchTerm.Trim().ToLower();

                    if (string.IsNullOrWhiteSpace(cleanSearchTerm))
                    {
                        patients = patients.Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();
                    }
                    else
                    {
                        patients = patients.Where(p => p.Cpf.Contains(cleanSearchTerm)).ToList();
                    }
                }
            }

            rptPatients.DataSource = patients;
            rptPatients.DataBind();
        }

        protected void RptPatients_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SelectPatient")
            {
                int patientId = Convert.ToInt32(e.CommandArgument);
                ViewState["SelectedPatientId"] = patientId;

                RegisterAsyncTask(new PageAsyncTask(() => OpenModalPatientAsync(patientId)));
            }
        }

        private async Task OpenModalPatientAsync(int id)
        {
            var (response, statusCode) = await _servicePatient.GetPatientId(id);

            if (statusCode == HttpStatusCode.OK && response?.Patient != null)
            {
                var p = response.Patient;
                var a = response.Address;

                imgPatientPhoto.ImageUrl = string.IsNullOrWhiteSpace(p.Photo) ? "/Photo_patient.png" : p.Photo;
                litName.Text = p.Name ?? "N/A";
                litCpf.Text = FormatCpf(p.Cpf);
                litCivilState.Text = SetValueCivilState(p.CivilState);
                litGender.Text = string.Equals(p.Gender, "MALE", StringComparison.OrdinalIgnoreCase) ? "Masculino" : "Feminino";
                litProfession.Text = !string.IsNullOrWhiteSpace(p.Professional) ? p.Professional : "N/A";
                litEmail.Text = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "N/A";
                litPhone.Text = !string.IsNullOrWhiteSpace(p.Phone) ? p.Phone : "N/A";
                litEmergencyPhone.Text = !string.IsNullOrWhiteSpace(p.PhoneEmergency) ? p.PhoneEmergency : "N/A";

                litWeight.Text = p.Weight.HasValue ? $"{p.Weight.Value} Kg" : "N/A";
                litHeight.Text = p.Height.HasValue ? $"{p.Height.Value} cm" : "N/A";

                if (DateTime.TryParse(p.BornDate.ToString(), out DateTime bornDate) && bornDate != DateTime.MinValue)
                {
                    litBornDate.Text = bornDate.ToString("dd/MM/yyyy");
                    litAge.Text = $"{CalculateAge(bornDate)} anos";
                }
                else
                {
                    litBornDate.Text = "N/A";
                    litAge.Text = "N/A";
                }

                litBloodType.Text = SetValueBloodType(p.BloodType);
                litCreatedAt.Text = p.CreatedAt.HasValue ? p.CreatedAt.Value.ToString("dd/MM/yyyy") : "N/A";

                litAddress.Text = a != null
                    ? $"{a.Street}, {a.Number} - {a.District} - {a.City}/{a.UfAddress}"
                    : "Endereço não informado";

                litObservations.Text = !string.IsNullOrWhiteSpace(p.Observations) ? p.Observations : "Sem observações cadastradas.";

                modalOverlay.Visible = true;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            modalOverlay.Visible = false;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            if (ViewState["SelectedPatientId"] != null)
            {
                int patientId = Convert.ToInt32(ViewState["SelectedPatientId"]);

                Response.Redirect($"~/PatientForm.aspx?id={patientId}", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertViewState", "alert('ID do paciente não encontrado na sessão da página. Por favor, selecione o paciente novamente.');", true);
            }
        }

        protected async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedPatientId"] != null && int.TryParse(ViewState["SelectedPatientId"].ToString(), out int patientId))
            {
                HttpStatusCode statusCode = await _servicePatient.DeletePatient(patientId);

                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NoContent)
                {
                    modalOverlay.Visible = false;
                    ViewState["SelectedPatientId"] = null;

                    string script = "alert('Paciente inativado com sucesso!'); window.location.reload();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "successReload", script, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Erro ao excluir paciente.');", true);
                }
            }
        }


        //Métodos para requisições HTTP
        private async Task LoadPatientsAsync()
        {
            var (patients, statusCode) = await _servicePatient.GetPatientsPreview();

            rptPatients.DataSource = patients;
            rptPatients.DataBind();
        }


        //Métodos de formatação dos dados e tradução de dados BANCO <--> INTEFACE
        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear) age--;
            return age;
        }

        protected string FormatCpf(object value)
        {
            string cpf = value.ToString();
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11) return cpf ?? "N/A";
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        protected string FormatPhone(object value)
        {
            string phone = value.ToString();
            if (string.IsNullOrWhiteSpace(phone) || phone.Length != 11) return phone ?? "N/A";
            return Convert.ToUInt64(phone).ToString(@"(00) 00000-0000");
        }

        private string SetValueCivilState(string state)
        {
            if (string.IsNullOrWhiteSpace(state)) return "N/A";
            switch (state.ToLower())
            {
                case "married": return "Casado(a)";
                case "single": return "Solteiro(a)";
                case "divorcied": return "Divorciado(a)";
                case "separated": return "Separado(a)";
                case "widowed": return "Viúvo(a)";
                default: return state;
            }
        }

        private string SetValueBloodType(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) return "N/A";
            switch (type.ToLower())
            {
                case "a_positive": return "A+";
                case "a_negative": return "A-";
                case "b_positive": return "B+";
                case "b_negative": return "B-";
                case "o_positive": return "O+";
                case "o_negative": return "O-";
                case "ab_positive": return "AB+";
                case "ab_negative": return "AB-";
                default: return type;
            }
        }
    }
}