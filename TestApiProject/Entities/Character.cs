using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace TestApiProject.Entities
{
    [Table("Characters", Schema = "game")]
    public class Characters
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string CharacterName { get; set; }

        public int CharacterHealth { get; set; }
        [MaxLength(10)]
        public string CharacterType { get; set; }
        

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<CharactersSuperpowersJoin> CharactersSuperpowers { get; set;}

    }
}
