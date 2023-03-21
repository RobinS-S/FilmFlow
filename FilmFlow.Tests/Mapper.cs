using AutoMapper;
using FilmFlow.Application;

namespace FilmFlow.Tests
{
	public class Mapper
	{
		public static IMapper CreateMockMapper()
		{
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MappingProfiles());
			});
			return mockMapper.CreateMapper();
		}
	}
}
