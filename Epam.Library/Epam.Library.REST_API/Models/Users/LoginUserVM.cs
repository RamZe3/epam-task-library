using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.Library.REST_API.Models
{
    public class LoginUserVM
    {
        [Required]
        [RegularExpression(@"(^[a-zA-Z]+([_]?[a-zA-Z0-9]+)+$)")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (DependenciesResolverConfig.DependenciesResolver.UserRollProvider
        //        .GetUser(Name, Password).id == null)
        //    {
        //        yield return new ValidationResult("Неправильно введен логин или пароль", new[] { nameof(Name), nameof(Password) });
        //    }
        //}
    }
}