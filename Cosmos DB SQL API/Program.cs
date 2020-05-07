using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace Cosmos_DB_SQL_API
{
    public class Program
    {
        static string DatabaseName = "mydb";
        static string CollectionName = "student";
        static DocumentClient dc;

        static string endpoint = "https://vishalaccount.documents.azure.com:443/";
        static string key = "WXPVqjTy9afejJR8z1wjW6TNYaXPsFEbVefNbfR94uU2Bmc5FIcSniFbOXX8JqQ3Rg31pdENc317WiOO4WwrAQ==";
        static void Main(string[] args)
        {
            dc = new DocumentClient(new Uri(endpoint), key);
            /*            InsertOp("John", "Wick");
                        InsertOp("Will", "Smith");
                        InsertOp("Robert", "Downey");*/

            QueryOp();

            Console.WriteLine("\n\n");
            Console.WriteLine("Press any key to end");
            Console.ReadKey();

            static void InsertOp(string fname, string lname)
            {
                StudentEntity studentno1 = new StudentEntity();
                studentno1.FirstName = fname;
                studentno1.FirstName = lname;

                var result = dc.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), studentno1).GetAwaiter().GetResult();
            }
            static void QueryOp()
            {
                FeedOptions queryoptions = new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true };

                IQueryable<StudentEntity> query = dc.CreateDocumentQuery<StudentEntity>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseName, CollectionName), queryoptions)
                     .Where(e => e.LastName == "smith");

            Console.WriteLine("Names of all students");
            foreach (StudentEntity std in query)
            {
                Console.WriteLine(std);
            }
        }
     }

        public class StudentEntity 
        {
         public string FirstName { get; set; }
         public string LastName { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

    }
}