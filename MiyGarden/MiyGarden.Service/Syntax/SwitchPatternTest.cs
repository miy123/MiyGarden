namespace MiyGarden.Service.Syntax
{
    public class SwitchPatternTest
    {
        public record Point(int X, int Y);

        static Point Transform(Point point) => point switch
        {
            var (x, y) when x < y => new Point(-x, y),
            var (x, y) when x > y => new Point(x, -y),
            var (x, y) => new Point(x, y),
        };
    }
}
