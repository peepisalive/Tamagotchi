using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System;

namespace Save
{
    public sealed class SaveDataProvider
    {
        public void SaveFile(string filePath, string saveData)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            try
            {
                using (var sw = new StreamWriter(filePath))
                {
                    sw.Write(saveData);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError($"Save file exception, path: {filePath}.\n{e}");
#endif
            }
        }

        public async Task SaveFileAsync(string filePath, string saveData, CancellationToken ct = default)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            try
            {
                using (var sw = new StreamWriter(filePath))
                {
                    ct.ThrowIfCancellationRequested();
                    await Task.Run(async () =>
                    {
                        await sw.WriteAsync(saveData);
                        sw.Close();
                    }, ct);
                }
            }
            catch(OperationCanceledException e)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError($"Save file async cancelled, path: {filePath}.\n{e.Message}");
#endif
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError($"Save file async exception, path: {filePath}.\n{e}");
#endif
            }
        }

        public bool TryLoadFile(string filePath, out string loadedData)
        {
            loadedData = string.Empty;

            try
            {
                loadedData = File.ReadAllText(filePath);
                return true;
            }
            catch(Exception e)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError($"Try load file exception, path: {filePath}.\n{e}");
#endif
                return false;
            }
        }
    }
}