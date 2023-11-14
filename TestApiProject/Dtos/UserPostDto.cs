using TestApiProject.Entities;

namespace TestApiProject.Dtos
{
    public class UserPostDto
    {
        public string? Name { get; set; }

        public string? SurName { get; set; }

        public int? Age { get; set; }

        public int? CharacterId { get; set; }
    }
}
