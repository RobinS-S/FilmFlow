using FilmFlow.API.Data.Entities.Helpers;

namespace FilmFlow.API.Data.Entities
{
	public class Social : Entity
	{
		public string SocialName { get; set; }

		public string Url { get; set; }

		public string Icon { get; set; } = null!;

		public Social() { }

		public Social(string socialname, string url, string icon)
		{
			SocialName = socialname;
			Url = url;
			Icon = icon;
		}
	}

}
