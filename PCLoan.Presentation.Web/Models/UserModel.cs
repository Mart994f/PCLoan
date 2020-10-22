﻿using System.ComponentModel.DataAnnotations;

namespace PCLoan.Presentation.Web.Models
{
    /// <summary>
    /// A model representing a user.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// The users ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The users username.
        /// </summary>
        [Required(ErrorMessage = "Indtast venligst et brugernavn")] 
        [Display(Name = "Brugernavn")]
        public string UserName { get; set; }

        /// <summary>
        /// The users password.
        /// </summary>
        [Required(ErrorMessage = "Indtast venligst en adgangskode")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// If the user is authenticated.
        /// </summary>
        public bool Authenticated { get; set; } = false;

        /// <summary>
        /// The role of the user.
        /// </summary>
        public string Role { get; set; } = null;

        /// <summary>
        /// The JWS for the user once authenticated & authorized
        /// </summary>
        public string Token { get; set; } = null;
    }
}
