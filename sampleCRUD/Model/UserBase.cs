using sampleCRUD.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sampleCRUD.Model
{
    public abstract class UserBase
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        //[EmailUniqueAttribute]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string email { get; set; }
    }
}
