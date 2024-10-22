﻿using System;

namespace AgroXchange.WebApi.Models
{
    public class UserView
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class UserAuthResponse
    {
        public UserView User { get; set; }
        public string Token { get; set; }
    }

    public class UserActivation
    {
        public string Email { get; set; }
        public string Key { get; set; }
    }

    public class PasswordReset
    {
        public string Email { get; set; }
        public string Key { get; set; }
        public string Password { get; set; }
    }
}
