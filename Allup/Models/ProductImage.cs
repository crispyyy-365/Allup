using Allup.Models.Base;

namespace Allup.Models
{
	public class ProductImage : BaseEntity
	{
		public string Image {  get; set; }
		public bool IsPrimary { get; set; }
		public int ProductId { get; set; }
		//relational
		public Product Product { get; set; }
	}
}
