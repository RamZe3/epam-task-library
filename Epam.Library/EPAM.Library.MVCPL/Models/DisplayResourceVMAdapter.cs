using Epam.Library.Entities;
using EPAM.Library.MVCPL.ViewModels.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.Library.MVCPL.Models
{
    public class DisplayResourceVMAdapter
    {
        public InformationResource Resource { get; set; }

        public DisplayResourceVMAdapter(InformationResource resource)
        {
            Resource = resource;
        }

        public DisplayResourceVM Adapt()
        {
            DisplayResourceVM displayResourceVM;
            if (Resource is Paper)
            {
                displayResourceVM = new DisplayResourceVM
                {
                    Id = Resource.Id,
                    NumberOfPages = Resource.NumberOfPages,
                    Name = GetPaperName((Paper)Resource),
                    Identifier = GetPaperIdentifier((Paper)Resource)
                };
                return displayResourceVM;
            }

            return new DisplayResourceVM();
        }

        private string GetPaperName(Paper paper)
        {
            string paperName = String.Format("{0} ", paper.Name);
            if (paper.Number != -1)
            {
                paperName += String.Format("№{0}/{1}", paper.Number, paper.YearOfPublishing);
            }
            else
            {
                paperName += String.Format(" {0}", paper.YearOfPublishing);
            }

            return paperName;
        }

        private string GetPaperIdentifier(Paper paper)
        {
            return paper.ISSN;
        }
    }
}