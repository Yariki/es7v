using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;

namespace EStestProj
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
            connectionSettings.DefaultMappingFor<Post>(m => m.IdProperty(p => p.Id).IndexName(DefaultIndexName));    
            connectionSettings.DefaultMappingFor<Comment>(m => m.IdProperty(c => c.Id).IndexName(DefaultIndexName));
            
            var connectionSettings2 = new ConnectionSettings(new Uri(localAddress));
            connectionSettings2.DefaultIndex(DefaultIndexName2);
            connectionSettings2.DefaultMappingFor<Author>(m => m.IdProperty(a => a.Id).IndexName(DefaultIndexName2));

            var client = new ElasticClient(connectionSettings);
            var client2 = new ElasticClient(connectionSettings2);

            CheckAndRemoveIndex(client, DefaultIndexName);
            CheckAndRemoveIndex(client2, DefaultIndexName2);
            
            var response = client.Indices.Create(DefaultIndexName,c => c
                            .Map<Post>(m => m
                                .AutoMap()
                                .Properties(p => p
                                    .Nested<Comment>(np => np
                                        .AutoMap()
                                        .Name(post => post.Comments)
                                    )
                                )
                            )
            );

            var response2 = client2.Indices.Create(DefaultIndexName2, c => c
            .Map<Author>(m => m
                .AutoMap()
            ));

            if (!IsResposeValid(response)) return;

            if (!IsResposeValid(response2)) return;

            IndexData(client, Data.Posts);
            IndexData(client2, Data.Authors);
        }

        private static void CheckAndRemoveIndex(IElasticClient client, string indexName)
        {
            var resp = client.Indices.Exists(indexName);
            if (!resp.Exists) return;

            client.Indices.Delete(indexName);
        }
        
        private static void IndexData<T>(IElasticClient client, IEnumerable<T> data) where T : Entity
        {
            foreach (var doc in data)
            {
                var resp = client.IndexDocument(doc);
                Console.WriteLine($"{resp.Index} << {resp.Result} << {resp.Type}");
            }
        }

        private static bool IsResposeValid(CreateIndexResponse response)
        {
            if (response.ServerError == null)
            {
                Console.WriteLine(response.Index);
                Console.WriteLine("Index was created");
            }
            else
            {
                Console.WriteLine($"There is a error during creating index <{response.Index}>:");
                Console.WriteLine(response.ServerError.Error.ToString());
                return false;
            }

            return true;
        }
    }
}
