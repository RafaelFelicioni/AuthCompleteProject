namespace CleanArchMonolit.Domain.Auth.Entities
{
    public class UserSystemPermissions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SystemPermissionId { get; set; }
        public virtual User User { get; set; }
        public virtual SystemPermission SystemPermission { get; set; }
    }
}
