using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.Library.REST_API.Models
{
    public class CreateUserVM : IValidatableObject
    {
        [Required]
        [RegularExpression(@"(^[a-zA-Z]+([_]?[a-zA-Z0-9]+)+$)")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(int.MaxValue ,MinimumLength = 3)]
        [RegularExpression(@"(^[ -�]+$)")]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.ToLower() == Password.ToLower())
            {
                yield return new ValidationResult("Пароль не может быть логином", new[] { nameof(Password) });
            }
        }
    }
}