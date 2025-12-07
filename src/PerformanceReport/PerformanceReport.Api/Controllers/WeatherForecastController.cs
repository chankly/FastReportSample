using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Mvc;
using PerformanceReport.Models.TeamPostMatch;

namespace PerformanceReport.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var report = new Report();
                var reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "TeamPostMatch.frx");
                report.Load(reportPath);

                report.RegisterData(new[] { GetData(_webHostEnvironment.ContentRootPath) }, "Matches");
                report.SetParameterValue("MyParameter", "pruebaaaa");

                await report.PrepareAsync();

                var memoryStream = new MemoryStream();
                new PDFSimpleExport().Export(report, memoryStream);
                memoryStream.Position = 0;
                return File(memoryStream, "application/pdf", "reporte.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating report: {ex.Message}");
            }
        }

        private PostTeamMatchReportModel GetData(string rootPath)
        {
            return new PostTeamMatchReportModel
            {
                HomeTeam = new MatchTeamData
                {
                    Id = Guid.NewGuid(),
                    Name = "Home Team",
                    Score = 2,
                    Logo = System.IO.File.ReadAllBytes(System.IO.Path.Combine(rootPath, "Resources", "Reporting", "3a1b1b34-3ac5-9932-31de-984c924b9d7b.png"))
                },
                AwayTeam = new MatchTeamData
                {
                    Id = Guid.NewGuid(),
                    Name = "Away Team",
                    Score = 0,
                    Logo = System.IO.File.ReadAllBytes(System.IO.Path.Combine(rootPath, "Resources", "Reporting", "3a1b1b34-3ac5-9932-31de-984c924b9d7b.png"))
                },
                MatchDate = DateTime.Now,
                DecimalMetrics = new List<Metric>(),
                IntegerMetrics = new List<Metric>()
            };
        }
    }
}
