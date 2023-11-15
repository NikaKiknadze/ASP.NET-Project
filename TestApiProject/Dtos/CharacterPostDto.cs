using System.ComponentModel.DataAnnotations;
using TestApiProject.Entities;

namespace TestApiProject.Dtos
{
    public class CharacterPostDto
    {
        public string? CharacterName { get; set; }

        public int? CharacterHealth { get; set; }

        public string? CharacterType { get; set; }

        public List<int>? UserIds { get; set; }

        public List<int>? SuperPowerIds { get; set; }
    }
}
