﻿using System;

namespace AgroXchange.WebApi.Models
{
    public class User
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
        public User User { get; set; }
        public string Token { get; set; }
    }
}