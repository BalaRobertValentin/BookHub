﻿using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public bool Admin { get; set; }

}