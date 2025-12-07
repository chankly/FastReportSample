namespace PerformanceReport.Models.TeamPostMatch
{
    public class PostTeamMatchReportModel
    {
        public MatchTeamData HomeTeam { get; set; }
        public MatchTeamData AwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
        public List<Metric> IntegerMetrics { get; set; }
        public List<Metric> DecimalMetrics { get; set; }
    }

    public class MatchTeamData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public byte[] Logo { get; set; }
    }

    public class Metric
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
