﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuthentification.Resources
{
    public class ConstantsEmail
    {
        #region Register

        /// <summary>
        /// envoie d'email quand on créer un compte avec un recap des informations 
        /// </summary>
        public const string RegisterPath = "./wwwroot/EmailTemplates/Register.html";
        /// <summary>
        /// message validation KO (refus) de création de compte
        /// </summary>
        public const string RefusRegister = "./wwwroot/EmailTemplates/RegisterRefus.html";

        /// <summary>
        /// message validation OK création de compte
        /// </summary>
        public const string ValidateRegister = "./wwwroot/EmailTemplates/ValidateRegister.html";

        #endregion

        #region Login

        public static readonly string LoginResetPassword = "Voici le lien pour rénitialiser votre mot de passe </br>" +
                                                           Environment.GetEnvironmentVariable("UrlResetPassword");

        #endregion

        #region Location

        public const string LocationAsk = "./wwwroot/EmailTemplates/AskLocation.html";


        public const string LocationValidation = "./wwwroot/EmailTemplates/LocationValidation.html";
        public const string LocationRefuser = "./wwwroot/EmailTemplates/LocationRefus.html";
        //(ConstantsEmail.LocationAsk, user.UserFirstname, location.LocDatestartlocation, location.LocDateendlocation, poleDepart, poleArrive)
        #endregion

        /// <summary>
        /// envoie d'un email lorsqu'un utilisateur fait une nouvelle réservation
        /// </summary>
        public const string ValidationReservation = "./wwwroot/EmailTemplates/LocationValidation.html";

        /// <summary>
        /// envoie d'un email pour reset le password
        /// </summary>
        //public static readonly string ResetPassword = "Voici le lien pour rénitialiser votre mot de passe " + Environment.GetEnvironmentVariable("UrlResetPassword") + "%%TOKEN%%";
        public static readonly string ResetPassword = "./wwwroot/EmailTemplates/ResetPassword.html";
    }
}
