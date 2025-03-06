using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CpApi.Model
{
    public class Films
    {
        [Key]
        public int id_Film { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        [Required]
        [ForeignKey("Genres")]
        public int Genre_id { get; set; }
        public Genres Genres { get; set; }
        public DateTime DateCreate { get; set; }
        public double Rating { get; set; }

    }
}
