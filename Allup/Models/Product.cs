using Allup.Migrations;
using Allup.Models.Base;

namespace Allup.Models
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal Tax { get; set; }
		public string Description { get; set; }
		public string SKU { get; set; }

		//relational
		public int CategoryId { get; set; }
		public int BrandId { get; set; }
		public Category Category { get; set; }
		public Brand Brand { get; set; }
		public List<ProductImage> ProductImages { get; set; }
		public List<Tag> ProductTags { get; set; }
	}
}