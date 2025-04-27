using System.ComponentModel.DataAnnotations;

namespace CleanArchMonolit.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Username { get; set; } = null!;
        [MaxLength(500)]
        public string Mail { get; set; }
        [MaxLength(800)]
        public string PasswordHash { get; set; } = null!;
        public int ProfileId { get; set; }
        public Profiles Profile { get; set; } = null!;
    }
}
