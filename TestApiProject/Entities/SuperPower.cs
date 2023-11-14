using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApiProject.Entities
{
    [Table("SuperPowers", Schema = "game")]
    public class SuperPowers
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string PowerName { get; set; }
        public int HealthGain { get; set; }

        public virtual ICollection<CharactersSuperpowersJoin> CharactersSuperpowers { get; set; }
    }
}
