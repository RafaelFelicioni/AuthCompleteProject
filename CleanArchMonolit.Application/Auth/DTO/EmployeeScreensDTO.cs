using CleanArchMonolit.Domain.Auth.Enums;

namespace CleanArchMonolit.Application.Auth.DTO
{
    public class EmployeeScreensDTO
    {
        public int UserId { get; set; }
        public List<EmployessListScreensDTO> Screens { get; set; } = new();
    }

    public class EmployessListScreensDTO
    {
        public ScreenEnum Screen { get; set; }
        public bool Active { get; set; }
    }
}
