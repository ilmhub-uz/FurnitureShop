namespace FurnitureShop.Client.Blazor.Dtos
{
    public class CategoryView
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<CategoryView>? Children { get; set; }
    }
}
