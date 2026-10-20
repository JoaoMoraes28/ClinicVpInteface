using ClinicVpInterface.Services.Cep;
using ClinicVpInterface.Services.Patient;
using ClinicVpInterface.Services.Uf;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ClinicVpInterface
{
    public partial class PatientForm : System.Web.UI.Page
    {
        readonly private Cep _servicesCep = new Cep();
        readonly private Uf _serviceUf = new Uf();
        readonly private Patient _servicePatient = new Patient();

        public bool[] fieldSetListVisibility = { true, false, false };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        protected void ChangeVisibilityFieldset(object sender, EventArgs e)
        {
            Button panelButton = (Button)sender;

            switch (panelButton.ID)
            {
                case "showCivilState": panelCivilState.Visible = !panelCivilState.Visible; break;
                case "showGenders": panelGenders.Visible = !panelGenders.Visible; break;
                case "showBloodType": panelBloodType.Visible = !panelBloodType.Visible; break;
                default: return;

            }

        }

        protected void ChangeValueIndexCivilState(object sender, EventArgs e)
        {
            var valueOptionSelected = listCivilState.SelectedItem.Value;

            civilState.Text = valueOptionSelected;

            panelCivilState.Visible = false;
        }

        protected void ChangeValueIndexGender(object sender, EventArgs e)
        {
            var valueOptionSelected = listGenders.SelectedItem.Value;

            gender.Text = valueOptionSelected;

            panelGenders.Visible = false;
        }

        protected void ChangeValueIndexBloodType(object sender, EventArgs e)
        {
            var valueOptionSelected = listBloodType.SelectedItem.Value;

            bloodType.Text = valueOptionSelected;

            panelBloodType.Visible = false;
        }

        protected async void GetAddressByCep(object sender, EventArgs e)
        {
            if (cep.Text.Length == 8)
            {
                var address = await _servicesCep.GetAddressByCep(cep.Text);

                city.Text = address.Localidade;
                uf.Text = address.Uf;
                district.Text = address.Bairro;
                street.Text = address.Logradouro;

                return;
            }

            cep.Text = "CEP inválido!";
        }

        protected async void RegisterPatient(object sender, EventArgs e)
        {
            var valideDatas = await ValideFieldsFormPatient();

            if (!valideDatas.status)
            {
                errorMessage.Text = valideDatas.message;
                return;
            }

            var ufs = await _serviceUf.GetUfs();

            int idUf = 0;

            foreach (var item in ufs)
            {
                if (item.Abbreviation.Equals(uf.Text))
                {
                    idUf = item.Id;
                }
            }

            int? heightPatient;

            int.TryParse(height.Text, out int heightConvert);

            if (heightConvert == 0)
            {
                heightPatient = null;
            }
            else
            {
                heightPatient = heightConvert;
            }

            decimal? weightPatient;

            decimal.TryParse(weight.Text, out decimal weightConvert);

            if (weightConvert == 0)
            {
                weightPatient = null;
            }
            else
            {
                weightPatient = weightConvert;
            }

            var patient = new PatientWriteBase(name.Text, cpf.Text, email.Text == "" ? null : email.Text, phone.Text, null, profession.Text == "" ? null : profession.Text, gender.Text == "Masculino" ? "MALE" : "FEMALE",
                GetValueCivilState(civilState.Text), GetValueBloodType(bloodType.Text) == "" ? null : GetValueBloodType(bloodType.Text), weightPatient, heightPatient, bornDate.Text,
                emergencyPhone.Text == "" ? null : emergencyPhone.Text, observations.Text == "" ? null : observations.Text);

            var address = new AddressPatient(idUf, city.Text, district.Text, street.Text, number.Text, cep.Text);

            var newPatient = new PatientWriteDto(patient, address);

            var (response, statusCode) = await _servicePatient.PostPatient(newPatient);

            blackContainer.Visible = true;

            messageResult.Text = $"{statusCode}";

            if (statusCode != HttpStatusCode.OK)
            {
                messageResult.Text = "Ocorreu um erro ao tentar cadastrar este paciente!";
                finish.Text = "Tentar novamente";
            }
            else
            {
                messageResult.Text = "Paciente cadastrado!";
                finish.Text = "Concluir";
            }
        }

        protected void CloseModalResponse(object sender, EventArgs e)
        {
            blackContainer.Visible = false;
        }

        private async Task<(string message, bool status)> ValideFieldsFormPatient()
        {
            string message = "";
            bool status = true;

            if (name.Text == "")
            {
                message = "Nome inválido!";
                status = false;
            }
            else if (cpf.Text == "")
            {
                message = "CPF inválido!";
                status = false;
            }
            else if (civilState.Text == "")
            {
                message = "Estado civil inválido!";
                status = false;
            }
            else if (gender.Text == "")
            {
                message = "Gênero inválido!";
                status = false;
            }
            else if (phone.Text == "")
            {
                message = "Telefone inválido!";
                status = false;
            }
            else if (cep.Text == "" || number.Text == "")
            {
                message = "Endereço inválido!";
                status = false;
            }
            else if (email.Text == "")
            {
                message = "Email inválido!";
                status = false;
            }

            return (message, status);
        }

        private string GetValueCivilState(string state)
        {
            switch (state)
            {
                case "Casado(a)": return "MARRIED";
                case "Solteiro(a)": return "SINGLE";
                case "Divorciado(a)": return "DIVORCIED";
                case "Separado(a)": return "SEPARATED";
                case "Viúvo(a)": return "WIDOWED";
                default: return "";
            }
        }

        private string GetValueBloodType(string type)
        {
            switch (type)
            {
                case "A+": return "A_POSITIVE";
                case "A-": return "A_NEGATIVE";
                case "B+": return "B_POSITIVE";
                case "B-": return "B_NEGATIVE";
                case "O+": return "O_POSITIVE";
                case "O-": return "O_NEGATIVE";
                case "AB+": return "AB_POSITIVE";
                case "AB-": return "AB_NEGATIVE";
                default: return "";
            }
        }
    }
}