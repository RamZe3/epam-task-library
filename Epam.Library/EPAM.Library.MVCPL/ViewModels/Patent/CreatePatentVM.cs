using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.ViewModels.Patent
{
    public class CreatePatentVM : IValidatableObject
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        [Required]
        [RegularExpression(@"(^[A-Z][a-z]+$)|(^[A-Z]+$)|(^[А-ЯЁ][а-яё]+$)|(^[А-ЯЁ]+$)")]
        [StringLength(200)]
        public string Country { get; set; }

        [Required]
        public int RegistrationNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfApplication { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfPublication { get; set; }

        [Required]
        public string[] AuthorsId { get; set; }

        public List<SelectListItem> AuthorsList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NumberOfPages < 0)
            {
                yield return new ValidationResult("Число страниц должно быть положительным", new[] { nameof(NumberOfPages) });
            }

            if (RegistrationNumber >= 999999999)
            {
                yield return new ValidationResult("Номер должен состоять из 9  цифр", new[] { nameof(RegistrationNumber) });
            }

            if (RegistrationNumber < 0)
            {
                yield return new ValidationResult("Номер должен быть положительным", new[] { nameof(RegistrationNumber) });
            }

            if (DateOfApplication > DateTime.Now)
            {
                yield return new ValidationResult("Дата должна быть меньше текущего года", new[] { nameof(DateOfApplication) });
            }

            if (DateOfPublication > DateTime.Now)
            {
                yield return new ValidationResult("Дата должна быть меньше текущего года", new[] { nameof(DateOfPublication) });
            }

            if (DateOfApplication.Year <= 1474)
            {
                yield return new ValidationResult("Дата должна быть больше 1474 года", new[] { nameof(DateOfApplication) });
            }

            if (DateOfPublication < DateOfApplication)
            {
                yield return new ValidationResult("Дата публикации не может бать раньше даты издания", new[] { nameof(DateOfPublication) });
            }
        }
    }
}