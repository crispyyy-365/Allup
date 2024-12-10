using Allup.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Allup.Models
{
	public class Tag : BaseEntity
	{
		[Required]
		[MaxLength(30, ErrorMessage = "Max 30 characters !")]
		public string Name { get; set; }
		//relational
		public List<ProductTag> productTags { get; set; }
	}
}
