﻿using System.ComponentModel.DataAnnotations;

namespace TestAuthentification.ViewModels
{
    public partial class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Pôle")]
        public int PoleId { get; set; }

        [Key()]
        [Required]
        public string PhoneNumber { get; set; }

        public string NumPermis { get; set; }

    }
}
