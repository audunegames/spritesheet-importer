using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Audune.Spritesheet.Utils
{
  // Class that defines a CSV writer
  public sealed class CSVWriter
  {
    // Write to a text writer
    public static void Write(IEnumerable<CSVRecord> records, TextWriter textWriter)
    {
      var keys = records.SelectMany(record => record.Keys).Distinct().ToList();

      textWriter.WriteLine(string.Join(",", keys));

      foreach (var record in records)
      {
        var values = new List<string>();
        foreach (var key in keys)
        {
          if (record.TryGetObject(key, out var value))
          {
            var stringValue = value switch {
              bool boolValue => boolValue ? "true" : "false",
              int intValue => intValue.ToString(CultureInfo.InvariantCulture),
              float floatValue => floatValue.ToString("G", CultureInfo.InvariantCulture),
              _ => value.ToString(),
            };

            values.Add(stringValue.Contains("\"") ? $"\"{stringValue.Replace("\"", "\"\"")}\"" : stringValue);
          }
          else
          {
            values.Add("");
          }
        }

        textWriter.WriteLine(string.Join(",", values));
      }
    }

    // Write to a file with the specified encoding
    public static void Write(IEnumerable<CSVRecord> records, string path, Encoding encoding)
    {
      using var textWriter = new StreamWriter(path, false, encoding);
      Write(records, textWriter);
    }

    // Write to a file
    public static void Write(IEnumerable<CSVRecord> records, string path)
    {
      using var textWriter = new StreamWriter(path, false);
      Write(records, textWriter);
    }
  }
}