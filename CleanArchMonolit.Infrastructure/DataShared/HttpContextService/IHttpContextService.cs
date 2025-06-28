namespace CleanArchMonolit.Infrastructure.DataShared.HttpContextService
{
    public interface IHttpContextService
    {
        public int UserId { get; }
        public int CompanyId { get; }
        public int ProfileId { get; }
        public string ProfileName { get; }
        public string UserName { get; }
        public bool IsAdmin { get; }
    }
}
