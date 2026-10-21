using ClinicVpInterface.Services.Patient;
using System;

namespace ClinicVpInterface
{
    public partial class PatientList : System.Web.UI.Page
    {

        readonly private Patient _servicePatient = new Patient();

        protected async void Page_Load(object sender, EventArgs e)
        {

            var (patients, statusCode) = await _servicePatient.GetPatientsPreview();

            rptPatients.DataSource = patients;
            rptPatients.DataBind();

        }


    }
}