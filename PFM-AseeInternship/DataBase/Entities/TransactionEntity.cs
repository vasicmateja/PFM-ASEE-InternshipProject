using CsvHelper.Configuration.Attributes;
using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.DataBase.Entities
{
    public class TransactionEntity
    {
        [Name("id")]
        public int Id { get; set; }

        [Name("beneficiary-name")]
        public string BeneficiaryName { get; set; }

        [Name("date")]
        public string Date { get; set; }

        [Name("direction")]
        public Directions Direction { get; set; }

        [Name("amount")]
        public double Amount { get; set; }

        [Name("description")]
        public string Description { get; set; }

        [Name("currency")]
        public string Currency { get; set; }

        [Name("mcc")]
        public string MccCode { get; set; }

        [Name("kind")]
        public KindEnum Kind { get; set; }

        public string CatCode { get; set; }
    }

}

