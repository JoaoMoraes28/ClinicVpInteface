namespace ClinicVpInterface.Services.Patient
{
    public class PatientWriteDto
    {

        public PatientWriteBase Patient { get; set; }

        public AddressPatient Address { get; set; }

        public PatientWriteDto(PatientWriteBase patient, AddressPatient address)
        {
            Patient = patient;
            Address = address;
        }

    }

    public class PatientWriteBase
    {
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Photo { get; set; }
        public string Professional { get; set; }
        public string Gender { get; set; }
        public string CivilState { get; set; }
        public string BloodType { get; set; }
        public decimal? Weight { get; set; }
        public int? Height { get; set; }
        public string BornDate { get; set; }
        public string PhoneEmergency { get; set; }
        public string Notes { get; set; }

        public PatientWriteBase(string name, string cpf, string email, string phone, string photo, string professional,
            string gender, string civilState, string bloodType, decimal? weight, int? height, string bornDate,
            string phoneEmergency, string notes)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
            Photo = photo;
            Professional = professional;
            Gender = gender;
            CivilState = civilState;
            BloodType = bloodType;
            Weight = weight;
            Height = height;
            BornDate = bornDate;
            PhoneEmergency = phoneEmergency;
            Notes = notes;
        }
    }

    public class AddressPatient
    {
        public int UfId { get; set; }
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;

        public AddressPatient(int ufId, string city, string district, string street, string number, string cep)
        {
            UfId = ufId;
            City = city;
            District = district;
            Street = street;
            Number = number;
            Cep = cep;
        }
    }
}
