﻿namespace ArsarExchAPI.Model.DTOS
{

    public class LoginResult
    {
        public bool Successful { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }
    }

}
