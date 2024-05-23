using System.Security.Cryptography;
using UnityEngine;
using System.Text;
using System.IO;
using System;

namespace Utils
{
    public static class SaveUtils
    {
        public static string RootPath
        {
            get
            {
                var rootPath = Application.persistentDataPath;
#if UNITY_EDITOR
                rootPath = Path.Combine(Application.dataPath, "..", "Saves");

                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);
#endif
                return rootPath;
            }
        }

        public static string GetHashSum(string text)
        {
            var hashBytes = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}