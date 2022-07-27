using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CSharpUtils
{
    public class FileHandling
    {
        public static class CSV
        {
            /// <summary>
            /// Uses System.Xml.Linq to build an XML tree from a CSV file, and saves it to an XML file.
            /// </summary>
            /// <param name="inputFilePath">A valid path to a CSV file.</param>
            /// <param name="outputFilePath">A valid directory to output the XML file to.</param>
            public static void ToXml(string inputFilePath, string outputFilePath)
            {
                if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
                {
                    Console.WriteLine("Invalid path provided.");
                    return;
                }
                Console.WriteLine("Converting to XML...");
                var lines = File.ReadAllLines(inputFilePath);
                var xmlTree = new XElement("CSV");
                AddContentForEachLine(lines[0], ref xmlTree, "Header");
                foreach (var line in lines.Skip(1))
                    AddContentForEachLine(line, ref xmlTree, "Row");

                File.WriteAllText(outputFilePath + "output.xml", xmlTree.ToString());
                Console.WriteLine($"output.xml created in {outputFilePath}.");
            }

            /// <summary>
            /// Adds a column element to the tree.
            /// </summary>
            /// <param name="line">A valid row of CSV values.</param>
            /// <param name="xmlTree">A reference to the XML tree being built.</param>
            private static void AddContentForEachLine(string line, ref XElement xmlTree, string elementName)
            {
                if (string.IsNullOrWhiteSpace(line)) return;
                var currentTree = new XElement("Line");
                string[] slices = line.Split(",");
                for (int i = 0; i < slices.Length; i++)
                    currentTree.Add(new XElement($"{elementName}{i}", slices[i].ToString().Trim()));
                xmlTree.Add(currentTree);
            }
        }
    }
}