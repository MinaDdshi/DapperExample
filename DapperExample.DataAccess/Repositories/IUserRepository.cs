using DapperExample.Models.DTOs;
using DapperExample.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExample.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();

        Task<User> GetById(int id);

        Task Post(SignUpDto dto);

        Task Update(int id,UpdateDto updateDto);

        Task<User> GetUserAndUserRoles(int id, string queryName);
    }
}
