namespace Mostra.Application.Businesses.UpdateBusiness
{
    public class UpdateBusinessResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string UniqueSlug { get; set; } = string.Empty;

    }
}
