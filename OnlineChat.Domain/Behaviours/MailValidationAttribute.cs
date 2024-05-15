using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Domain.Behaviours
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class MailValidationAttribute : ValidationAttribute
    {
        public MailValidationAttribute()
        {
            ErrorMessage = "Invalid mail address!";
        }
        public override bool IsValid(object? value)
        {
            if (value == null || value is not string email)
            {
                return false;
            }

            const string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            #pragma warning disable SYSLIB1045

            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);

            #pragma warning restore SYSLIB1045
        }
    }
}
