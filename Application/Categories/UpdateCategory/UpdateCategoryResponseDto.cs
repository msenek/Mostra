namespace Mostra.Application.Categories.UpdateCategory
{
    public class UpdateCategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BusinessId { get; set; }
    }
}