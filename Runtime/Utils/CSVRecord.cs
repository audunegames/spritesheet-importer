using System;
using System.Collections.Generic;

namespace Audune.Spritesheet.Utils
{
  // Class that defines a record
  public sealed class CSVRecord
  {
    // The dictionary backed by the record
    private readonly Dictionary<string, object> _data = new Dictionary<string, object>();


    // Return the keys of the record
    public IEnumerable<string> Keys => _data.Keys;


    // Return if the record contains the specified key
    public bool ContainsKey(string key)
    {
      return _data.ContainsKey(key);
    }


    // Return if the record contains the specified key and store its value as an object
    public bool TryGetObject(string key, out object objectValue)
    {
      return _data.TryGetValue(key, out objectValue);
    }

    // Return the value of the specified key as an object, or a default value if the key does not exist
    public object GetObject(string key, object defaultValue = default)
    {
      return TryGetObject(key, out var value) ? value : defaultValue;
    }

    // Return if the record contains the specified key and store its value as a bool
    public bool TryGetBool(string key, out bool boolValue)
    {
      var result = TryGetObject(key, out var value) && value is bool;
      boolValue = result ? (bool)value : default;
      return result;
    }

    // Return the value of the specified key as a bool, or a default value if the key does not exist
    public bool GetBool(string key, bool defaultValue = default)
    {
      return TryGetBool(key, out var value) ? value : defaultValue;
    }

    // Return if the record contains the specified key and store its value as an int
    public bool TryGetInt(string key, out int intValue)
    {
      var result = TryGetObject(key, out var value) && value is int;
      intValue = result ? (int)value : default;
      return result;
    }

    // Return the value of the specified key as an int, or a default value if the key does not exist
    public int GetInt(string key, int defaultValue = default)
    {
      return TryGetInt(key, out var value) ? value : defaultValue;
    }

    // Return if the record contains the specified key and store its value as a float
    public bool TryGetFloat(string key, out float floatValue)
    {
      var result = TryGetObject(key, out var value) && (value is float || value is int);

      floatValue = result ? (value is int ? (int)value : (float)value) : default;
      return result;
    }

    // Return the value of the specified key as a float, or a default value if the key does not exist
    public float GetFloat(string key, float defaultValue = default)
    {
      return TryGetFloat(key, out var value) ? value : defaultValue;
    }

    // Return if the record contains the specified key and store its value as a string
    public bool TryGetString(string key, out string stringValue)
    {
      var result = TryGetObject(key, out var value) && value is string;
      stringValue = result ? (string)value : default;
      return result;
    }

    // Return the value of the specified key as a string, or a default value if the key does not exist
    public string GetString(string key, string defaultValue = default)
    {
      return TryGetString(key, out var value) ? value : defaultValue;
    }


    // Set the value of the specified key as an object
    public void SetObject(string key, object value)
    {
      _data[key] = value;
    }

    // Set the value of the specified key as a bool
    public void SetBool(string key, bool boolValue)
    {
      SetObject(key, boolValue);
    }

    // Set the value of the specified key as an int
    public void SetInt(string key, int intValue)
    {
      SetObject(key, intValue);
    }

    // Set the value of the specified key as a float
    public void SetFloat(string key, float floatValue)
    {
      SetObject(key, floatValue);
    }

    // Set the value of the specified key as a string
    public void SetString(string key, string stringValue)
    {
      SetObject(key, stringValue);
    }
  }
}