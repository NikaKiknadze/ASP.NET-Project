using System.ComponentModel.DataAnnotations;

namespace TestApiProject.Dtos
{
    public class CharacterPutDto
    {
        public int? Id { get; set; }

        public string? CharacterName { get; set; }

        public int? CharacterHealth { get; set; }

        public string? CharacterType { get; set; }

        public List<int>? UserIds { get; set;}

        public List<int>? SuperPowerIDs { get; set; }
    }
}
