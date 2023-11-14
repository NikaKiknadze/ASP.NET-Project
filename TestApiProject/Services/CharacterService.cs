using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.Serialization;
using TestApiProject.Data;
using TestApiProject.Dtos;
using TestApiProject.Entities;
using TestApiProject.Repositories;

namespace TestApiProject.Services
{
    public class CharacterService
    {
        private readonly TestDbContext _database;
        private readonly CharacterRepository _characterRepository;

        

    }
}
