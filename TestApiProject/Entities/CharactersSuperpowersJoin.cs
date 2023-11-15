using System.ComponentModel.DataAnnotations.Schema;

namespace TestApiProject.Entities
{
    [Table("CharactersSuperpowersJoin", Schema = "game")]
    public class CharactersSuperpowersJoin
    {
        public int CharacterId { get; set; }
        public int SuperPowerId { get; set; }
        public virtual Characters Character { get; set; }
        public virtual SuperPowers SuperPower { get; set; }
    }
}
