using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using collectionsapi.Data.Entities;
using collectionsapi.Repositories;
using collectionsapi.Services;
using Newtonsoft.Json.Linq;

namespace collectionsapi.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IUserRepository _userRepository;

        public CollectionService(ICollectionRepository collectionRepository, IUserRepository userRepository)
        {
            _collectionRepository = collectionRepository;
            _userRepository = userRepository;
        }

        public async Task<Collection> CreateCollection(Collection collection, string requestToken)
        {
            collection.CreatedAt = DateTime.UtcNow;
            var uniqueName = GetUniqueNameFromToken(requestToken);
            var userId = await GetUserIdFromUniqueName(uniqueName);

            collection.UserId = userId.Value;

            await _collectionRepository.AddAsync(collection);
            return collection;
        }

        public async Task<Collection> GetCollection(int id, string requestToken)
        {
            var uniqueName = GetUniqueNameFromToken(requestToken);
            var userId = await GetUserIdFromUniqueName(uniqueName);

            var collection = await _collectionRepository.GetByIdAsync(id);

            // Check if the collection exists and belongs to the specified user
            if (collection != null && collection.UserId == userId)
            {
                return collection;
            }
            else
            {
                return null;
                // Collection not found or does not belong to the user
            }
        }


        public async Task<List<Collection>> GetAllCollections(string requestToken)
        {
            var uniqueName = GetUniqueNameFromToken(requestToken);
            int userId = (int)await GetUserIdFromUniqueName(uniqueName);
            return await _collectionRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<Collection> UpdateCollection(Collection collection, string requestToken)
        {
            var uniqueName = GetUniqueNameFromToken(requestToken);
            int userId = (int)await GetUserIdFromUniqueName(uniqueName);

            if (collection != null && collection.UserId == userId)
            {
                await _collectionRepository.UpdateAsync(collection);
            }
            else
            {
                throw new InvalidOperationException("Is not your collection or doesn't exists");
            }
            return collection;
        }

        public async Task DeleteCollection(int id, string requestToken)
        {
            var uniqueName = GetUniqueNameFromToken(requestToken);
            var userId = await GetUserIdFromUniqueName(uniqueName);

            var collection = await _collectionRepository.GetByIdAsync(id);

            // Check if the collection exists and belongs to the specified user
            if (collection != null && collection.UserId == userId)
            {
                await _collectionRepository.DeleteAsync(id);
            }
            else
            {
                throw new InvalidOperationException("Is not your collection or doesn't exists");
            }
        }

        private string GetUniqueNameFromToken(string requestToken)
        {
            var jwtToken = requestToken.Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken).ToString().Split('.')[1];
            var jsonObject = JObject.Parse(token);
            return (string)jsonObject["unique_name"];
        }

        private async Task<int?> GetUserIdFromUniqueName(string uniqueName)
        {
            var user = await _userRepository.GetUserByUsernameAsync(uniqueName);
            return user?.Id;
        }
    }
}
