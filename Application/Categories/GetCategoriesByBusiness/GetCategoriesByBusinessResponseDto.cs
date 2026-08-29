namespace Mostra.Application.Categories.GetCategoriesByBusiness
{
    public class GetCategoriesByBusinessResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}