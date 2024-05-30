#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Utils
{
    public static class SceneUtils
    {
        private static readonly string _scriptsFolderPath = $"{Application.dataPath}/Scripts/Scene/Scenes";

        [MenuItem("Parameterized scenes/Generate scenes scripts")]
        private static void GenerateSceneScripts()
        {
            var sceneNames = GetNewSceneNames();

            if (!sceneNames.Any())
                return;

            if (!Directory.Exists(_scriptsFolderPath))
                Directory.CreateDirectory(_scriptsFolderPath);

            foreach (var name in sceneNames)
            {
                var path = Path.Combine(_scriptsFolderPath, $"{name}.cs");
                var code = $@"using UnityEngine;

namespace Scenes
{{
    public sealed class {name} : Scene
    {{
        public static string Name => ""{name}"";

        public static AsyncOperation LoadScene()
        {{
            return LoadScene(Name);
        }}

        public static AsyncOperation LoadScene<T>(T argument)
        {{
            return LoadScene(Name, argument);
        }}
    }}
}}";

                File.WriteAllText(path, code);
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private static List<string> GetNewSceneNames()
        {
            var scenesGuids = AssetDatabase.FindAssets("t:Scene");
            var scenes = new List<string>();

            foreach (var guid in scenesGuids)
            {
                var scenePath = AssetDatabase.GUIDToAssetPath(guid);
                var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

                scenes.Add(scene.name);
            }

            if (Directory.Exists(_scriptsFolderPath))
            {
                var scriptsPath = Directory.EnumerateFiles(_scriptsFolderPath);

                if (scriptsPath == null || !scriptsPath.Any())
                    return scenes;

                var scriptsNames = scriptsPath.Select(sp => Path.GetFileName(sp));

                foreach (var name in scriptsNames)
                {
                    scenes.Remove(name);
                }
            }

            return scenes;
        }
    }
}
#endif