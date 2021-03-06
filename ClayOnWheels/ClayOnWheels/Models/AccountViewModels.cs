﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClayOnWheels.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email-adres")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email-adres")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email-adres")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Paswoord")]
        public string Password { get; set; }

        [Display(Name = "Onthoud mij?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "EmailMandatory", ErrorMessageResourceType = typeof(Messages))]
        [EmailAddress]
        [Display(Name = "Email-adres")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PaswoordMandatory", ErrorMessageResourceType = typeof(Messages))]
        [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} karakters bevatten.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Paswoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig paswoord")]
        [Compare("Password", ErrorMessage = "Passwoord en bevestig passwoord moeten identiek zijn.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "StraatMandatory", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Straatnaam en nummer")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "GemeenteMandatory", ErrorMessageResourceType =typeof(Messages))]
        [Display(Name = "Gemeente")]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "PostcodeMandatory", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Postcode")]
        public string PostalCode { get; set; }

        [Required(ErrorMessageResourceName = "VoornaamMandatory", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "AchternaamMandatory", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "TelefoonMandatory", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Telefoon (Gsm)")]
        public string PhoneNumber { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email-adres")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Passwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig passwoord")]
        [Compare("Password", ErrorMessage = "Passwoord en bevestig passwoord moeten identiek zijn.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email-adres")]
        public string Email { get; set; }
    }
}
