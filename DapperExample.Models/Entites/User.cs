using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExample.Models.Entites
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string UserName { get; set; }

        //[Required]
        //public string? Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

    }
}
