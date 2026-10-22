using ClinicVpInterface.Services.Patient;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.UI;

namespace ClinicVpInterface
{
    public partial class PatientData : Page
    {
        private readonly Patient _servicePatient = new Patient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int patientId))
                {
                    RegisterAsyncTask(new PageAsyncTask(() => LoadPatientDataAsync(patientId)));
                }
                else
                {
                    Response.Redirect("/PatientList.aspx");
                }
            }
        }

        private async Task LoadPatientDataAsync(int id)
        {
            var (response, statusCode) = await _servicePatient.GetPatientId(id);

            if (statusCode == System.Net.HttpStatusCode.OK && response?.Patient != null)
            {
                var p = response.Patient;
                var a = response.Address;

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

                if (DateTime.TryParse(p.BornDate, out DateTime bornDate))
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

                if (a != null)
                {
                    litAddress.Text = $"{a.Street}, {a.Number} - {a.District} - {a.City} - {a.UfAddress}";
                }
                else
                {
                    litAddress.Text = "Endereço não informado";
                }
            }
            else
            {
                Response.Redirect("/PatientList.aspx");
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("/PatientList.aspx");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Response.Redirect($"/PatientForm.aspx?id={Request.QueryString["id"]}");
            }
        }

        protected async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int id))
            {
              
                Response.Redirect("/PatientList.aspx");
            }
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age--;
            return age;
        }

        private string FormatCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11) return cpf ?? "N/A";
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
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