using Dapper;
using DapperExample.DataAccess.Queries;
using DapperExample.Models.DTOs;
using DapperExample.Models.Entites;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DapperExample.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {

        #region Fields

        private readonly IConfiguration _configuration;

        #endregion


        #region Ctor

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion


        #region Methods

        public async Task<List<User>> GetAll()
        {
            using IDbConnection connection = new SqlConnection
             (_configuration.GetSection("ConnectionStrings")["DefaultConnectionStrings"]);

            return (await connection.QueryAsync<User>(Query.GetAllUsers)).ToList();
        }

        public async Task<User> GetById(int id)
        {
            using IDbConnection connection = new SqlConnection
             (_configuration.GetSection("ConnectionStrings")["DefaultConnectionStrings"]);

            return await connection.QuerySingleOrDefaultAsync<User>(Query.GetUserById, new { id });

        }

        public async Task Post(SignUpDto dto)
        {
            using IDbConnection connection = new SqlConnection
             (_configuration.GetSection("ConnectionStrings")["DefaultConnectionStrings"]);

            await connection.QueryAsync(Query.InsertUser, new { UserName = dto.UserName });
        }

        public async Task Update(int id,UpdateDto updateDto)
        {
            using IDbConnection connection = new SqlConnection
             (_configuration.GetSection("ConnectionStrings")["DefaultConnectionStrings"]);

            await connection.QueryAsync<User>(Query.UpdateUser, new { id = id, UserName = updateDto.UserName });

        }

        public async Task<User> GetUserAndUserRoles(int id, string queryName)
        {
            using IDbConnection connection = new SqlConnection
          (_configuration.GetSection("ConnectionStrings")["DefaultConnectionStrings"]);

            var userDictionary = new Dictionary<int, User>();

            return
               (await connection.QueryAsync<User, UserRole, User>
                (Query.GetUserAndRoles, map: (User, UserRole) =>
              {
                  UserRole.UserId = User.Id;


                  if (userDictionary.TryGetValue(User.Id, out User existingUser))
                  {
                      User = existingUser;
                  }
                  else
                  {
                      User.UserRoles = new List<UserRole>();
                      userDictionary.Add(User.Id, User);
                  }
                  User.UserRoles.Add(UserRole);
                  return User;

              }
               ,splitOn: "Role", param: new { Id = id })).FirstOrDefault();
        }

        #endregion

    }
}
