using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace News_Website.Models
{
    public class Contactus
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Message { get; set; }

        public int Email { get; set; }

        public int Subject { get; set; }

    }
}
