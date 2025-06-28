using System.ComponentModel.DataAnnotations;

namespace CleanArchMonolit.Domain.Auth.Entities
{
    public class SystemPermission
    {
        public int Id { get; set; }
        [MaxLength(4)]
        public string PermissionCode { get; set; }
        [MaxLength(100)]
        public string PermissionName { get; set; }
        [MaxLength(250)]
        public string PermissionDefinition { get; set; }
        public bool AdminOnly { get; set; }
        public List<UserSystemPermissions> UserPermissions { get; set; }
    }
}
