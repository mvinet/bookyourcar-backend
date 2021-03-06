﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using TestAuthentification.Models;

namespace TestAuthentification.Services
{
    public class AuthService
    {
        private readonly BookYourCarContext _context;
        public readonly CustomIdentityErrorDescriber Describer;
        private static readonly PasswordHasherService<User> ServicePassword = new PasswordHasherService<User>();

        //context bdd
        public AuthService(BookYourCarContext context, CustomIdentityErrorDescriber errors = null)
        {

            _context = context;

            Describer = errors ?? new CustomIdentityErrorDescriber();
        }

        /// <summary>
        /// Recupère l'utilisateur en fonction de son email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User FindByEmail(string email)
        {
            var user = _context.User.SingleOrDefault(x => x.UserEmail == email);
            if (user != null)
            {
                user.UserRight = _context.Right.FirstOrDefault(x => x.RightId == user.UserRightId);
                user.UserPole = _context.Pole.FirstOrDefault(x => x.PoleId == user.UserPoleId);
            }

            return user;

        }

        /// <summary>
        /// Regarde si le password correspond à celui à l'utilisateur
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool CheckPassword(User user, string passwordHashed, string passwordToDecript)
        {
            var result = ServicePassword.VerifyHashedPassword(user, passwordHashed, passwordToDecript);
            return result == PasswordVerificationResult.Success;
        }

        public bool CheckEmail(string email)
        {
            var result = _context.User.FirstOrDefault(x => x.UserEmail == email);
            if (result != null)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IdentityResult VerifUser(User user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (CheckEmailUnique(user.UserEmail))
            {
                errors.Add(Describer.DuplicateEmail(user.UserEmail));
            }
            else if (string.IsNullOrEmpty(user.UserEmail))
            {
                errors.Add(Describer.InvalidEmail(user.UserEmail));
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IdentityResult VerifPhoneNumber(User user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (!string.IsNullOrEmpty(user.UserPhone))
            {
                if (CheckPhoneNumberUnique(user.UserPhone))
                {
                    errors.Add(Describer.InvalidPhoneNumber(user.UserPhone));
                }
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        /// <summary>
        /// Regarde si l'email est bien unique
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        private bool CheckEmailUnique(string userEmail)
        {
            var user = FindByEmail(userEmail);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Regarde si l'email est bien unique
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        private bool CheckPhoneNumberUnique(string userPhone)
        {
            var user = _context.User.FirstOrDefault(x => x.UserPhone == userPhone);

            if (user != null)
            {
                return true;
            }

            return false;
        }


        public IdentityResult AddToRoleAdminAsync(User user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (user.UserRightId != 2 || user.UserRightId != null)
            {
                errors.Add(Describer.UserAlreadyInRole(user.UserRight.RightLabel));
            }
            else
            {
                user.UserRightId = 2;
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        public IdentityResult AddToRoleUserAsync(User user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (user.UserRightId != null)
            {
                errors.Add(Describer.UserAlreadyInRole(user.UserRight.RightLabel));
            }
            else
            {
                user.UserRightId = 1;
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        /// <summary>
        /// recupère l'user connecté en fonction du token renseigné
        /// </summary>
        /// <param name="authToken"></param>
        /// <returns></returns>
        public User GetUserConnected(string authToken)
        {
            User user = new User();
            if (TokenService.ValidateToken(authToken) && TokenService.VerifDateExpiration(authToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var simplePrinciple = handler.ReadJwtToken(authToken);
                var emailUser = simplePrinciple.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                if (!string.IsNullOrEmpty(emailUser))
                {

                    user = FindByEmail(emailUser);
                }
            }
            return user;
        }
    }
}
