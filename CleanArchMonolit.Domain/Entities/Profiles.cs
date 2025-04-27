using System.ComponentModel.DataAnnotations;

namespace CleanArchMonolit.Domain.Entities
{
    public class Profiles
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string ProfileName { get; set; }
    }
}
