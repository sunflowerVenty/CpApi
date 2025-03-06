using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CpApi.Model
{
    public class Genres
    {
        [Key]
        public int id_Genre { get; set; }
        [Unicode]
        public string NameGenre { get; set; }
    }
}
