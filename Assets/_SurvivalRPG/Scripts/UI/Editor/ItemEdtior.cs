
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemSO))]
public class ItemEdtior : Editor
{
    ItemSO item;

    private void OnEnable()
    {
        item = target as ItemSO;
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        if (item.Icon == null)
            return;


        Texture2D texture = AssetPreview.GetAssetPreview(item.Icon);

        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
 
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
