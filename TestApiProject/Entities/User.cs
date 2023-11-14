using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApiProject.Entities
{
    [Table("Users", Schema = "game")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string SurName { get; set; }
        public int Age { get; set; }
        [ForeignKey("Character")]
        public int? CharacterId { get; set; }

        public virtual Character? Character { get; set; }
    }
}
