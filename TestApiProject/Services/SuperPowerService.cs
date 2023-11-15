using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestApiProject.Data;
using TestApiProject.Dtos;
using TestApiProject.Entities;
using TestApiProject.Repositories;

namespace TestApiProject.Services
{
    public class SuperPowerService
    {
        private readonly TestDbContext _database;
        private readonly SuperPowerRepository _superPowerRepository;

        

        public SuperPowerService(SuperPowerRepository superPowerRepository, TestDbContext database)
        {
            _superPowerRepository = superPowerRepository;
            _database = database;
        }

        #region CreateSuperPower
        public SuperPowerGetDto CreateSuperPower(SuperPowerPostDto input) 
        {
            var superPower = new SuperPowers
            {
                PowerName = input.PowerName,
                HealthGain = (int)input.HealthGain
            };

            if(!input.CharacterIds.IsNullOrEmpty())
            {
                superPower.CharactersSuperpowers = new List<CharactersSuperpowersJoin>();

                foreach (var characterId in input.CharacterIds)
                {
                    superPower.CharactersSuperpowers.Add(new CharactersSuperpowersJoin
                    {
                        CharacterId = characterId,
                        SuperPowerId = superPower.Id
                    });
                }
            }
            

            _superPowerRepository.CreateSuperPower(superPower);
            _superPowerRepository.SaveChanges();


            return new SuperPowerGetDto
            {
                Id = superPower.Id,
                PowerName = superPower.PowerName,
                HealthGain = superPower.HealthGain,
                CharacterIds = superPower.CharactersSuperpowers.Select(c => c.CharacterId).ToList()
            };
        }
        #endregion

        #region UpdateSuperPower
        public bool UpdateSuperPower(SuperPowerPutDto input)
        {
            var updatedSuperPower = new SuperPowers
            {
                Id = input.Id.HasValue ? (int)input.Id : 0,
                HealthGain = input.HealthGain.HasValue ? (int)input.HealthGain : 0,
                PowerName = input.PowerName
            };

            if (!input.CharacterIds.IsNullOrEmpty())
            {
                updatedSuperPower.CharactersSuperpowers = new List<CharactersSuperpowersJoin>();

                foreach (var updatedCharacterId in input.CharacterIds)
                {
                    updatedSuperPower.CharactersSuperpowers.Add(new CharactersSuperpowersJoin
                    {
                        CharacterId = updatedCharacterId,
                        SuperPowerId = updatedSuperPower.Id
                    });
                    
                }
            }

            if (_superPowerRepository.UpdateSuperPower(updatedSuperPower) ) 
            {
                _superPowerRepository.SaveChanges();
                return true; 
            }
            return false;
        }

        #endregion

        #region GetSuperPowers
        public List<SuperPowerGetDto> GetSuperPowers()
        {
            var superPowers = _superPowerRepository.GetSuperPowers()
                                                   .Include(c => c.CharactersSuperpowers)
                                                   .Select(s => new SuperPowerGetDto
                                                   {
                                                       Id = s.Id,
                                                       PowerName = s.PowerName,
                                                       HealthGain = s.HealthGain,
                                                       CharacterIds = s.CharactersSuperpowers.Select(c=> c.CharacterId).ToList()
                                                   }).ToList();
            return superPowers;
        }
        #endregion

        #region DeleteUser
        public bool DeleteUser(int superPowerId)
        {
            if (_superPowerRepository.DeleteSuperPower(superPowerId) && _superPowerRepository.DeleteCharactersSuperPowers(superPowerId))
            {
                _superPowerRepository.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion
    }
}
