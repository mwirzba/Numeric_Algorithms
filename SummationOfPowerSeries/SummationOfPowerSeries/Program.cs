using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace SummationOfPowerSeries
{
    class Program
    {
        static void Main(string[] args)
        {
            Algorithm alg = new Algorithm();
            double numberOfArgs = 2000000;
            List<double> argumentsList = alg.GetArgumentsList(numberOfArgs);
            List<double> formulaResultsFromBegin =  new List<double>();
            List<double> formulaResultsFromEnd = new List<double>();

            List<double> countUsingPreviousWordResultsFromBegin = new List<double>();
            List<double> countUsingPreviousWordResultsFromEnd = new List<double>();

            foreach (var arg in argumentsList)
            {
                alg.CountUsingFormula(arg,30);
                alg.CountUsingPreviousResult(arg,30);

                formulaResultsFromBegin.Add(alg.SumFromBeginning(resultsList: alg.ResultsList));
                formulaResultsFromEnd.Add(alg.SumFromEnd(resultsList: alg.ResultsList));

                countUsingPreviousWordResultsFromBegin.Add(alg.SumFromBeginning(resultsList: alg.ResultsList2));
                countUsingPreviousWordResultsFromEnd.Add(alg.SumFromEnd(resultsList: alg.ResultsList2));
            }


            double cmpResult1 = Program.Compare(formulaResultsFromBegin, formulaResultsFromEnd,argumentsList)/numberOfArgs;
            double cmpResult2 = Program.Compare(countUsingPreviousWordResultsFromBegin,
                                    countUsingPreviousWordResultsFromEnd, argumentsList) / numberOfArgs;


            Console.WriteLine();

            double cmpResult3 = Program.Compare(formulaResultsFromBegin, countUsingPreviousWordResultsFromBegin, argumentsList) / numberOfArgs;
            double cmpResult4 = Program.Compare(formulaResultsFromEnd, countUsingPreviousWordResultsFromEnd, argumentsList) / numberOfArgs;

            AddColumnChartToPDF( cmpResult1,  cmpResult2,  cmpResult3,  cmpResult4);

            Console.ReadKey();

        }

        static int Compare(List<double> formulaResults, List<double> countUsingPreviousWordResults, List<double> args)
        {
            int numberOfMoreAccurateResults=0;
            for (int i = 0; i < formulaResults.Count; i++)
            {
                double tmp = Math.Log(args[i]);
                if ( tmp - countUsingPreviousWordResults[i] < tmp - formulaResults[i])
                {
                    numberOfMoreAccurateResults++;
                }
            }

            return numberOfMoreAccurateResults;

        }

        // ReSharper disable once InconsistentNaming
        private static void AddColumnChartToPDF(double cmpResult1, double cmpResult2, double cmpResult3, double cmpResult4)
        {
            // Create a new PDF document.
            PdfDocument document = new PdfDocument();

            // Create a page.
            PdfPage page = document.AddPage();

            // Generate a 2d column chart graph
            Chart chart = ColumnChart(cmpResult1, cmpResult2, cmpResult3, cmpResult4);

            // Create a chart frame, set chart location and size
            ChartFrame chartFrame = new ChartFrame();
            chartFrame.Location = new XPoint(30, 30);
            chartFrame.Size = new XSize(500, 600);
            chartFrame.Add(chart);

            // Render chart symbols into pdf page
            XGraphics g = XGraphics.FromPdfPage(page);
            chartFrame.Draw(g);

            // Save and show the document           
            document.Save("ColumnChart.pdf");
            Process.Start("ColumnChart.pdf");
        }

        static Chart ColumnChart(double cmpResult1, double cmpResult2, double cmpResult3, double cmpResult4)
        {
            // Set chart type to Column2D
            Chart chart = new Chart(ChartType.Column2D);

            // Add first series of column chart with name and data
            Series series = chart.SeriesCollection.AddSeries();
            series.Name = "Series 1";
            series.Add(new double[] { cmpResult1 });

            // Add second series of column chart with name and data
            series = chart.SeriesCollection.AddSeries();
            series.Name = "Series 2";
            series.Add(new double[] { cmpResult2 });

            // Add third series of column chart with name and data
            series = chart.SeriesCollection.AddSeries();
            series.Name = "Series 3";
            series.Add(new double[] { cmpResult3 });

            // Add fourth series of column chart with name and data
            series = chart.SeriesCollection.AddSeries();
            series.Name = "Series 4";
            series.Add(new double[] { cmpResult4 });

            // Set X axes
            chart.XAxis.TickLabels.Format = "00";
            chart.XAxis.MajorTickMark = TickMarkType.Outside;
            chart.XAxis.Title.Caption = "";

            // Set Y axes
            chart.YAxis.MajorTickMark = TickMarkType.Outside;
            chart.YAxis.HasMajorGridlines = true;

            // Set plot area (chart diagram)
            chart.PlotArea.LineFormat.Color = XColors.DarkGray;
            chart.PlotArea.LineFormat.Width = 1;
            chart.PlotArea.LineFormat.Visible = true;

            // Set legend
            chart.Legend.Docking = DockingType.Right;

            chart.DataLabel.Type = DataLabelType.Value;
            chart.DataLabel.Position = DataLabelPosition.OutsideEnd;

            return chart;
        }

    }
}
