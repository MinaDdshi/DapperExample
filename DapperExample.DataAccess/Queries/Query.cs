using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExample.DataAccess.Queries
{
    public static class Query
    {
        public const string GetUserAndRoles =
             @"select Users.Id,Users.UserName,UserRoles.Role from Users
             left join UserRoles on Users.Id = UserRoles.UserId
             where Users.Id=@Id";

        public const string GetAllUsers =
             @"SELECT * FROM dbo.[Users];";

        public const string GetUserById =
             @"SELECT * FROM dbo.[Users] WHERE Id=@id";


        public const string InsertUser =
              @"INSERT INTO dbo.[Users]
              (UserName, CreatedAt, UpdateAt)
              VALUES(@UserName, GETDATE(), GETDATE())";

        public const string UpdateUser =
              @"Update dbo.[Users]
	           Set UserName=@UserName , UpdateAt = GETDATE()
	           WHERE Id =@Id ";
    }
}
