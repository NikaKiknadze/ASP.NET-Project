using System.ComponentModel.DataAnnotations;

namespace TestApiProject.Dtos
{
    public class SuperPowerPutDto
    {
        public int? Id { get; set; }
        
        public string? PowerName { get; set; }

        public int? HealthGain { get; set; }

        public List<int>? CharacterIds { get; set; }
    }
}
