namespace NearEarthObject_WebApp.Models
{
    public class NearEarthObject
    {
        public string Name { get; set; }
        public double EstimatedDiameterMeters { get; set; }
        public double EstimatedDiameterFeet { get; set; }
        public double MissDistanceKm { get; set; }
        public double MissDistanceMi { get; set; }
        public double VelocityKmPerHour { get; set; }
        public double VelocityMiPerHour { get; set; }
        public bool IsPotentiallyDangerous { get; set; }
        public bool IsSentryObject { get; set; }
        public string OrbitingBody { get; set; }
        public string NasaJplUrl { get; set; }
    }
}
