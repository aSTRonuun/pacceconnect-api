﻿namespace Application.UserApplication.Dtos
{
    public class LoginUserDto
    {
        public string EmailOrUserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
