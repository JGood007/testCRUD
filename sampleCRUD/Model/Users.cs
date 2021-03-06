﻿using Microsoft.AspNetCore.Mvc;
using sampleCRUD.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sampleCRUD.Model
{
    public class Users: UserBase
    {
        public int id { get; set; }
        //[Required(ErrorMessage = "Name is required")]
        //public string name { get; set; }        
        //[EmailUniqueAttribute]
        //[EmailAddress]
        //public string email { get; set; }
        public DateTime email_verified_at { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string remember_token { get; set; }
        public Nullable<DateTime> created_at { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public Users()
        {
            this.created_at = DateTime.UtcNow;
            this.updated_at = DateTime.UtcNow;
        }
    }
}
