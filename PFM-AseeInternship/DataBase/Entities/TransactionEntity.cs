using PFM_AseeInternship.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFM_AseeInternship.DataBase.Entities
{
    public class TransactionEntity
    {
        public int Id { get; set; }
        public string BeneficiaryName { get; set; }
        public string Date { get; set; }
        public Directions Direction { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string MccCode { get; set; }
        public KindEnum Kind { get; set; }
        public string CatCode { get; set; }

        //[NotMapped]public List<SplitCategory> Splits { get; set; }
    }
}
