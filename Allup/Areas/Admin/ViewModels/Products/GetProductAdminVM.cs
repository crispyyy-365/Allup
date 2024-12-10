namespace Allup.Areas.Admin.ViewModels.Products
{
	public class GetProductAdminVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal? Tax { get; set; }
		public string CategoryName { get; set; }
		public string BrandName { get; set; }
		public string Image { get; set; }
	}
}
