using FilmFlow.API.Data.Entities.Helpers;

namespace FilmFlow.API.Data.Entities
{
    public class Social : Entity
    {
        public string SocialName { get; set; } = null!;

        public string Url { get; set; } = null!;

        public string Icon { get; set; } = null!;

        public Social() { }

        public Social(string socialName, string url, string icon)
        {
            SocialName = socialName;
            Url = url;
            Icon = icon;
        }
    }

}
