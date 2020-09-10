using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Web;

namespace PCLoan.Models
{
    public class LoginModel
    {
        [Display(Name="Indtast brugernavn:")]
        public string username { get; set; }
        [Display(Name ="Indtast adgangskode:")]
        public string password { get; set; }
    }
}