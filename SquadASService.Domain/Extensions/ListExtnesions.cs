using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Domain.Extensions
{
    public static class ListExtnesions
    {
        public static List<string> GetErrorsList(this List<ValidationFailure> validationFailures)
        {
            List<string> errors = [];
            validationFailures.ForEach(a =>
            {
                errors.Add($"{a.PropertyName}: {a.ErrorMessage}");
            });

            return errors;
        }

        public static List<string> GetErrorsList(this List<IdentityError> validationFailures)
        {
            List<string> errors = [];
            validationFailures.ForEach(a =>
            {
                errors.Add($"{a.Code}: {a.Description}");
            });

            return errors;
        }
    }
}
