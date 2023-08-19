using ElasticSearch_TextSearchUsage.Model;
using Faker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Reflection.Metadata;

namespace ElasticSearch_TextSearchUsage.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DataImportController : ControllerBase
    {

        [HttpPost(Name = "AddFakeText")]
        public void AddFakeText()
        {
            var settings = new ConnectionSettings(new Uri("https://localhost:9200/")).BasicAuthentication("serkan", "pass123").DefaultIndex("documents-person").ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);

            var client = new ElasticClient(settings);

            var createIndexResponse = client.Indices.Create("documents-person", c => c
    .Map<Person>(m => m
        .AutoMap()
    )
);

            var people = new List<Person>();
            for (int i = 130350; i <= 1000000; i++)
            {             
                client.IndexDocument(new Person
                {
                    Id = i,
                    Name = Faker.Name.FullName(),
                    Description = Lorem.Sentence(10)
                });
            }        

        }
    }
}
