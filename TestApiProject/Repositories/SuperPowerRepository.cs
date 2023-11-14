using TestApiProject.Data;
using TestApiProject.Entities;

namespace TestApiProject.Repositories
{
    public class SuperPowerRepository
    {
        private readonly TestDbContext _database;
        public SuperPowerRepository(TestDbContext database)
        {
            _database = database;
        }

        #region SaveChanges
        public void SaveChanges()
        {
            try
            {
                _database.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        #endregion

        #region GetSuperPowers
        public IQueryable<SuperPowers> GetSuperPowers()
        {
            return _database.SuperPowers.AsQueryable();
        }
        #endregion

        #region DeleteSuperPower
        public bool DeleteSuperPower(int superPowerId)
        {
            var superPower = _database.SuperPowers.FirstOrDefault(s => s.Id == superPowerId);
            if(superPower == null)
            {
                return false;
            }
            _database.SuperPowers.Remove(superPower);
            return true;
        }
        #endregion

        #region CreateSuperPower
        public SuperPowers CreateSuperPower(SuperPowers superPower)
        {
            _database.SuperPowers.Add(superPower);
            return superPower;
        }
        #endregion

        #region UpdateSuperPower
        public bool UpdateSuperPower(SuperPowers updatedSuperPower)
        {
            var existingSuperPower = _database.SuperPowers.FirstOrDefault(s => s.Id == updatedSuperPower.Id);

            if (existingSuperPower == null)
            {
                return false;
            }

            existingSuperPower.HealthGain = updatedSuperPower.HealthGain;
            existingSuperPower.PowerName = updatedSuperPower.PowerName;
            existingSuperPower.CharactersSuperpowers = updatedSuperPower.CharactersSuperpowers;
            
            return true;
        }
        #endregion
    }
}
