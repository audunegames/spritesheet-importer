using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Audune.Spritesheet.Editor
{
  [CustomEditor(typeof(Spritesheet))]
  public class SpritesheetEditor : UnityEditor.Editor
  {
    // Reorderable list for the sprites
    private ReorderableList _spritesList;


    // OnEnable is called when the editor becomes enabled
    private void OnEnable()
    {
      // Create the reorderable list for the sprites
      _spritesList = new ReorderableList(serializedObject, serializedObject.FindProperty("sprites"), false, false, false, false) {
        drawElementCallback = (rect, index, isActive, isFocused) => {
          var element = _spritesList.serializedProperty.GetArrayElementAtIndex(index);
          EditorGUI.PropertyField(rect, element);
        },
        elementHeightCallback = (index) => {
          var element = _spritesList.serializedProperty.GetArrayElementAtIndex(index);
          return EditorGUI.GetPropertyHeight(element);
        },
      };
    }


    // Draw the inspector
    public override void OnInspectorGUI()
    {
      // Draw the header label
      EditorGUILayout.LabelField(new GUIContent($"Spritesheet with {serializedObject.FindProperty("sprites").arraySize} sprites"), EditorStyles.boldLabel);

      // Draw the sprite list
      _spritesList.DoLayoutList();
    }
  }
}