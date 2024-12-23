﻿using Allup.Models.Base;

namespace Allup.Models
{
	public class Slide : BaseEntity
	{
		public string Button { get; set; }
		public string ProductName { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public int Order { get; set; }
	}
}
