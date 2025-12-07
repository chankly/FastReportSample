using PerformanceReport.ImageCreator.Player;
using System.Drawing.Drawing2D;

namespace PerformanceReport.ImageCreator
{
    public class FootballFieldRenderer
    {
        private readonly FieldDimensions _dimensions;
        private readonly FieldColors _colors;
        private readonly Size _realFieldSize;// Standard football field size in meters

        /// <summary>
        /// Initializes a new instance of the <see cref="FootballFieldRenderer"/> class with specified image size and
        /// colors.
        /// </summary>
        /// <param name="imageSize">The size of the image to render, representing the dimensions of the football field.</param>
        /// <param name="grass">The color used to render the grass on the football field.</param>
        /// <param name="lines">The color used to render the lines on the football field.</param>
        public FootballFieldRenderer(Size imageSize, Color grass, Color lines)
        {            
            _dimensions = new FieldDimensions(imageSize);
            _colors = new FieldColors(grass, lines);
        }

        public FootballFieldRenderer(Size imageSize, Color grass, Color lines, Size realFieldSize)
        {
            _dimensions = new FieldDimensions(imageSize);
            _colors = new FieldColors(grass, lines);
            _realFieldSize = realFieldSize;
        }

        public Bitmap GenerateField()
        {
            var bitmap = new Bitmap(_dimensions.Width, _dimensions.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                ConfigureGraphics(graphics);
                DrawField(graphics);
            }

            return bitmap;
        }

        public Bitmap GenerateField(List<(Point playerPosition, int shirtNumber, Color circleColor, int circleDiameter)> players)
        {
            var bitmap = new Bitmap(_dimensions.Width, _dimensions.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                ConfigureGraphics(graphics);
                DrawField(graphics);
            }

            foreach (var player in players)
            {
                PlayerRenderer.DrawPlayer(
                    bitmap,
                    player.playerPosition,
                    _realFieldSize,
                    player.shirtNumber,
                    player.circleColor,
                    player.circleDiameter);
            }

            return bitmap;
        }

        private void ConfigureGraphics(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        private void DrawField(Graphics graphics)
        {
            DrawBackground(graphics);
            DrawOuterField(graphics);
            DrawCenterLineAndCircle(graphics);
            DrawGoalAreas(graphics);
            DrawPenaltyAreas(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            using (var brush = new SolidBrush(_colors.GrassGreen))
            {
                graphics.FillRectangle(brush, 0, 0, _dimensions.Width, _dimensions.Height);
            }
        }

        private void DrawOuterField(Graphics graphics)
        {
            using (var pen = new Pen(_colors.White, _dimensions.LineWidth))
            {
                var fieldRect = new Rectangle(
                    _dimensions.Margin,
                    _dimensions.Margin,
                    _dimensions.FieldWidth,
                    _dimensions.FieldHeight
                );

                graphics.DrawRectangle(pen, fieldRect);
            }
        }

        private void DrawCenterLineAndCircle(Graphics graphics)
        {
            using (var pen = new Pen(_colors.White, _dimensions.LineWidth))
            {
                // Línea central
                var centerX = _dimensions.Width / 2;
                graphics.DrawLine(pen, centerX, _dimensions.Margin, centerX, _dimensions.Margin + _dimensions.FieldHeight);

                // Círculo central
                var circleDiameter = _dimensions.FieldWidth / 4;
                var circleX = centerX - circleDiameter / 2;
                var circleY = (_dimensions.Height - circleDiameter) / 2;

                graphics.DrawEllipse(pen, circleX, circleY, circleDiameter, circleDiameter);

                // Punto central
                DrawCenterPoint(graphics, centerX, _dimensions.Height / 2);
            }
        }

        private void DrawCenterPoint(Graphics graphics, int x, int y)
        {
            using (var brush = new SolidBrush(_colors.White))
            {
                var pointSize = _dimensions.LineWidth * 3;
                graphics.FillEllipse(brush, x - pointSize / 2, y - pointSize / 2, pointSize, pointSize);
            }
        }

        private void DrawGoalAreas(Graphics graphics)
        {
            using (var pen = new Pen(_colors.White, _dimensions.LineWidth))
            {
                // Área izquierda
                var leftGoalArea = new Rectangle(
                    _dimensions.Margin,
                    (_dimensions.Height - _dimensions.GoalAreaHeight) / 2,
                    _dimensions.GoalAreaWidth,
                    _dimensions.GoalAreaHeight
                );
                graphics.DrawRectangle(pen, leftGoalArea);

                // Área derecha
                var rightGoalArea = new Rectangle(
                    _dimensions.Margin + _dimensions.FieldWidth - _dimensions.GoalAreaWidth,
                    (_dimensions.Height - _dimensions.GoalAreaHeight) / 2,
                    _dimensions.GoalAreaWidth,
                    _dimensions.GoalAreaHeight
                );
                graphics.DrawRectangle(pen, rightGoalArea);
            }
        }

        private void DrawPenaltyAreas(Graphics graphics)
        {
            using (var pen = new Pen(_colors.White, _dimensions.LineWidth))
            {
                // Área de penal izquierda
                var leftPenaltyArea = new Rectangle(
                    _dimensions.Margin,
                    (_dimensions.Height - _dimensions.PenaltyAreaHeight) / 2,
                    _dimensions.PenaltyAreaWidth,
                    _dimensions.PenaltyAreaHeight
                );
                graphics.DrawRectangle(pen, leftPenaltyArea);

                // Área de penal derecha
                var rightPenaltyArea = new Rectangle(
                    _dimensions.Margin + _dimensions.FieldWidth - _dimensions.PenaltyAreaWidth,
                    (_dimensions.Height - _dimensions.PenaltyAreaHeight) / 2,
                    _dimensions.PenaltyAreaWidth,
                    _dimensions.PenaltyAreaHeight
                );
                graphics.DrawRectangle(pen, rightPenaltyArea);

                // Puntos de penal
                DrawPenaltyPoints(graphics);
            }
        }

        private void DrawPenaltyPoints(Graphics graphics)
        {
            using (var brush = new SolidBrush(_colors.White))
            {
                var pointSize = _dimensions.LineWidth * 3;

                // Punto izquierdo
                var leftPenaltyPointX = _dimensions.Margin + _dimensions.PenaltyPointDistance;
                var leftPenaltyPointY = _dimensions.Height / 2;
                graphics.FillEllipse(brush,
                    leftPenaltyPointX - pointSize / 2,
                    leftPenaltyPointY - pointSize / 2,
                    pointSize, pointSize);

                // Punto derecho
                var rightPenaltyPointX = _dimensions.Margin + _dimensions.FieldWidth - _dimensions.PenaltyPointDistance;
                var rightPenaltyPointY = _dimensions.Height / 2;
                graphics.FillEllipse(brush,
                    rightPenaltyPointX - pointSize / 2,
                    rightPenaltyPointY - pointSize / 2,
                    pointSize, pointSize);
            }
        }
    }
}
