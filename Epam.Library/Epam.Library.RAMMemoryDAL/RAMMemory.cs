using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using Epam.Library.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.RAMMemoryDAL
{
    public class RAMMemory : IInformationResourceDAL
    {
        public static List<InformationResource> Library = new List<InformationResource>();

        public List<Book> FindBooksByAuthor(Author author)
        {
            IEnumerable<Book> books = RAMMemory.Library.OfType<Book>();
            var answerBooks = books.Where(book => book.GetAuthors().Contains(author));

            return answerBooks.ToList();
        }

        public List<InformationResource> FindPatentsAndBooksByAuthor(Author author)
        {
            IEnumerable<IHaveAuthors> haveAuthors = RAMMemory.Library.OfType<IHaveAuthors>();

            var resources = haveAuthors.Where(resource => resource.GetAuthors().Contains(author)).Select(resource => (InformationResource)resource);

            return resources.ToList();
        }

        public List<Patent> FindPatentsByAuthor(Author author)
        {
            IEnumerable<Patent> patents = RAMMemory.Library.OfType<Patent>();
            var answerPatents = patents.Where(patent => patent.GetAuthors().Contains(author));

            return answerPatents.ToList();
        }

        public List<InformationResource> FindResourcesByName(string name)
        {
            var resources = RAMMemory.Library.Where(resource => resource.Name == name);
            return resources.ToList();
        }

        public List<InformationResource> GetLibrary()
        {
            return RAMMemory.Library;
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse)
        {
            IEnumerable<IHaveYearOfPublishing> iHaveYearOfPublishingresources = RAMMemory.Library.OfType<IHaveYearOfPublishing>();
            IEnumerable<IHaveYearOfPublishing> resources;
            if (reverse)
            {
                resources = iHaveYearOfPublishingresources.OrderByDescending(x => x.GetYearOfPublishing());
            }
            else
            {
                resources = iHaveYearOfPublishingresources.OrderBy(x => x.GetYearOfPublishing());
            }
            return resources.Cast<InformationResource>().ToList();
        }

        public Dictionary<int ,List<InformationResource>> GroupingResourceByYearOfPublication()
        {
            IEnumerable<IHaveYearOfPublishing> iHaveYearOfPublishingresources = RAMMemory.Library.OfType<IHaveYearOfPublishing>();
            //var resources = iHaveYearOfPublishingresources.OrderBy(g => g.GetYearOfPublishing()).GroupBy(g => g.GetYearOfPublishing()).ToDictionary(g => g.Key, g => g.Cast<InformationResource>().ToList());
            var resources = iHaveYearOfPublishingresources.GroupBy(g => g.GetYearOfPublishing()).ToDictionary(g => g.Key, g => g.Cast<InformationResource>().ToList());
            return resources;
        }

        public Dictionary<string, List<Book>> SmartBookSearchByPublisher(string str)
        {
            IEnumerable<Book> books = RAMMemory.Library.OfType<Book>();
            var answer = books.Where(x => x.Publisher.StartsWith(str));
            //var groupBooks = answer.OrderBy(g => g.Publisher).GroupBy(g => g.Publisher).ToDictionary(g => g.Key, g => g.ToList());
            var groupBooks = answer.GroupBy(g => g.Publisher).ToDictionary(g => g.Key, g => g.ToList());

            return groupBooks;
        }
    }
}
