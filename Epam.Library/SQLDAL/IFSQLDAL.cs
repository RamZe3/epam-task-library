using Epam.Library.DAL.Interfaces;
using Epam.Library.Entities;
using SQLDAL.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Library.SQLDAL
{
    public class IFSQLDAL : IInformationResourceDAL
    {
        private string _connectionString = @"Data Source=DESKTOP-SL9L2I0\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Book> FindBooksByAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> FindPatentsAndBooksByAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public List<Patent> FindPatentsByAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> FindResourcesByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<InformationResource> GetLibrary()
        {
            List<InformationResource> resources = new List<InformationResource>();

            using (var _connection = new SqlConnection(_connectionString))
            {
                var stProc = "Resources_GetResources";
                var srProcAuthors = "Authors_GetAuthors";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                var Authorscommand = new SqlCommand(srProcAuthors, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                _connection.Open();

                List<AuthorWithResourceID> authorWithResourceIDs = new List<AuthorWithResourceID>();
                var Authorreader = Authorscommand.ExecuteReader();
                while (Authorreader.Read())
                {
                    Guid ResourceId = (Guid)Authorreader["ResourceID"];
                    Author author = new Author(
                        id: (Guid)Authorreader["AuthorID"],
                        name: Authorreader["Name"] as string,
                        surname: Authorreader["SurName"] as string
                        );
                    authorWithResourceIDs.Add(new AuthorWithResourceID(ResourceId, author));
                }
                Authorreader.Close();

                var reader = command.ExecuteReader();



                while (reader.Read())
                {
                    String Type = reader["Type"] as string;
                    switch (Type)
                    {
                        case "Book":
                            Book book = new Book(
                                name: reader["Name"] as string,
                                id: (Guid)reader["ID"],
                                numberOfPages: (int)reader["NumberOfPages"],
                                note: reader["Note"] as string,
                                placeOfPublication: reader["PlaceOfPublication"] as string,
                                publisher: reader["Publisher"] as string,
                                yearOfPublishing: ((DateTime)reader["YearOfPublishing"]).Year,
                                iSBN: reader["ISBN"] as string
                                );
                            //resources.Add(new Book(
                            //    name: reader["Name"] as string,
                            //    id: (Guid)reader["ID"],
                            //    numberOfPages: (int)reader["NumberOfPages"],
                            //    note: reader["Note"] as string,
                            //    placeOfPublication: reader["PlaceOfPublication"] as string,
                            //    publisher: reader["Publisher"] as string,
                            //    yearOfPublishing: ((DateTime)reader["YearOfPublishing"]).Year,
                            //    iSBN: reader["ISBN"] as string
                            //    ));

                            foreach (var author in authorWithResourceIDs)
                            {
                                if (author.ResourceId == (Guid)reader["ID"])
                                {
                                    book.Authors.Add(author.author);
                                    //authorWithResourceIDs.Remove(author);
                                }
                            }

                            resources.Add(book);
                            break;
                        case "Patent":
                            Patent patent = new Patent(
                                name: reader["Name"] as string,
                                id: (Guid)reader["ID"],
                                numberOfPages: (int)reader["NumberOfPages"],
                                note: reader["Note"] as string,
                                country: reader["Country"] as string,
                                dateOfApplication: (DateTime)reader["DateOfApplication"],
                                dateOfPublication: ((DateTime)reader["DateOfPublication"]),
                                registrationNumber: (int)reader["RegistrationNumber"]
                                );
                            foreach (var author in authorWithResourceIDs)
                            {
                                if (author.ResourceId == (Guid)reader["ID"])
                                {
                                    patent.Inventors.Add(author.author);
                                    //authorWithResourceIDs.Remove(author);
                                }
                            }
                            resources.Add(patent);
                            break;
                        case "Paper":
                            Paper paper = new Paper(
                                name: reader["Name"] as string,
                                id: (Guid)reader["ID"],
                                numberOfPages: (int)reader["NumberOfPages"],
                                note: reader["Note"] as string,
                                placeOfPublication: reader["PlaceOfPublication"] as string,
                                publisher: reader["Publisher"] as string,
                                yearOfPublishing: ((DateTime)reader["YearOfPublishing"]).Year,
                                number: (int)reader["Number"],
                                date: (DateTime)reader["Date"],
                                iSSN: reader["ISSN"] as string
                                );
                            resources.Add(paper);
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(reader["Type"] as string);
                    //return new ForumPost(
                    //    id: (Guid)reader["Id"],
                    //    text: reader["Text"] as string,
                    //   publicationDate: (DateTime)reader["PublicationDate"],
                    //   authorId: (Guid)reader["AuthorId"],
                    //   themeId: (Guid)reader["ThemeId"]);
                }

                

                //command.ExecuteNonQuery();

                _connection.Close();

                return resources;
            }
        }

        public List<InformationResource> GetSortedLibraryByYearOfPublishing(bool reverse)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, List<InformationResource>> GroupingResourceByYearOfPublication()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Book>> SmartBookSearchByPublisher(string str)
        {
            throw new NotImplementedException();
        }
    }
}
