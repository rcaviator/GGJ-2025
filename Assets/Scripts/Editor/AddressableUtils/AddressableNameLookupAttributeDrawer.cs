using System.Collections.Generic;
using System.Linq;
using GGJ2025.AddressableUtils;
using UnityEditor;
using UnityEngine;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace GGJ2025.Editor.AddressableUtils;

[CustomPropertyDrawer(typeof(AddressableNameLookupAttribute))]
public class AddressableNameLookupAttributeDrawer : PropertyDrawer
{
  private GUIContent[]? names;

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    if (names == null)
    {
      var lookupAttribute = (AddressableNameLookupAttribute)attribute;
      var assets = new List<AddressableAssetEntry>();
      AddressableAssetSettingsDefaultObject.Settings.GetAllAssets(assets, false, null,
        entry => lookupAttribute.Type == null || lookupAttribute.Type == entry.MainAssetType);
      names = assets.Select(a => new GUIContent(a.AssetPath)).ToArray();
    }

    var selectedIndex = -1;
    for (var i = 0; i < names.Length; i++)
    {
      if (names[i].text == property.stringValue)
      {
        selectedIndex = i;
        break;
      }
    }

    var newIndex = EditorGUI.Popup(position, label, selectedIndex == -1 ? 0 : selectedIndex, names);
    if (newIndex != selectedIndex)
    {
      property.stringValue = names[newIndex].text;
    }
  }
}