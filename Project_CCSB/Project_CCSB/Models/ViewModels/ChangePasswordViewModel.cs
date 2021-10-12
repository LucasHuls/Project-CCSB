using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_CCSB.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [DisplayName("Huidig wachtwoord")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} tekens bevatten", MinimumLength = 6)]
        public string OldPassWord { get; set; }

        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} tekens bevatten", MinimumLength = 6)]
        public string NewPassWord { get; set; }

        [DisplayName("Bevestig wachtwoord")]
        [DataType(DataType.Password)]
        public string NewPassWordConfirm { get; set; }
    }
}
