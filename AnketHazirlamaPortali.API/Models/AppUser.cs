﻿using Microsoft.AspNetCore.Identity;

namespace AnketHazirlamaPortali.API.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
