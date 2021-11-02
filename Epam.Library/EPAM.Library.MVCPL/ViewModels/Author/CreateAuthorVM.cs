﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.ViewModels.Author
{
    public class CreateAuthorVM
    {
        [Required]
        [RegularExpression(@"(^[A-Z]([a-z]|(\-[A-Z]))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ]))+$)")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"(^[A-Z]([a-z]|(\-[A-Z])|([a-z] ([a-z]|[A-Z])))+$)|(^[А-ЯЁ]([а-яё]|(\-[А-ЯЁ])|([а-яё] ([а-яё]|[А-ЯЁ])))+$)")]
        [StringLength(200)]
        public string Surname { get; set; }
    }
}