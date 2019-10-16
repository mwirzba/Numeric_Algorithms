using System;
using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SummationOfPowerSeries
{
    class GraphsFunctions
    {
        public void WriteResultsWithNumbersOfElementsToExelFile(List<Result> results)
        {
            using (StreamWriter file =  File.AppendText(Directory.GetCurrentDirectory() + "//results2.csv"))
            {
                 file.WriteLine("Number of added elements;Results");
                foreach (var result in results)
                {
                    if (result.NumberOfAddedWords%100!=0)
                    {
                        file.WriteLine("" + ";" + result.AlgorithmResult);
                    }
                    else
                    {
                        file.WriteLine(result.NumberOfAddedWords + ";" + result.AlgorithmResult);
                    }
                   
                }
                file.WriteLine();  
            }
        }

        public void WriteResultsToExcelFile(List<Result> results)
        {
            using (StreamWriter file = File.AppendText(Directory.GetCurrentDirectory() + "//results1.csv"))
            {
                file.WriteLine("Args;Results");
                foreach (var result in results)
                {
                    file.WriteLine(result.NumberOfAddedWords + ";" + result.AlgorithmResult);
                }
                file.WriteLine();
            }
        }
    }
}
