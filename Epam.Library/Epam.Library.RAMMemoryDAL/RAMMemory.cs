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
            books = books.ToList();

            List<Book> answerBooks = new List<Book>();
            foreach (var book in books)
            {
                foreach (var authorOfBook in book.Authors)
                {
                    if (authorOfBook == author)
                    {
                        answerBooks.Add(book);
                    }
                }
            }

            return answerBooks;
        }

        public List<InformationResource> FindPatentsAndBooksByAuthor(Author author)
        {
            IEnumerable<IHaveAuthors> haveAuthors = RAMMemory.Library.OfType<IHaveAuthors>();
            haveAuthors = haveAuthors.ToList();

            List<InformationResource> resources = new List<InformationResource>();
            foreach (var resource in haveAuthors)
            {
                foreach (var authorOfBook in resource.GetAuthors())
                {
                    if (authorOfBook == author)
                    {
                        resources.Add((InformationResource)resource);
                    }
                }
            }

            return resources;
        }

        public List<Patent> FindPatentsByAuthor(Author author)
        {
            IEnumerable<Patent> patents = RAMMemory.Library.OfType<Patent>();
            patents = patents.ToList();

            List<Patent> answerPatents = new List<Patent>();
            foreach (var patent in patents)
            {
                foreach (var authorOfPattent in patent.Inventors)
                {
                    if (authorOfPattent == author)
                    {
                        answerPatents.Add(patent);
                    }
                }
            }

            return answerPatents;
        }

        public InformationResource FindResourceByName(string name)
        {
            List<InformationResource> Library = RAMMemory.Library;
            InformationResource resource = Library.Single(res => res.Name == name);
            return resource;
        }

        public List<InformationResource> GetLibrary()
        {
            return RAMMemory.Library;
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse)
        {
            IEnumerable<IHaveYearOfPublishing> IHaveYearOfPublishingresources = RAMMemory.Library.OfType<IHaveYearOfPublishing>();
            IHaveYearOfPublishingresources = IHaveYearOfPublishingresources.ToList();

            IEnumerable<InformationResource> resources =
                from resource in IHaveYearOfPublishingresources
                orderby resource.GetYearOfPublishing() descending
                select (InformationResource)resource;

            List<InformationResource> sortedResources = resources.ToList();
            if (reverse)
            {
                sortedResources.Reverse();
                return sortedResources;
            }
            else
            {
                return sortedResources;
            }
        }

        public List<InformationResource> GroupingResourceByYearOfPublication()
        {
            List<InformationResource> Library = RAMMemory.Library;
            var resources =
                from resource in Library
                group resource by (resource is IHaveYearOfPublication);

            List<InformationResource> informationResources = new List<InformationResource>();
            foreach (var item in resources)
            {
                foreach (var resource in item)
                {
                    if (item.Key)
                    {
                        informationResources.Add(resource);
                    }
                }
            }
            return informationResources;
        }

        public List<Book> SmartBookSearchByPublisher(string str)
        {
            IEnumerable<Book> books = RAMMemory.Library.OfType<Book>();
            books = books.ToList();

            List<Book> answerBooks = new List<Book>();
            foreach (var book in books)
            {
                if (book.Publisher.Contains(str))
                {
                    answerBooks.Add(book);
                }
            }

            return answerBooks;
        }
    }
}
