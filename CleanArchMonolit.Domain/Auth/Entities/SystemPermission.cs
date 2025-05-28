using System.ComponentModel.DataAnnotations;

namespace CleanArchMonolit.Domain.Auth.Entities
{
    public class SystemPermission
    {
        public int Id { get; set; }
        [MaxLength(3)]
        public string PermissionCode { get; set; }
        [MaxLength(50)]
        public string PermissionName { get; set; }

        public ICollection<UserSystemPermissions> UserPermissions { get; set; }
    }
}
