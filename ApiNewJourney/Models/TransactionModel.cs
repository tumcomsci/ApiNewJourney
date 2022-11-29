namespace ApiNewJourney.Models
{
    public class TransactionModel
    {
        public Guid TransactionId { get; set; }
        public Guid? OriginAccount { get; set; }
        public Guid? DestinationAccount { get; set; }
        public Decimal? CurrentBalance { get; set; }
        public Decimal? Amount { get; set; }
        public Guid? PackageId { get; set; }
        public Decimal? PackageCalculated { get; set; }
        public Guid? TypeId { get; set; }
        public DateTime? CreatedDate { get; set; }  
        public DateTime? ModifiedDate { get; set; }
    }
}
