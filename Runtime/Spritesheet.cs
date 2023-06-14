using System.Collections.Generic;
using UnityEngine;

namespace Audune.Spritesheet
{
  // Class that defines a spritesheet
  public class Spritesheet : ScriptableObject
  {
    // Sprite sheet settings
    [Tooltip("List of sprites in the sprite sheet")]
    public List<SpritesheetEntry> sprites = new List<SpritesheetEntry>();
  }
}