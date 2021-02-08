using sampleCRUD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sampleCRUD.CustomAttributes
{
    public class EmailUniqueAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult(GetErrorMessage());
            }

            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var entity = context.Users.SingleOrDefault(e => e.email == value.ToString());

            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage(string email)
        {
            return $"Email {email} is already in use.";
        }
        public string GetErrorMessage()
        {
            return $"Email is required!";
        }

    }
}
