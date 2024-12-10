namespace Allup.Areas.Admin.ViewModels.Slides
{
	public class CreateSlideVM
	{
		public string Button { get; set; }
		public string ProductName { get; set; }
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
		public IFormFile Photo { get; set; }
	}
}
