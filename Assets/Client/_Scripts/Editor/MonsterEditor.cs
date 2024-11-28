using Scripts.StaticData;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MonsterStaticData))]
public class MonsterEditor : Editor
{
    MonsterStaticData data;
    private void OnEnable()
    {
        data = target as MonsterStaticData;
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        if (data.calmSprite != null)
        {
            Texture2D calmTexture = AssetPreview.GetAssetPreview(data.calmSprite);
            GUILayout.Label("Calm Sprite Preview:", EditorStyles.boldLabel);
            GUILayout.Label(calmTexture, GUILayout.Height(80), GUILayout.Width(80));
        }

        if (data.angrySprite != null)
        {
            Texture2D angryTexture = AssetPreview.GetAssetPreview(data.angrySprite);
            GUILayout.Label("Calm Sprite Preview:", EditorStyles.boldLabel);
            GUILayout.Label(angryTexture, GUILayout.Height(80), GUILayout.Width(80));
        } 
    }

}
