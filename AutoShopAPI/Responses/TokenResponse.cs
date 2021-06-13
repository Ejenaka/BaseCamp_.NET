using System;

namespace AutoShop.API.Responses
{
    public class TokenResponse
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}