using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmFlow.Shared.Dto;

namespace FilmFlow.Tests
{
	public class SocialsTest
	{
		[Fact]
		public void TestSocialProperties()
		{
			var socialName = "Facebook";
			var socialUrl = "https://www.facebook.com";
			var icon = "https://www.facebook.com/avans/?locale=nl_NL";

			var socialDto = new SocialDto
			{
				SocialName = socialName,
				Url = socialUrl,
				Icon = icon
			};

			Assert.Equal(socialName, socialDto.SocialName);
			Assert.Equal(socialUrl, socialDto.Url);
			Assert.Equal(icon, socialDto.Icon);
		}
	}
}
