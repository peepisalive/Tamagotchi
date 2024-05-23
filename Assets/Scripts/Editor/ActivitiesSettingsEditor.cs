using Settings.Activity;
using UnityEditor;
using UnityEngine;

namespace InEditor
{
    [CustomEditor(typeof(ActivitiesSettings))]
    public sealed class ActivitiesSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Fill settings"))
            {
                FillSettings();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }

        private void FillSettings()
        {
            var activitiesSettings = (ActivitiesSettings)target;
            var path = "Assets/Settings/Activities";
            var guids = AssetDatabase.FindAssets("t:ActivitySettings", new string[] { path });

            foreach (var guid in guids)
            {
                var settingsPath = AssetDatabase.GUIDToAssetPath(guid);
                var settings = AssetDatabase.LoadAssetAtPath<ActivitySettings>(settingsPath);

                activitiesSettings.Add(settings);
            }

            EditorUtility.SetDirty(activitiesSettings);
            AssetDatabase.SaveAssets();
        }
    }
}