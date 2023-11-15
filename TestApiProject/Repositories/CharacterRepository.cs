using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IQueryable<Characters> GetCharacters()
        {
            return _database.Characters.AsQueryable();
        }
        #endregion

        #region GetCharactersWithRelatedData
        public List<Characters> GetCharactersWithRelatedData()
        {
            return _database.Characters
                .Include(c => c.Users)
                .Include(c => c.CharactersSuperpowers)
                .ToList();
        }
        #endregion

        #region CreateCharacter
        public Characters CreateCharacter(Characters character)
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
            var users = _database.Users.Where(u => u.CharacterId == characterId).ToList();
            foreach(var user in users)
            {
                user.CharacterId = null;
            }
            _database.Characters.Remove(character);
            return true;
        }
        #endregion

        #region DeleteCharactersSuperPowers
        public bool DeleteCharactersSuperPowers(int characterId)
        {
            var charactersSuperPowers = _database.CharactersSuperpowersJoin
                                                 .Where(cs => cs.CharacterId == characterId);
            if (charactersSuperPowers == null)
            {
                return false;
            }
            _database.CharactersSuperpowersJoin.RemoveRange(charactersSuperPowers);
            return true;
        }
        #endregion

        #region UpdateCharacter
        public bool UpdateCharacter(Characters updatedCharacter)
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
