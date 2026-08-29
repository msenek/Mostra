namespace Mostra.Application.Businesses.GetAllBusinesses
{
    public class GetAllBusinessesResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public string UniqueSlug { get; set; } = string.Empty;
    }
}