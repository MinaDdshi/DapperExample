

namespace DapperExample.Models.Entites
{
    public class UserRole : BaseEntity
    {
        public string Role { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
