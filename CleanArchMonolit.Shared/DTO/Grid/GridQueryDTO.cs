namespace CleanArchMonolit.Shared.DTO.Grid
{
    public class GridQueryDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } = "asc";
    }
}
