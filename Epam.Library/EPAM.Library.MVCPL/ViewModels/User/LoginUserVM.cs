﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.ViewModels.User
{
    public class LoginUserVM
    {
        [Required]
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