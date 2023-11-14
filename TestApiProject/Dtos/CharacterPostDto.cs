using System.ComponentModel.DataAnnotations;
using TestApiProject.Entities;

namespace TestApiProject.Dtos
{
    public class CharacterPostDto
    {
        public int? Id { get; set; }

        public string? CharacterName { get; set; }

        public int? CharacterHealth { get; set; }

        public string? CharacterType { get; set; }

        public List<int>? UserIds { get; set; }

        public List<int>? SuperPowerIds { get; set; }

        public ICollection<User>? Users { get; set; }

        public ICollection<SuperPowers>? SuperPowers { get; set; }
    }
}
