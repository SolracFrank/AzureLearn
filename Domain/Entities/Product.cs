namespace Domain.Entities;

public partial class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool Active { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}