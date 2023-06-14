using Audune.Spritesheet.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Audune.Spritesheet.Editor
{
  // Class that defines menu items for spritesheets
  public static class SpritesheetMenuItems
  {
    // Save the sprites in the selected asset to a sprite sheet
    [MenuItem("Assets/Audune Spritesheet/Save To Spritesheet Asset (.asset)")]
    public static void SaveSelectedToSpriteSheet()
    {
      var sprites = GetSpritesFromAsset(Selection.activeObject);
      if (sprites.Count() == 0)
        return;

      var spriteSheet = ConvertSpritesToSpriteSheet(sprites);

      var assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
      var path = Path.GetFullPath(Path.Combine($"{Application.dataPath}/..", $"{assetPath.Substring(0, assetPath.LastIndexOf("."))}.asset"))
        .Replace('/', Path.DirectorySeparatorChar);

      path = EditorUtility.SaveFilePanelInProject($"Save sprite sheet asset for {Selection.activeObject.name}", path, "asset", $"Enter a file name to save the sprite sheet asset for {Selection.activeObject.name} to");
      if (!string.IsNullOrEmpty(path))
      {
        AssetDatabase.CreateAsset(spriteSheet, path);
        AssetDatabase.SaveAssets();
      }
    }

    [MenuItem("Assets/Audune Spritesheet/Save To Spritesheet Asset (.asset)", true)]
    public static bool ValidateSaveSelectedToSpriteSheet()
    {
      return GetSpritesFromAsset(Selection.activeObject).Count() > 0;
    }

    // Save the sprites in the selected asset to a sprite sheet file
    [MenuItem("Assets/Audune Spritesheet/Save To Spritesheet File (.spritesheet)")]
    public static void SaveSelectedToSpriteSheetFile()
    {
      var sprites = GetSpritesFromAsset(Selection.activeObject);
      if (sprites.Count() == 0)
        return;

      var spritesheet = ConvertSpritesToSpriteSheet(sprites);

      var assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
      var path = Path.GetFullPath(Path.Combine($"{Application.dataPath}/..", $"{assetPath.Substring(0, assetPath.LastIndexOf("."))}.spritesheet"))
        .Replace('/', Path.DirectorySeparatorChar);

      path = EditorUtility.SaveFilePanelInProject($"Save sprite sheet file for {Selection.activeObject.name}", path, "spritesheet", $"Enter a file name to save the sprite sheet file for {Selection.activeObject.name} to");
      if (!string.IsNullOrEmpty(path))
      {
        CSVWriter.Write(spritesheet.sprites.Select(SpritesheetEntrySerializer.Serialize), path);
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
      }
    }

    [MenuItem("Assets/Audune Spritesheet/Save To Spritesheet File (.spritesheet)", true)]
    public static bool ValidateSaveSelectedToSpriteSheetFile()
    {
      return GetSpritesFromAsset(Selection.activeObject).Count() > 0;
    }


    // Get the sprites from an asset
    private static IEnumerable<Sprite> GetSpritesFromAsset(Object asset)
    {
      var assetPath = AssetDatabase.GetAssetPath(asset);
      return assetPath != "" ? AssetDatabase.LoadAllAssetsAtPath(assetPath).OfType<Sprite>() : Enumerable.Empty<Sprite>();
    }

    // Convert a list of sprites to a sprite sheet
    private static Spritesheet ConvertSpritesToSpriteSheet(IEnumerable<Sprite> sprites)
    {
      var spritesheet = ScriptableObject.CreateInstance<Spritesheet>();
      foreach (var sprite in sprites)
        spritesheet.sprites.Add(new SpritesheetEntry(sprite.name, sprite.rect, sprite.pivot, sprite.border));
      return spritesheet;
    }
  }
}
