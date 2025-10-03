namespace NavQurt.Server.Domain.Entities
{
    public class ProductCategory
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// В продаже или нет
        /// </summary>
        public bool InSale { get; set; }
        public int Priority { get; set; }
        public int ProductsPerRow { get; set; }
        public string? ImageHash { get; set; }
        public string? ProductCategoryPhotoPath { get; set; }

        public int? ParentCategoryId { get; set; }
        public ProductCategory ParentCategory { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = null!;
    }
}
