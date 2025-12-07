using System.Drawing;
using System.Drawing.Drawing2D;

namespace PerformanceReport.ImageCreator.Player
{
    public class PlayerRenderer
    {
        public static void DrawPlayer(Bitmap fieldImage, Point position, Size fieldSize,
                                    int playerNumber, Color circleColor, int circleDiameter = 30)
        {
            using (var graphics = Graphics.FromImage(fieldImage))
            {
                ConfigureGraphics(graphics);

                // Calcular la posición escalada en la imagen
                var scaledPosition = ScalePosition(position, fieldSize, fieldImage.Size);

                DrawPlayerCircle(graphics, scaledPosition, circleDiameter, circleColor);
                DrawPlayerNumber(graphics, scaledPosition, circleDiameter, playerNumber, circleColor);
            }
        }

        private static void ConfigureGraphics(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        private static Point ScalePosition(Point realPosition, Size realFieldSize, Size imageSize)
        {
            // Escalar la posición del campo real (metros) a la posición en la imagen (píxeles)
            // Considerando que la imagen tiene márgenes
            int margin = 10; // Mismo margen que usa FootballFieldRenderer
            int imageFieldWidth = imageSize.Width - (2 * margin);
            int imageFieldHeight = imageSize.Height - (2 * margin);

            float scaleX = (float)imageFieldWidth / realFieldSize.Width;
            float scaleY = (float)imageFieldHeight / realFieldSize.Height;

            // Aplicar escala y agregar margen
            int x = margin + (int)(realPosition.X * scaleX);
            int y = margin + (int)(realPosition.Y * scaleY);

            return new Point(x, y);
        }

        private static void DrawPlayerCircle(Graphics graphics, Point position,
                                           int diameter, Color color)
        {
            var circleRect = new Rectangle(
                position.X - diameter / 2,
                position.Y - diameter / 2,
                diameter,
                diameter
            );

            using (var brush = new SolidBrush(color))
            using (var pen = new Pen(Color.White, 2))
            {
                // Círculo relleno
                graphics.FillEllipse(brush, circleRect);
                // Borde blanco
                graphics.DrawEllipse(pen, circleRect);
            }
        }

        private static void DrawPlayerNumber(Graphics graphics, Point position,
                                           int circleDiameter, int number, Color circleColor)
        {
            using (var font = new Font("Arial", circleDiameter / 3, FontStyle.Bold))
            using (var brush = new SolidBrush(GetContrastColor(circleColor)))
            {
                var numberText = number.ToString();
                var textSize = graphics.MeasureString(numberText, font);

                var textPosition = new PointF(
                    position.X - textSize.Width / 2,
                    position.Y - textSize.Height / 2
                );

                graphics.DrawString(numberText, font, brush, textPosition);
            }
        }

        private static Color GetContrastColor(Color color)
        {
            // Calcular el brillo para determinar si usar blanco o negro
            double brightness = (color.R * 0.299 + color.G * 0.587 + color.B * 0.114) / 255;
            return brightness > 0.5 ? Color.Black : Color.White;
        }
    }
}