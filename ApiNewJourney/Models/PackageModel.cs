namespace ApiNewJourney.Models
{
    public class PackageModel
    {
        public Guid PackageId { get; set; }
        public String? Name { get; set; }
        public Decimal? Amount { get; set; }
        public Decimal? Ratio { get; set; }
        public DateTime? ValidStartDate { get; set; }
        public DateTime? ValidEndDate { get; set; }
    }
}
