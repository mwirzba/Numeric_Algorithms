using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummationOfPowerSeries
{
    class GraphsFunctions
    {
        public void AddColumnChartToPDF(List<double> results)
        {
            // Create a new PDF document.
            PdfDocument document = new PdfDocument();

            // Create a page.
            PdfPage page = document.AddPage();

            // Generate a 2d column chart graph
            Chart chart = ColumnChart(results);

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

        private Chart ColumnChart(List<double> results)
        {
            // Set chart type to Column2D
            Chart chart = new Chart(ChartType.Column2D);

            // Add first series of column chart with name and data


            Series series = chart.SeriesCollection.AddSeries();
            series.Name = "Series 1";
            series.Add(new double[] { results[0] });

            // Add second series of column chart with name and data
            series = chart.SeriesCollection.AddSeries();
            series.Name = "Series 2";
            series.Add(new double[] { results[1] });


            // Add third series of column chart with name and data
            series = chart.SeriesCollection.AddSeries();
            series.Name = "Series 3";
            series.Add(new double[] { results[2] });

            // Add fourth series of column chart with name and data
            series = chart.SeriesCollection.AddSeries();
            series.Name = "Series 4";
            series.Add(new double[] { results[3] });

            // Set X axes
            chart.XAxis.TickLabels.Format = "00.00";
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
