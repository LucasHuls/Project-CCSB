using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models
{
    public static class Helper
    {
        public static readonly string Admin = "Admin";
        public static readonly string User = "User";

        //Appointment messages
        public static string AppointmentAdded = "Afspraak succesvol opgeslagen.";
        public static string AppointmentConfirmed = "Afspraak bevestigd.";
        public static string AppointmentUpdated = "Afspraak succesvol gewijzigd.";
        public static string AppointmentDeleted = "Afspraak succesvol verwijderd.";
        public static string AppointmentExists = "Afspraak bestaat al op gegeven datum en tijdstip.";
        public static string AppointmentNotExists = "Afspraak bestaat niet.";
        public static string AppointmentAddError = "Er ging iets mis. Afspraak niet toegevoegd.";
        public static string AppointmentConfirmError = "Er ging iets mis. Afspraak niet bevestigd.";
        public static string SomethingWentWrong = "Er ging iets mis. Probeer het opnieuw.";
        public static string AppointmentUpdatError = "Er ging iets mis. Afspraak niet gewijzigd.";
        public static int Succes_code = 1;
        public static int Failure_code = 0;

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{ Value = Helper.Admin, Text = "Beheerder"},
                new SelectListItem{ Value = Helper.User, Text = "Gebruiker"}
            };
            return items.OrderBy(s => s.Text).ToList();
        }

        public static List<SelectListItem> GetYesOrNo()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{Value = "true", Text = "Ja"},
                new SelectListItem{Value = "false", Text = "Nee"}
            };
            return items.ToList();
        }

        public static List<SelectListItem> GetBringOrReceive()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{Value = "Brengen", Text = "Brengen"},
                new SelectListItem{Value = "Brengen", Text = "Ophalen"}
            };
            return items.ToList();
        }

        public static List<SelectListItem> GetVehicleType()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{Value = "Camper", Text = "Camper"},
                new SelectListItem{Value = "Caravan", Text = "Caravan"}
            };
            return items.ToList();
        }
    }
}
