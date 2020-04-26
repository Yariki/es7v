using System;
using Elasticsearch.Net;
using Nest;

namespace ESSearch
{
    class Program
    {
        static string DefaultIndexName = "testindex";
        static string DefaultIndexName2 = "testindex2";

        static string localAddress = "http://localhost:9200";

        static void Main(string[] args)
        {
            var connectionSettings = new ConnectionSettings(new Uri(localAddress));
            connectionSettings.DefaultIndex(DefaultIndexName);
            var client = new ElasticClient(connectionSettings);
            
            var connectionSettings2 = new ConnectionSettings(new Uri(localAddress));
            connectionSettings2.DefaultIndex(DefaultIndexName2);
            var client2 = new ElasticClient(connectionSettings2);
            
            var response = client.Search<Post>(
                q => q.Query(c => 
                    c.Match(d => 
                        d.Field(a => 
                            a.Author)
                            .Query("Yariki"))));

            foreach (var document in response.Documents)
            {
                 Console.WriteLine($"{document.Id}: {document.Content} : {document.Author}");
            }

            var reponse2 = client2.Search<Author>(q => q.Query(c => c
                .Match(d => d.Field(a => a.FirstName).Query("Yariki"))));

            foreach (var author in reponse2.Documents)
            {
                Console.WriteLine($"{author.Id}: {author.FirstName}: {author.LastName}");
            }

        }
    }
}
