namespace ApiNewJourney.Models
{
    public class AccountBalanceModel
    {
        public Guid AccountBalanceId { get; set; }
        public Guid AccountId { get; set; }
        public Decimal? Balance { get; set; }
        public bool? IsActive { get; set;}
        public DateTime? CreateDate { get; set; }
    }
}
