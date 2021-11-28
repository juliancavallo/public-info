using Newtonsoft.Json;
using RestSharp;
using PublicInfo.Domain.Entities;
using System.Net;
using PublicInfo.Domain.Services;
using PublicInfo.Domain.Entities.Responses;

namespace PublicInfo.Services
{
    public class GovernmentAPIService : IGovernmentAPIService
    {
        public string GetDatasetCsvURL(string datasetName)
        {
            var restClient = new RestClient($"https://datos.gob.ar/api/3/action/package_show?id={ datasetName }")
            {
                Timeout = -1,
            };
            var restRequestGet = new RestRequest(Method.GET);
            restRequestGet.AddHeader("Content-Type", "application/json");

            var apiResponse = restClient.Execute(restRequestGet);
            if (apiResponse.StatusCode == HttpStatusCode.OK)
            {
                var response = JsonConvert.DeserializeObject<GovernmentAPIResponse.Root>(apiResponse.Content);
                return response.result.resources[0].url;

            }
            else
            {
                throw apiResponse.ErrorException;
            }
        }
    }
}
