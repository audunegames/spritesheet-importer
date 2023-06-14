using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Audune.Spritesheet.Utils
{
  // Class that defines a CSV reader
  public sealed class CSVReader
  {
    // The split pattern to use when reading the CSV data
    private static readonly Regex splitPattern = new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))", RegexOptions.Compiled);

    // The characters to trim off the values when reading the CSV data
    private static readonly char[] trimChars = { '\"' };


    // Read from a text reader
    public static IEnumerable<CSVRecord> Read(TextReader textReader)
    {
      var headerLine = textReader.ReadLine() ?? throw new FormatException("The CSV file does not contain a header");
      var header = splitPattern.Split(headerLine);

      string line;
      while ((line = textReader.ReadLine()) != null)
      {
        var values = splitPattern.Split(line);
        if (values.Length == 0 || values[0] == "")
          continue;

        var record = new CSVRecord();
        for (var valueIndex = 0; valueIndex < header.Length && valueIndex < values.Length; valueIndex++)
        {
          var key = header[valueIndex];
          var value = values[valueIndex].TrimStart(trimChars).TrimEnd(trimChars).Replace("\\", "");

          if (bool.TryParse(value, out var boolValue))
            record.SetBool(key, boolValue);
          else if (int.TryParse(value, out var intValue))
            record.SetInt(key, intValue);
          else if (float.TryParse(value, out var floatValue))
            record.SetFloat(key, floatValue);
          else
            record.SetString(key, value);
        }
        yield return record;
      }
    }

    // Read from a file with the specified encoding
    public static IEnumerable<CSVRecord> Read(string path, Encoding encoding)
    {
      using var textReader = new StreamReader(path, encoding);
      foreach (var record in Read(textReader))
        yield return record;
    }

    // Read from a file
    public static IEnumerable<CSVRecord> Read(string path)
    {
      using var textReader = new StreamReader(path);
      foreach (var record in Read(textReader))
        yield return record;
    }
  }
}