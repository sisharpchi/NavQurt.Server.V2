namespace NavQurt.Server.Domain.Entities;

public class Product
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public int Priority { get; set; }
    public decimal SelfPrice { get; set; }
    public decimal Price { get; set; }
    /// <summary>
    /// В продаже или нет
    /// </summary>
    public bool InSale { get; set; }
    public bool IsCombo { get; set; }
    public string? ProductPhotoPath { get; set; }
    public string? ImageHash { get; set; }
    
    public int ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; } = null!;

    public int? WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;
}
