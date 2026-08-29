namespace Mostra.Application.Categories.CreateCategory
{
    public class CreateCategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BusinessId { get; set; }
    }
}