using CsvHelper.Configuration.Attributes;

namespace PFM_AseeInternship.DataBase.Entities
{
    public class CategoryEntity
    {
        [Name("code")]
        public string CategoryId { get; set; }

        [Name("name")]
        public string Name { get; set; }
        [Name("parent-code")]
        public string ParentCode { get; set; }
    }
}
