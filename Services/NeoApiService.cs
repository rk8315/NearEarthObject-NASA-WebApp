using NearEarthObject_WebApp.Models;
using Newtonsoft.Json.Linq;

namespace NearEarthObject_WebApp.Services
{
    public class NeoApiService
    {
        private readonly string _apiKey;
        private readonly ILogger<NeoApiService> _logger;

        public NeoApiService(IConfiguration configuration, ILogger<NeoApiService> logger)
        {
            _apiKey = Environment.GetEnvironmentVariable("NASA_API_KEY");
            _logger = logger;
        }

        public async Task<List<NearEarthObject>> FetchNeoDataAsync(string startDate, string endDate)
        {
            string url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key={_apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);

                var data = JObject.Parse(response);

                List<NearEarthObject> neos = new List<NearEarthObject>();

                foreach (var day in ((JObject)data["near_earth_objects"]).Properties())
                {
                    foreach (var neo in day.Value)
                    {
                        neos.Add(new NearEarthObject
                        {
                            Name = (string)neo["name"],
                            EstimatedDiameterMeters = ((double)neo["estimated_diameter"]["meters"]["estimated_diameter_min"] + (double)neo["estimated_diameter"]["meters"]["estimated_diameter_max"]) / 2.0,
                            EstimatedDiameterFeet = ((double)neo["estimated_diameter"]["feet"]["estimated_diameter_min"] + (double)neo["estimated_diameter"]["feet"]["estimated_diameter_max"]) / 2.0,
                            MissDistanceKm = (double)neo["close_approach_data"][0]["miss_distance"]["kilometers"],
                            MissDistanceMi = (double)neo["close_approach_data"][0]["miss_distance"]["miles"],
                            VelocityKmPerHour = (double)neo["close_approach_data"][0]["relative_velocity"]["kilometers_per_hour"],
                            VelocityMiPerHour = (double)neo["close_approach_data"][0]["relative_velocity"]["miles_per_hour"],
                            IsPotentiallyDangerous = (bool)neo["is_potentially_hazardous_asteroid"],
                            IsSentryObject = (bool)neo["is_sentry_object"],
                            OrbitingBody = (string)neo["close_approach_data"][0]["orbiting_body"]
                        });
                    }
                }

                return neos;
            }
        }
    }
}
