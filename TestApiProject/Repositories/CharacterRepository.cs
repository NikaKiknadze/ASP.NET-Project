using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using TestApiProject.Data;
using TestApiProject.Entities;

namespace TestApiProject.Repositories
{
    public class CharacterRepository
    {
        private readonly TestDbContext _database;
        public CharacterRepository(TestDbContext database)
        {
            _database = database;
        }
        #region SaveChanges
        public void SaveChanges()
        {
            _database.SaveChanges();
        }
        #endregion

        #region GetCharacters
        public IQueryable<Character> GetCharacters()
        {
            return _database.Characters.AsQueryable();
        }
        #endregion

        #region CreateCharacter
        public Character CreateCharacter(Character character)
        {
            _database.Characters.Add(character);
            return character;
        }
        #endregion

        #region DeleteCharacter
        public bool DeleteCharacter(int characterId)
        {
            var character = _database.Characters.FirstOrDefault(f => f.Id == characterId);
            if (character == null)
            {
                return false;
            }
            _database.Characters.Remove(character);
            return true;
        }
        #endregion

        #region UpdateCharacter
        public bool UpdateCharacter(Character updatedCharacter)
        {
            var existingCharacter = _database.Characters.FirstOrDefault(f => f.Id == updatedCharacter.Id);
            if (existingCharacter == null)
            {
                return false;
            }

            existingCharacter.CharacterName = updatedCharacter.CharacterName;
            existingCharacter.CharacterHealth = (int)updatedCharacter.CharacterHealth;
            existingCharacter.CharacterType = updatedCharacter.CharacterType;
            return true;
        }
        #endregion
    }
}
