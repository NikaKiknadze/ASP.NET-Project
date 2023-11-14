using Microsoft.EntityFrameworkCore;
using TestApiProject.Data;
using TestApiProject.Dtos;
using TestApiProject.Entities;
using TestApiProject.Repositories;

namespace TestApiProject.Services
{
    public class UserService
    {
        private readonly TestDbContext _database;
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        #region GetUsersById
        public List<UserGetDto> GetUserById(int userId)
        {
            var user = _userRepository.GetUsers()
                                      .Include(u => u.Character)
                                      .Where(u => u.Id == userId)
                                      .Select(u => new UserGetDto
                                      {
                                          Id = u.Id,
                                          Name = u.Name,
                                          SurName = u.SurName,
                                          Age = u.Age,
                                          Character = new CharacterGetDto
                                          {
                                              Id = (int)u.CharacterId,
                                              CharacterName = (string)u.Character.CharacterName,
                                              CharacterHealth = (int)u.Character.CharacterHealth,
                                              CharacterType = (string)u.Character.CharacterType
                                          }
                                      }).ToList();

            return user;
        }
        #endregion

        #region GetUsers
        public List<UserGetDto> GetUsers()
        {


            var users = _userRepository.GetUsers()
                                       .Include(u => u.Character)
                                       .Select(u => new UserGetDto
                                       {
                                           Id = u.Id,
                                           Name = u.Name,
                                           SurName = u.SurName,
                                           Age = u.Age,
                                           Character = new CharacterGetDto
                                           {
                                               Id = (int)u.CharacterId,
                                               CharacterName = (string)u.Character.CharacterName,
                                               CharacterHealth = (int)u.Character.CharacterHealth,
                                               CharacterType = (string)u.Character.CharacterType
                                           }
                                       }).ToList();

            return users;
        }
        #endregion

        #region CreateUser
        public UserGetDto CreateUser(UserPostDto input)
        {
            var user = new User
            {
                Name = input.Name,
                SurName = input.SurName,
                Age = (int)input.Age,
                CharacterId = input.CharacterId
            };

            _userRepository.CreateUser(user);
            _userRepository.SaveChanges();

            return new UserGetDto
            {
                Id = user.Id,
                Name = user.Name,
                SurName = user.SurName,
                Age = user.Age,
                Character = new CharacterGetDto
                {
                    Id = user.CharacterId
                }
            };

        }
        #endregion

        #region UpdateUser
        public bool UpdateUser(UserPutDto input)
        {
            var updatedUser = new User
            {
                Id = input.Id.HasValue ? (int)input.Id : 0,
                Name = input.Name,
                SurName = input.SurName,
                Age = input.Age.HasValue ? (int)input.Age : 0,
                CharacterId = input.CharacterId.HasValue ? (int)input.CharacterId : null
            };

            if (_userRepository.UpdateUser(updatedUser))
            {
                _userRepository.SaveChanges();
                return true;
            }

            return false;

        }
        #endregion

        #region DeleteUser
        public bool DeleteUser(int userId)
        {
            if (_userRepository.DeleteUser(userId))
            {
                _userRepository.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion
    }
}
