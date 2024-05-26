using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Connection : DBEntity
    {
        public int DepartureCityId { get; set; }

        [ForeignKey("DepartureCityId")]
        public City Departure { get; set; }

        public int DestinationCityId { get; set; }

        [ForeignKey("DestinationCityId")]
        public City Destination { get; set; }

        public double Distance { get; set; }
        public double Duration { get; set; }
        public double Direction { get; set; }
    }
}
