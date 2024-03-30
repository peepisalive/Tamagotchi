using Settings.Job;
using UnityEditor;
using UnityEngine;

namespace InEditor
{
    [CustomEditor(typeof(JobSettings))]
    public sealed class JobSettingsEditor : Editor
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
            var jobSettings = (JobSettings)target;
            var path = "Assets/Settings/Job";
            var guids = AssetDatabase.FindAssets("t:JobTypeSettings", new string[] { path });

            foreach (var guid in guids)
            {
                var settingsPath = AssetDatabase.GUIDToAssetPath(guid);
                var settings = AssetDatabase.LoadAssetAtPath<JobTypeSettings>(settingsPath);

                jobSettings.Add(settings);
            }

            EditorUtility.SetDirty(jobSettings);
            AssetDatabase.SaveAssets();
        }
    }
}