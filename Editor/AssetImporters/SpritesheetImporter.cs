using Audune.Spritesheet.Utils;
using System.Linq;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Audune.Spritesheet.Editor
{
  // Class that defines an importer for spritesheets
  [ScriptedImporter(202303002, new[] { "spritesheet" })]
  public class SpritesheetImporter : ScriptedImporter
  {
    // Import the asset
    public override void OnImportAsset(AssetImportContext ctx)
    {
      // Create the sprite sheet
      var spriteSheet = ScriptableObject.CreateInstance<Spritesheet>();

      // Read the sprite sheet
      spriteSheet.sprites = CSVReader.Read(ctx.assetPath).Select(SpritesheetEntrySerializer.Deserialize).ToList();

      // Add the sprite sheet to the asset
      ctx.AddObjectToAsset(spriteSheet.name, spriteSheet);
      ctx.SetMainObject(spriteSheet);
    }
  }
}