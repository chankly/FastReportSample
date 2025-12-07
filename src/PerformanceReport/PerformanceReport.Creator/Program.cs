// See https://aka.ms/new-console-template for more information
using FastReport;
using FastReport.Data;
using PerformanceReport.Models.TeamPostMatch;

Console.WriteLine("Hello, World!");
var report = new Report();
report.Dictionary.RegisterBusinessObject(
              new List<PostTeamMatchReportModel>(), // a (empty) list of objects
              "Matches",          // name of dataset
              2,                   // depth of navigation into properties
              true                 // enable data source
       );

report.Dictionary.Parameters.Add(new Parameter(""));

report.Save(System.IO.Path.Combine(AppContext.BaseDirectory, @"TeamPostMatch.frx"));