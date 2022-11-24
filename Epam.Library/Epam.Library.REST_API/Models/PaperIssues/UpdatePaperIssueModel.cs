using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.PaperIssues
{
    public class UpdatePaperIssueModel : IValidatableObject
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        public int Number { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression(@"(^ISSN \d{4}-\d{4}$)")]
        public string ISSN { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NumberOfPages < 0)
            {
                yield return new ValidationResult("Число страниц должно быть положительным", new[] { nameof(NumberOfPages) });
            }

            if (Number < 0)
            {
                yield return new ValidationResult("Номер должен быть положительным", new[] { nameof(Number) });
            }
        }
    }
}