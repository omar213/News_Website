using System.ComponentModel.DataAnnotations.Schema;

namespace News_Website.Models
{
    public class News
    {
        public int ?Id { get; set; }
        public string ?Title { get; set; }
        public DateTime ?Date { get; set; }
        public string ?Image { get; set; }
        public string ?Topic { get; set; }

        [ForeignKey("Category")]
        public int ?CategoryId { get; set; }
        public Category ?Category { get; set; }


        [NotMapped]
        public IFormFile ?File { get; set; } 

    }
}
