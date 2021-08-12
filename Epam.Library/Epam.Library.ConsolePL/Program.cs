using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            List<InformationResource> informationResources = new List<InformationResource>();
            Book book = new Book("qwe",new Guid(), "asd");
            informationResources.Add(book);
            foreach (var item in informationResources)
            {
                if (item is Book)
                {
                    Console.WriteLine("book");
                }
            }
            Console.ReadKey();
        }
    }
}
