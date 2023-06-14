using Audune.Spritesheet.Utils;
using System;
using UnityEngine;

namespace Audune.Spritesheet.Editor
{
  // Class that defines a record(de)serializer for sprite sheet entries
  public sealed class SpritesheetEntrySerializer
  {
    // Deserialize the spritesheet entry
    public static SpritesheetEntry Deserialize(CSVRecord record)
    {
      if (!record.TryGetString("name", out var name))
        throw new ArgumentException("The record does not contain a \"name\" value");

      if (!record.TryGetFloat("rect_x", out var rectX))
        throw new ArgumentException("The record does not contain a \"rect_x\" value");
      if (!record.TryGetFloat("rect_y", out var rectY))
        throw new ArgumentException("The record does not contain a \"rect_y\" value");
      if (!record.TryGetFloat("rect_width", out var rectWidth))
        throw new ArgumentException("The record does not contain a \"rect_width\" value");
      if (!record.TryGetFloat("rect_height", out var rectHeight))
        throw new ArgumentException("The record does not contain a \"rect_height\" value");
      var rect = new Rect(rectX, rectY, rectWidth, rectHeight);

      Vector2? pivot = null;
      if (record.TryGetFloat("pivot_x", out var pivotX)
        && record.TryGetFloat("pivot_y", out var pivotY))
        pivot = new Vector2(pivotX, pivotY);

      Vector4? border = null;
      if (record.TryGetFloat("border_left", out var borderLeft)
        && record.TryGetFloat("border_right", out var borderRight)
        && record.TryGetFloat("border_top", out var borderTop)
        && record.TryGetFloat("border_bottom", out var borderBottom))
        border = new Vector4(borderLeft, borderBottom, borderRight, borderTop);

      return new SpritesheetEntry(name, rect, pivot, border);
    }

    // Serialize the spritesheet entry
    public static CSVRecord Serialize(SpritesheetEntry entry)
    {
      var record = new CSVRecord();

      record.SetString("name", entry.name);

      record.SetFloat("rect_x", entry.rect.x);
      record.SetFloat("rect_y", entry.rect.y);
      record.SetFloat("rect_width", entry.rect.width);
      record.SetFloat("rect_height", entry.rect.height);

      if (entry.pivot != Vector2.zero)
      {
        record.SetFloat("pivot_x", entry.pivot.x);
        record.SetFloat("pivot_y", entry.pivot.y);
      }

      if (entry.border != Vector4.zero)
      {
        record.SetFloat("border_left", entry.border.x);
        record.SetFloat("border_right", entry.border.z);
        record.SetFloat("border_top", entry.border.w);
        record.SetFloat("border_bottom", entry.border.y);
      }

      return record;
    }
  }
}