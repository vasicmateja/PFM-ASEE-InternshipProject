namespace PFM_AseeInternship.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string BeneficiaryName { get; set; }
        public string Date {get; set; }
        public Directions Direction { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string MccCode { get; set; } //TODO: Pitaj Nikoluu
        public KindEnum Kind { get; set; }

        public string CatCode { get; set; }
        public List<SplitCategory> splits { get; set; }
    }
}
