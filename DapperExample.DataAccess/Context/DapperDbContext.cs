using DapperExample.Models.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace DapperExample.DataAccess.Context
{
    public class SqlDataAccess : DbContext
    {


        public SqlDataAccess(DbContextOptions<SqlDataAccess> dbContextOptions) : base(dbContextOptions)
        {

        }


        public DbSet<User> Users { get; set; }


        public DbSet<UserRole> UserRoles { get; set; }
    }
}
