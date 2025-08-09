using CleanArchMonolit.Shared.DTO.Grid;

namespace CleanArchMonolit.Application.Auth.DTO
{
    public class GetUsersGrid : GridQueryDTO
    {
        public int? UserId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProfileId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTaxId { get; set; }
    }
}
