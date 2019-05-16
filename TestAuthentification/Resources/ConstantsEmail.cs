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
        public const string RefusRegister = "Bonjour {0}, <br />" +
                                            "Votre demande de création de compte n'a malheureusement pas été accepté par notre Equipe." +
                                            "Cordialement, <br />" +
                                            "L'Équipe A5D - Book Your Car ";
        /// <summary>
        /// message validation OK création de compte
        /// </summary>
        public const string ValidateRegister = "Bonjour {0}, <br />" +
                                               "Votre demande de création de compte vient d'être validé par l'Administrateur. <br />" +
                                               "Vous pouvez maintenant accéder au logiciel Book Your Car avec vos identifiants. <br />" +
                                               "Ceci est un email automatique merci de ne pas y répondre." +
                                               "Cordialement, <br />" +
                                               "L'Équipe A5D - Book Your Car ";

        #endregion

        #region Login

        public static readonly string LoginResetPassword = "Voici le lien pour rénitialiser votre mot de passe </br>" +
                                                           Environment.GetEnvironmentVariable("UrlResetPassword");

        #endregion

        #region Location

        public const string LocationAsk = "./wwwroot/EmailTemplates/AskLocation.html";


        public const string LocationValidation = "Bonjour {0}, <br />" +
                                                 "Votre demande de location vient d'être validé par l'Administrateur. <br />" +
                                                 "Vous pouvez maintenant accéder à votre location sur votre écran d'accueil. <br />" +
                                                 "Cordialement, <br />" +
                                                 "L'Équipe A5D - Book Your Car ";
        public const string LocationRefuser = "Bonjour {0}, <br />" +
                                              "Votre demande de location n'a malheureusement pas été accepté par notre Equipe. <br />" +
                                              "Ceci est un email automatique merci de ne pas y répondre." +
                                              "Cordialement, <br />" +
                                              "L'Équipe A5D - Book Your Car ";
        //(ConstantsEmail.LocationAsk, user.UserFirstname, location.LocDatestartlocation, location.LocDateendlocation, poleDepart, poleArrive)
        #endregion

        /// <summary>
        /// envoie d'un email lorsqu'un utilisateur fait une nouvelle réservation
        /// </summary>
        public const string ValidationReservation = "";

        /// <summary>
        /// envoie d'un email pour reset le password
        /// </summary>
        public static readonly string ResetPassword = "Voici le lien pour rénitialiser votre mot de passe" + Environment.GetEnvironmentVariable("UrlResetPassword");
    }
}
