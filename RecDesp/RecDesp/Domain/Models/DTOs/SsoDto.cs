using System;

namespace RecDesp.Domain.Models.DTOs
{
    public class SsoDto
    {
        public string Access_token { get; set; }
        public DateTime Expiration { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public SsoDto(string access_token, DateTime expiration, ApplicationUser applicationUser)
        {
            Access_token = access_token;
            Expiration = expiration;
            this.applicationUser = applicationUser;
        }
    }
}
