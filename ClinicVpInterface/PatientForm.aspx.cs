using ClinicVpInterface.Services.Cep;
using ClinicVpInterface.Services.Midia;
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
        readonly private Midia _serviceMidia = new Midia();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();

                if (Request.QueryString["id"] != null)
                {
                    submitButton.Text = "Atualizar";
                    int patientId = Convert.ToInt32(Request.QueryString["id"]);
                    RegisterValuesInFields(patientId);

                }
            }
        }


        //Métodos que manipulam dados e interações do HTML
        private void CleanFields()
        {
            name.Text = "";
            cpf.Text = "";
            civilState.Text = "";
            profession.Text = "";
            gender.Text = "";
            email.Text = "";
            phone.Text = "";
            emergencyPhone.Text = "";
            weight.Text = "";
            height.Text = "";
            bornDate.Text = "";
            bloodType.Text = "";

            cep.Text = "";
            city.Text = "";
            uf.Text = "";
            district.Text = "";
            street.Text = "";
            number.Text = "";

            observations.Text = "";
        }

        private async void RegisterValuesInFields(int id)
        {
            var (patient, statusCode) = await GetPatientId(id);

            profilePhoto.Src = string.IsNullOrWhiteSpace(patient.Patient.Photo) ? "/Photo_patient.png" : $"{patient.Patient.Photo}";
            name.Text = $"{patient.Patient.Name}";
            cpf.Text = $"{patient.Patient.Cpf}";
            civilState.Text = $"{SetValueCivilState(patient.Patient.CivilState)}";
            profession.Text = $"{patient.Patient.Professional}";
            gender.Text = $"{(patient.Patient.Gender.ToLower() == "male" ? "Masculino" : "Feminino")}";
            email.Text = $"{patient.Patient.Email}";
            phone.Text = $"{patient.Patient.Phone}";
            emergencyPhone.Text = $"{patient.Patient.PhoneEmergency}";
            weight.Text = $"{patient.Patient.Weight}";
            height.Text = $"{patient.Patient.Height}";
            bornDate.Text = $"{patient.Patient.BornDate.ToString().Split(' ')[0]}";
            bloodType.Text = $"{SetValueBloodType(patient.Patient.BloodType)}";

            cep.Text = $"{patient.Address.Cep}";
            city.Text = $"{patient.Address.City}";
            uf.Text = $"{patient.Address.UfAddress}";
            district.Text = $"{patient.Address.District}";
            street.Text = $"{patient.Address.Street}";
            number.Text = $"{patient.Address.Number}";

            observations.Text = $"{patient.Patient.Notes}";
        }

        protected void ChangeVisibilityFieldset(object sender, EventArgs e)
        {
            Button panelButton = (Button)sender;

            switch (panelButton.ID)
            {
                case "showCivilState":
                    {
                        panelCivilState.Visible = !panelCivilState.Visible;

                        panelGenders.Visible = false;
                        panelBloodType.Visible = false;
                        break;
                    }
                case "showGenders":
                    {
                        panelGenders.Visible = !panelGenders.Visible;

                        panelCivilState.Visible = false;
                        panelBloodType.Visible = false;
                        break;
                    }
                case "showBloodType":
                    {
                        panelBloodType.Visible = !panelBloodType.Visible;

                        panelCivilState.Visible = false;
                        panelGenders.Visible = false;
                        break;
                    }
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


        //Métodos para requisições HTTP
        private async Task<PatientWriteDto> GetPatientWriteModel()
        {
            var valideDatas = await ValideFieldsFormPatient();

            if (!valideDatas.status)
            {
                errorMessage.Text = valideDatas.message;
                return null;
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

            var cpfFormated = cpf.Text.Replace(".", "");
            cpfFormated = cpfFormated.Replace("-", "");
            cpfFormated.Trim();

            var phoneFormated = phone.Text.Replace("(", "");
            phoneFormated = phoneFormated.Replace(")", "");
            phoneFormated = phoneFormated.Replace("-", "");
            phoneFormated = phoneFormated.Replace(" ", "");

            var emergencyPhoneFormated = emergencyPhone.Text.Replace("(", "");
            emergencyPhoneFormated = emergencyPhoneFormated.Replace(")", "");
            emergencyPhoneFormated = emergencyPhoneFormated.Replace("-", "");
            emergencyPhoneFormated = emergencyPhoneFormated.Replace(" ", "");

            var cepFormated = cep.Text.Replace("-", "");

            var guid = Guid.NewGuid();

            string photo = null;

            if (photoFile.HasFile)
            {
                photo = await _serviceMidia.GetUrlImgProfile(photoFile.FileContent, guid.ToString());
            }

            var patient = new PatientWriteBase(name.Text, cpfFormated, email.Text == "" ? null : email.Text, phoneFormated, photo, profession.Text == "" ? null : profession.Text, gender.Text == "Masculino" ? "MALE" : "FEMALE",
                GetValueCivilState(civilState.Text), GetValueBloodType(bloodType.Text) == "" ? null : GetValueBloodType(bloodType.Text), weightPatient, heightPatient, bornDate.Text,
                emergencyPhoneFormated == "" ? null : emergencyPhoneFormated, observations.Text == "" ? null : observations.Text);

            var address = new AddressPatient(idUf, city.Text, district.Text, street.Text, number.Text, cepFormated);

            return new PatientWriteDto(patient, address);
        }

        protected async void ChoiceFunctionButtonSubmit(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                UpdatePatient();
            }
            else
            {
                RegisterPatient();
            }
        }

        private async Task<(PatientResponseDto, HttpStatusCode)> GetPatientId(int id)
        {
            return await _servicePatient.GetPatientId(id);
        }

        private async void RegisterPatient()
        {
            var newPatient = await GetPatientWriteModel();

            if (newPatient == null)
            {
                return;
            }

            var (response, statusCode) = await _servicePatient.PostPatient(newPatient);

            blackContainer.Visible = true;

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

        private async void UpdatePatient()
        {
            var updatePatient = await GetPatientWriteModel();

            if (updatePatient == null)
            {
                return;
            }

            if (updatePatient.Patient.Photo == null && profilePhoto.Src != "/Photo_patient.png")
            {
                updatePatient.Patient.Photo = profilePhoto.Src;
            }

            int patientId = Convert.ToInt32(Request.QueryString["id"]);

            var (response, statusCode) = await _servicePatient.UpdatePatient(updatePatient, patientId);

            blackContainer.Visible = true;

            if (statusCode != HttpStatusCode.OK)
            {
                messageResult.Text = "Ocorreu um erro ao tentar atualizar este paciente!";
                finish.Text = "Tentar novamente";
            }
            else
            {
                messageResult.Text = "Paciente atualizado!";
                finish.Text = "Concluir";
            }
        }

        protected async void GetAddressByCep(object sender, EventArgs e)
        {
            var cepFormated = cep.Text.Replace("-", "");

            if (cepFormated.Length == 8)
            {
                var (address, statusCode) = await _servicesCep.GetAddressByCep(cep.Text);

                city.Text = address.Localidade;
                uf.Text = address.Uf;
                district.Text = address.Bairro;
                street.Text = address.Logradouro;

                if (address.Localidade == null)
                {
                    cep.Text = "";
                    errorMessage.Text = "CEP não encontrado!";
                    return;
                }

                return;
            }
        }


        //Métodos de interação antes/depois de requisições HTTP
        private void RedirectPageForListPatient()
        {
            CleanFields();
            Response.Redirect("PatientList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void CloseModalResponse(object sender, EventArgs e)
        {
            blackContainer.Visible = false;

            if (finish.Text == "Concluir")
            {
                RedirectPageForListPatient();
            }
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


        //Métodos de tradução de dados do BANCO <--> INTEFACE
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

        private string SetValueBloodType(string type)
        {

            if (type == null)
            {
                return "";
            }

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
                default: return "";
            }
        }

        private string SetValueCivilState(string state)
        {
            switch (state.ToLower())
            {
                case "married": return "Casado(a)";
                case "single": return "Solteiro(a)";
                case "divorcied": return "Divorciado(a)";
                case "separated": return "Separado(a)";
                case "widowed": return "Viúvo(a)";
                default: return "";
            }
        }
    }
}