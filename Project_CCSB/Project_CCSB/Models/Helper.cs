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

        public static List<SelectListItem> GetAllUsers()
        {
            var items = new List<SelectListItem>
            {

            };
            return items.ToList();
        }

        public static string GetFullName(string fn, string mn, string ln) // First, middle and last name
        {
            return string.IsNullOrEmpty(mn) ?
                fn + " " + ln :
                fn + " " + mn + " " + ln;
        }
    }
}
