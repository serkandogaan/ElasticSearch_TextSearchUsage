using ElasticSearch_TextSearchUsage.Model;
using Faker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Text.Json;

namespace ElasticSearch_TextSearchUsage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchTextController : ControllerBase
    {
        
        [HttpGet]
        public List<Person> Search(string searchText)
        {
            var settings = new ConnectionSettings(new Uri("https://localhost:9200/")).BasicAuthentication("serkan", "pass123").DefaultIndex("documents-person").ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);

            var client = new ElasticClient(settings);        

            var searchResponse = client.Search<Person>(s => s
            .Index("documents-person")
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Description)
                    .Query(searchText)
                )
            )
        ).Documents.ToList();


            return searchResponse;


        }
    }
}
