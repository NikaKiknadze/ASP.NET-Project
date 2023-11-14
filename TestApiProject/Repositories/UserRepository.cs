using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using TestApiProject.Data;
using TestApiProject.Entities;

namespace TestApiProject.Repositories
{
    public class UserRepository
    {
        private readonly TestDbContext _database;
        public UserRepository(TestDbContext database)
        {
            _database = database;
        }
        #region SaveChanges
        public void SaveChanges()
        {
            _database.SaveChanges();
        }
        #endregion
        
        #region GetUserById
        public User GetUserById(int userId)
        {
            return _database.Users
                                  .AsNoTracking()
                                  .Include(u => u.Character)
                                  .FirstOrDefault(u => u.Id == userId);
        }
        #endregion

        #region GetUsers
        public IQueryable<User> GetUsers()
        {
            return _database.Users.AsQueryable();
        }
        #endregion

        #region CreateUser
        public User CreateUser(User user)
        {
            _database.Users.Add(user);
            return user;
        }
        #endregion

        #region UpdateUser
        public bool UpdateUser(User updatedUser)
        {
            var existingUser = _database.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = updatedUser.Name;
            existingUser.SurName = updatedUser.SurName;
            existingUser.Age = updatedUser.Age;
            existingUser.CharacterId = updatedUser.CharacterId;

            return true;
        }
        #endregion

        #region DeleteUser
        public bool DeleteUser(int userId)
        {
            var user = _database.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            _database.Users.Remove(user);
            return true;
        }
        #endregion
    }
}
