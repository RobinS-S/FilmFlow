using FilmFlow.Application;
using FilmFlow.Domain.Entities;
using FilmFlow.Shared.Dto;

namespace FilmFlow.Tests
{
    public class SocialsMappingTest
    {
        [Fact]
        public void TestSocialPropertyMapping()
        {
	        var mapper = Mapper.CreateMockMapper();

            const string socialName = "Facebook";
            const string socialUrl = "https://www.facebook.com";
            const string icon = "https://www.facebook.com/avans/?locale=nl_NL";

            var socialEntity = new Social(socialName, socialUrl, icon);
            var socialDto = mapper.Map<SocialDto>(socialEntity);

            Assert.Equal(socialName, socialDto.SocialName);
            Assert.Equal(socialUrl, socialDto.Url);
            Assert.Equal(icon, socialDto.Icon);
        }
    }
}
