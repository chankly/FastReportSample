namespace PerformanceReport.ImageCreator
{
    public class FieldColors
    {
        public Color GrassGreen { get; } = Color.FromArgb(76, 175, 80);
        public Color White { get; } = Color.White;

        public FieldColors(Color grass, Color line)
        {
            GrassGreen = grass;
            White = line;
        }
    }
}
