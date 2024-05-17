using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Api.Gax.Grpc;
using Google.Api.Gax.Grpc.Rest;
using Google.Apis.Auth.OAuth2;
using Google.Maps.Routing.V2;
using Google.Protobuf.WellKnownTypes;
using Google.Type;
using Microsoft.Extensions.Configuration;
using static Google.Maps.Routing.V2.Routes;

namespace Business.Services
{
    public class RouteService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string? ApiKey;

        public RouteService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            ApiKey = _configuration["KEYS:RouteAPI"];
        }
        public async Task<ComputeRoutesResponse> GetRouteAsync(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            var reponse = await ComputeRoutesRequestObjectAsync(endLatitude, endLongitude, startLatitude, startLongitude, Timestamp.FromDateTime(System.DateTime.UtcNow.AddMinutes(1)));
            return reponse;
        }
        public async Task<ComputeRoutesResponse> ComputeRoutesRequestObjectAsync(double sLat, double eLat, double sLong, double eLong, Timestamp departure)
        {
            var client = new RoutesClientBuilder
            {
                GrpcAdapter = RestGrpcAdapter.Default
            }.Build();
            CallSettings callSettings = CallSettings.FromHeader("X-Goog-FieldMask", "*");
            // Initialize request argument(s)
            ComputeRoutesRequest request = new ComputeRoutesRequest
            {
                Origin = new Waypoint
                {
                    Location = new Location { LatLng = new LatLng { Latitude = sLat, Longitude = sLong } }
                },
                Destination = new Waypoint
                {
                    Location = new Location { LatLng = new LatLng { Latitude = eLat, Longitude = eLong } }
                },
                TravelMode = RouteTravelMode.Bicycle,
                PolylineQuality = PolylineQuality.HighQuality,
                DepartureTime = departure,
                RouteModifiers = new RouteModifiers(),
                LanguageCode = "en-US",
                Units = Units.Metric,
                PolylineEncoding = PolylineEncoding.Unspecified,
                
            };
            // Make the request
            ComputeRoutesResponse response = await client.ComputeRoutesAsync(request, callSettings);
            return response;
        }
    }
}
