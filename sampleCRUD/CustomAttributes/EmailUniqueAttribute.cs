using Microsoft.AspNetCore.Http;
using sampleCRUD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sampleCRUD.CustomAttributes
{
    public class EmailUniqueAttribute : ValidationAttribute
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

            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            var user = httpContextAccessor.HttpContext.User;

            if (user.Claims.Count() == 0)
            {

                if (entity != null)
                {
                    return new ValidationResult(GetErrorMessage(value.ToString()));
                }

            }
            else
            {
                var emailclaim = user.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email).Value;


                if (entity != null)
                {
                    if (emailclaim == value.ToString())
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {

                        return new ValidationResult(GetErrorMessage(value.ToString()));
                    }
                }
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
