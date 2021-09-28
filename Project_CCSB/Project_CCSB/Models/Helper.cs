using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models
{
    public static class Helper
    {
        public static readonly string Admin = "Beheerder";
        public static readonly string User = "Gebruiker";
        
        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{ Value = Helper.Admin, Text = Helper.Admin},
                new SelectListItem{ Value = Helper.User, Text = Helper.User}
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
    }
}
