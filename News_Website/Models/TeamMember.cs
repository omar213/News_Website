using System.ComponentModel.DataAnnotations.Schema;

namespace News_Website.Models
    

{
    public class TeamMember
    { 
    public int Id { get; set; }
    public string ?Name { get; set; }
    public string ?Job { get; set; }
    public string ?Image { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
