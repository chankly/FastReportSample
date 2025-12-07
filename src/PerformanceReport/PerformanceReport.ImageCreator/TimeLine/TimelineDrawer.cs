using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;

namespace PerformanceReport.ImageCreator.TimeLine
{
    public class TimelineEvent
    {
        public int TotalTime { get; set; }
        public int Minute { get; set; }
        public int ShirtNumber { get; set; }
        public byte[] Image { get; set; }
        public bool IsHome { get; set; }
    }

    public class TimelineDrawer
    {

        public Bitmap DrawTimeline(int width, List<TimelineEvent> events)
        {
            // Configuración de dimensiones
            int height = 120; // Altura ajustada
            int timelineY = 70; // Posición Y de la línea principal
            int lineHeight = 3; // Grosor de la línea
            int markerSize = 8; // Tamaño del marcador de evento

            // Crear el bitmap
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Configurar calidad del gráfico
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                // Fondo blanco
                g.Clear(Color.White);

                // Dibujar línea principal de la timeline
                using (Pen timelinePen = new Pen(Color.Black, lineHeight))
                {
                    g.DrawLine(timelinePen, 0, timelineY, width, timelineY);
                }

                // Dibujar cada evento
                foreach (var eventItem in events)
                {
                    DrawEvent(g, eventItem, width, timelineY, markerSize, height);
                }
            }

            return bitmap;
        }

        private void DrawEvent(Graphics g, TimelineEvent eventItem, int width, int timelineY, int markerSize, int totalHeight)
        {
            // Calcular posición X en la timeline basado en la proporción minuto/tiempo total
            float x = (float)eventItem.Minute / eventItem.TotalTime * width;

            // Dibujar marcador en la línea
            using (Brush markerBrush = new SolidBrush(Color.Black))
            {
                g.FillRectangle(markerBrush, new RectangleF(x - markerSize / 2, timelineY - 5, 5, 5));
            }

            // Minute
            using (Font minuteFont = new Font("Arial", 7, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.Black))
            {
                string minuteText = $"{eventItem.Minute}'";
                SizeF textSize = g.MeasureString(minuteText, minuteFont);

                //Text position
                float textX = x - textSize.Width / 2;
                float textY = eventItem.IsHome ? timelineY - markerSize - 35 : timelineY - markerSize + 35;

                g.DrawString(minuteText, minuteFont, textBrush, textX, textY);
            }

            // Shirt number
            using (Font shirtFont = new Font("Arial", 7, FontStyle.Bold))
            using (Brush shirtBrush = new SolidBrush(Color.Blue))
            {
                string shirtText = $"{eventItem.ShirtNumber}";
                SizeF shirtSize = g.MeasureString(shirtText, shirtFont);

                //Shirt number position
                float shirtX = x - shirtSize.Width / 2;
                float shirtY = eventItem.IsHome ? timelineY - markerSize - 25 : timelineY - markerSize + 25;

                g.DrawString(shirtText, shirtFont, shirtBrush, shirtX, shirtY);
            }

            // Event Icon
            if (eventItem.Image != null && eventItem.Image.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(eventItem.Image))
                    using (Image playerImage = Image.FromStream(ms))
                    {
                        // Tamaño pequeño para la imagen
                        int imageSize = 20;
                        float imageX = x - imageSize / 2; // Centrada con el marcador
                        float imageY = eventItem.IsHome ? timelineY - markerSize - 15 : timelineY - markerSize + 15; // Encima del marcador

                        g.DrawImage(playerImage, imageX, imageY, imageSize, imageSize);
                    }
                }
                catch (Exception ex)
                {
                    // En caso de error al cargar la imagen, dibujar un placeholder pequeño
                    using (Brush errorBrush = new SolidBrush(Color.LightGray))
                    using (Pen errorPen = new Pen(Color.DarkGray))
                    {
                        int imageSize = 20;
                        float imageX = x - imageSize / 2;
                        float imageY = timelineY - markerSize - 5;

                        g.FillRectangle(errorBrush, imageX, imageY, imageSize, imageSize);
                        g.DrawRectangle(errorPen, imageX, imageY, imageSize, imageSize);

                        using (Font errorFont = new Font("Arial", 5))
                        using (Brush errorTextBrush = new SolidBrush(Color.DarkGray))
                        {
                            g.DrawString("IMG", errorFont, errorTextBrush, imageX + 3, imageY + 6);
                        }
                    }
                }
            }
        }
    }
}