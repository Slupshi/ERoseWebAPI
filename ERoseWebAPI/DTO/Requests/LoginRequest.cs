﻿namespace ERoseWebAPI.DTO.Requests
{
    public class LoginRequest
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
