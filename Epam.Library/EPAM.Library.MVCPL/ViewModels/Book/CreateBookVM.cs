using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.Library.MVCPL.ViewModels.Book
{
    public class CreateBookVM : IValidatableObject
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
        [RegularExpression(@"(^[A-Z]([a-z]|(\-[A-Z])|( [A-Za-z]))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ])|( [А-ЯЁа-яё]))+$)")]
        [StringLength(300)]
        public string PlaceOfPublication { get; set; }

        [Required]
        [StringLength(300)]
        public string Publisher { get; set; }

        [Required]
        public int YearOfPublishing { get; set; }

        [Required]
        [RegularExpression(@"(^ISBN ([0-7]|(8[0-9]|9[0-4])|(9[5-8][0-9])|(99[0-3])|(99[4-8][0-9])|(999[0-9][0-9]))-\d{1,7}-\d{1,7}-([0-9]|X)$)")]
        public string ISBN { get; set; }

        [Required]
        public string[] AuthorsId { get; set; }

        public List<SelectListItem> AuthorsList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NumberOfPages < 0)
            {
                yield return new ValidationResult("Число страниц должно быть положительным", new[] { nameof(NumberOfPages) });
            }

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