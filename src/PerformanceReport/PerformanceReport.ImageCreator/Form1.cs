using PerformanceReport.ImageCreator.Player;
using PerformanceReport.ImageCreator.TimeLine;

namespace PerformanceReport.ImageCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var image = DrawWithPlayers();
            //var image = DrawTimeLine();
            pbCreatedImage.Image = image;
        }

        private Bitmap DrawTimeLine()
        {
            
            // Crear algunos eventos de ejemplo
            var events = new List<TimelineEvent>
            {
                new TimelineEvent
                {
                    TotalTime = 98,
                    Minute = 23,
                    ShirtNumber = 10,
                    Image = File.ReadAllBytes(System.IO.Path.Combine(AppContext.BaseDirectory, "Images","player10.png")), // Ruta a imagen real
                    IsHome = true
                },
                new TimelineEvent
                {
                    TotalTime = 98,
                    Minute = 45,
                    ShirtNumber = 7,
                    Image = File.ReadAllBytes(System.IO.Path.Combine(AppContext.BaseDirectory, "Images","player7.png")) // Ruta a imagen real
                },
                new TimelineEvent
                {
                    TotalTime = 98,
                    Minute = 67,
                    ShirtNumber = 9,
                    Image = null // Sin imagen
                }
            };

            // Crear el drawer y generar la timeline
            var timeLineWidth = 400;
            var drawer = new TimelineDrawer();
            return drawer.DrawTimeline(timeLineWidth, events);
        }

        private Bitmap DrawWithPlayers()
        {
            var IMAGE_SIZE = new Size(355, 235); //pixels
            var FIELD_SIZE = new Size(105, 68); //meters

            var players = new List<(Point playerPosition, int shirtNumber, Color circleColor, int circleDiameter)>
            {
                (new Point(67, 33), 1, Color.Blue, 30),
                (new Point(23, 30), 4, Color.Blue, 30),
                (new Point(89, 54), 5, Color.Blue, 30),
                (new Point(103, 12), 9, Color.Red, 30),
                (new Point(45, 64), 7, Color.Red, 30)
            };

            var renderer = new FootballFieldRenderer(IMAGE_SIZE, Color.LightGreen, Color.White, FIELD_SIZE);
            return renderer.GenerateField(players);
        }

        private Bitmap NormalImage()
        {
            var IMAGE_SIZE = new Size(355, 235); //pixels
            var FIELD_SIZE = new Size(105, 68); //meters
            var SHIRT_NUMBER = 23;
            var CIRCLE_PLAYER_COLOR = Color.Red;
            var CIRCLE_PLAYER_DIAMETER = 30;

            var renderer = new FootballFieldRenderer(IMAGE_SIZE, Color.LightGreen, Color.White);
            var fieldImage = renderer.GenerateField();

            PlayerRenderer.DrawPlayer(fieldImage, new Point(89, 54), FIELD_SIZE, SHIRT_NUMBER, CIRCLE_PLAYER_COLOR, CIRCLE_PLAYER_DIAMETER);

            return fieldImage;
        }
    }
}
