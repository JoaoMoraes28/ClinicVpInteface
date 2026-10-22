using System;

namespace ClinicVpInterface.Services.Patient
{
    public class PatientResponseBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PhoneEmergency { get; set; }
        public int? Height { get; set; }
        public decimal? Weight { get; set; }
        public string BloodType { get; set; }
        public string Professional { get; set; }
        public string BornDate { get; set; }
        public string CivilState { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Observations { get; set; }
        public string Notes { get; set; }
    }

    public class PatientAddressResponseDto
    {
        public string UfAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;

    }

    public class PatientResponseDto
    {
        public PatientResponseBase Patient { get; set; }
        public PatientAddressResponseDto Address { get; set; }

    }

    public class PatientResponsePreviewDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }

    }
}