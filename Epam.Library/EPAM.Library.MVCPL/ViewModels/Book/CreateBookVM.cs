using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.ViewModels.Book
{
    public class CreateBookVM : IValidatableObject
    {
        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        [Required]
        [RegularExpression(@"(^[A-Z]([a-z]|(\-[A-Z])|( [A-Za-z]))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ])|( [А-ЯЁа-яё]))+$)")]
        [StringLength(300)]
        public string PlaceOfPublication { get; set; }

        [Required]
        [StringLength(300)]
        public string Publisher { get; set; }

        [Required]
        public int YearOfPublishing { get; set; }

        [Required]
        [RegularExpression(@"(^ISSN \d{4}-\d{4}$)")]
        public string ISBN { get; set; }

        [Required]
        public string[] AuthorsId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NumberOfPages < 0)
            {
                yield return new ValidationResult("Число страниц должно быть положительным", new[] { nameof(NumberOfPages) });
            }

            //if (CountOfAuthor < 0)
            //{
            //    yield return new ValidationResult("Число авторов должно быть положительным", new[] { nameof(CountOfAuthor) });
            //}

            if (YearOfPublishing > DateTime.Now.Year)
            {
                yield return new ValidationResult("Год издания должен быть меньше текущего года", new[] { nameof(YearOfPublishing) });
            }

            if (YearOfPublishing < 1400)
            {
                yield return new ValidationResult("Год издания должен быть больше 1400 года", new[] { nameof(YearOfPublishing) });
            }
        }
    }
}