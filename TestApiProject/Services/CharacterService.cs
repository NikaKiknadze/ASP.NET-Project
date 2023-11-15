using Microsoft.EntityFrameworkCore;
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
        private readonly UserService _userService;
        private readonly TestDbContext _database;
        private readonly CharacterRepository _characterRepository;
        private readonly UserRepository _userRepository;


        public CharacterService(CharacterRepository characterRepository, UserRepository userRepository, UserService userService, TestDbContext database)
        {
            _characterRepository = characterRepository;
            _userRepository = userRepository;
            _database = database;
            _userService = userService;
        }


        #region GetCharacters
        public List<CharacterGetDto> GetCharacters()
        {
            var characters = _characterRepository.GetCharactersWithRelatedData();

            var characterDtos = characters.Select(character => new CharacterGetDto
            {
                Id = character.Id,
                CharacterName = character.CharacterName,
                CharacterHealth = character.CharacterHealth,
                CharacterType = character.CharacterType,
                SuperPowerIds = character.CharactersSuperpowers?.Select(s => s.SuperPowerId).ToList() ?? new List<int>(),
                UserIds = character.Users?.Select(u => u.Id).ToList() ?? new List<int>()
            }).ToList();

            return characterDtos;
        }

        #endregion

        #region CreateCharacter
        public CharacterGetDto CreateCharacter(CharacterPostDto input)
        {
            var character = new Characters
            {
                CharacterName = input.CharacterName,
                CharacterHealth = (int)input.CharacterHealth,
                CharacterType = input.CharacterType,
                CharactersSuperpowers = new List<CharactersSuperpowersJoin>(),
                Users = new List<User>()
            };
            _characterRepository.CreateCharacter(character);
            _characterRepository.SaveChanges();
            if (!input.SuperPowerIds.IsNullOrEmpty())
            {
                character.CharactersSuperpowers = new List<CharactersSuperpowersJoin>();

                foreach (var superPowerId in input.SuperPowerIds)
                {
                    character.CharactersSuperpowers.Add(new CharactersSuperpowersJoin
                    {
                        SuperPowerId = superPowerId,
                        CharacterId = character.Id
                    });
                }
            }

            if (!input.UserIds.IsNullOrEmpty())
            {
                foreach (var userId in input.UserIds)
                {
                    var user = _userRepository.GetUserById(userId);

                    if (user != null)
                    {
                        user.CharacterId = character.Id;
                        character.Users.Add(user);
                        _userRepository.SaveChanges();
                    }

                }

            }

            return new CharacterGetDto
            {
                Id = character.Id,
                CharacterName = character.CharacterName,
                CharacterHealth = character.CharacterHealth,
                CharacterType = character.CharacterType,
                SuperPowerIds = character.CharactersSuperpowers.Select(s => s.SuperPowerId).ToList(),
                UserIds = character.Users.Select(u => u.Id).ToList()
            };
        }
        #endregion
        //შესამოწმებელია
        #region UpdateCharacter
        public bool UpdateCharacter(CharacterPutDto input)
        {
            var character = _characterRepository.GetCharacters()
                                                .Include(s => s.CharactersSuperpowers)
                                                .Include(u => u.Users)
                                                .Where(ch => ch.Id == input.Id)
                                                .FirstOrDefault();

            character.Id = input.Id.HasValue ? (int)input.Id : 0;
            character.CharacterName = input.CharacterName;
            character.CharacterType = input.CharacterType;
            character.CharacterHealth = input.CharacterHealth.HasValue ? (int)input.CharacterHealth : 0;


            if (_characterRepository.UpdateCharacter(character))
            {

                if (!input.SuperPowerIDs.IsNullOrEmpty())
                {
                    foreach (var updatedSuperPowerId in input.SuperPowerIDs)
                    {
                        character.CharactersSuperpowers.Clear();
                        character.CharactersSuperpowers.Add(new CharactersSuperpowersJoin
                        {
                            SuperPowerId = updatedSuperPowerId,
                            CharacterId = character.Id
                        });
                    }
                }

                if (!input.UserIds.IsNullOrEmpty())
                {
                    character.Users.Clear();
                    foreach (var userId in input.UserIds)
                    {
                        var user = _userRepository.GetUserById(userId);
                        character.Users.Add(user);
                    }
                }
                _characterRepository.SaveChanges();
                return true;
            }



            return false;
        }
        #endregion

        #region DeleteCharacter
        public bool DeleteCharacter(int characterId)
        {
            if (_characterRepository.DeleteCharacter(characterId) &&
                _characterRepository.DeleteCharactersSuperPowers(characterId))
            {
                _characterRepository.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion



    }
}
