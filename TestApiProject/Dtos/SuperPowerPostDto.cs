using System.ComponentModel.DataAnnotations;
using TestApiProject.Entities;

namespace TestApiProject.Dtos
{
    public class SuperPowerPostDto
    {
        
        public string? PowerName { get; set; }

        public int? HealthGain { get; set; }
    }
}
