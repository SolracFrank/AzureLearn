namespace Domain.Entities;

public partial class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Active { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}