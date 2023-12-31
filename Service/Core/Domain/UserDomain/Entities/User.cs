﻿using Domain.UserDomain.Enuns;
using Domain.UserDomain.Exceptions;
using Domain.UtilsTools;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography;

namespace Domain.UserDomain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }

        public void CreatePasswordHash(string password)
        {
            if (password.Length < 8)
                throw new UserPasswordLengthException();

            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }

        protected void ValidateStateUser()
        {
            if (string.IsNullOrEmpty(UserName) ||
                string.IsNullOrEmpty(Email) ||
                Role.Equals(null))
                throw new UserMissingRequiredInformationException();

            if (!Utils.IsValidEmail(Email))
                throw new UserInvalidEmailException();
        }

        public bool IsValidateUser()
        {
            ValidateStateUser();
            return true;
        }
    }
}
