using Microsoft.EntityFrameworkCore;
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

        public SuperPowerService(SuperPowerRepository superPowerRepository)
        {
            _superPowerRepository = superPowerRepository;
        }
        #region CreateSuperPower
        public SuperPowerGetDto CreateSuperPower(SuperPowerPostDto input) 
        {
            var superPower = new SuperPowers
            {
                PowerName = input.PowerName,
                HealthGain = (int)input.HealthGain
            };

            _superPowerRepository.CreateSuperPower(superPower);
            _superPowerRepository.SaveChanges();

            return new SuperPowerGetDto
            {
                Id = superPower.Id,
                PowerName = superPower.PowerName,
                HealthGain = superPower.HealthGain
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

            if(_superPowerRepository.UpdateSuperPower(updatedSuperPower) ) 
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
                                                   .Select(s => new SuperPowerGetDto
                                                   {
                                                       Id = s.Id,
                                                       PowerName = s.PowerName,
                                                       HealthGain = s.HealthGain,
                                                   }).ToList();
            return superPowers;
        }
        #endregion

        #region DeleteUser
        public bool DeleteUser(int superPowerId)
        {
            if (_superPowerRepository.DeleteSuperPower(superPowerId))
            {
                _superPowerRepository.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion
    }
}
