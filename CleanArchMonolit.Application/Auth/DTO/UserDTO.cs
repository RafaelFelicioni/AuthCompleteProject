using CleanArchMonolit.Domain.Auth.Entities;

namespace CleanArchMonolit.Application.Auth.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTaxId { get; set; }
        public string UserName { get; set; }
        public string ProfileName { get; set; }
        public int ProfileId { get; set; }

        public static implicit operator UserDTO(User entity)
        {
            if (entity == null) return null;
            return new UserDTO
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                UserName = entity.Username,
                ProfileId = entity.ProfileId,
                ProfileName = entity.Profile.ProfileName,
            };
        }
    }
}
