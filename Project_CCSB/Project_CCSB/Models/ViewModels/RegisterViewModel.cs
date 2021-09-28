using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models
{
    public class RegisterViewModel
    {
        [DisplayName("Voornaam")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string FirstName { get; set; }

        [DisplayName("Tussenvoegsels")]
        public string MiddleName { get; set; }

        [DisplayName("Achternaam")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Email { get; set; }

        [DisplayName("Rekeningnummer")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string AccountNumber { get; set; }

        [DisplayName("Woonplaats")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string City { get; set; }

        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Adress { get; set; }

        [DisplayName("Postcode")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string ZipCode { get; set; }

        [DisplayName("Geboortedatum")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:dd/MM/yyy}")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} tekens bevatten", MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("Bevestig wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen")]
        public string PasswordConfirm { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string RoleName { get; set; }
    }
}
