using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Epam.Library.REST_API.Models.Patents
{
    public class CreatePatentModel
    {
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
        public List<string> AuthorsId { get; set; } = new List<string>();
    }
}