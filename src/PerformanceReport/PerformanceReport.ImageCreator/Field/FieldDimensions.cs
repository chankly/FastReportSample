namespace PerformanceReport.ImageCreator
{
    public class FieldDimensions
    {
        public FieldDimensions(Size size)
        {
            Width = size.Width;
            Height = size.Height;
        }

        public int Width { get; } = 355;
        public int Height { get; } = 235;
        public int Margin { get; } = 10;
        public int LineWidth { get; } = 2;

        public int FieldWidth => Width - (2 * Margin);
        public int FieldHeight => Height - (2 * Margin);

        // Dimensiones de las áreas (proporcionales al campo)
        public int GoalAreaWidth => FieldWidth / 10;
        public int GoalAreaHeight => FieldHeight / 3;

        public int PenaltyAreaWidth => FieldWidth / 5;
        public int PenaltyAreaHeight => FieldHeight / 2;

        public int PenaltyPointDistance => PenaltyAreaWidth;
    }
}
