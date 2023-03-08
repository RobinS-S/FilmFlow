using FilmFlow.Shared.Enums;

namespace FilmFlow.API.Misc
{
    public class TarriffTypes
    {
        // TODO: use information from movie and cinemahall and show to see if 3d
        public static decimal GetTarriffPrice(TarriffType type)
        {
            switch (type)
            {
                case TarriffType.NORMAL:
                    return 1m;
                case TarriffType.CHILDREN:
                    break;
                case TarriffType.STUDENTS:
                    break;
                case TarriffType.SENIORS:
                    break;
                case TarriffType.GIFTCARD:
                    break;
                case TarriffType.THREEDIMENSIONAL:
                    break;
            }

            return 1m;
        }
    }
}
