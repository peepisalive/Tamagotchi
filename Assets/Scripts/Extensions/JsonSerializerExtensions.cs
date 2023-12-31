using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Text;
using UnityEngine;
using System.IO;
using System;

namespace Extensions
{
    public static class JsonSerializerExtensions
    {
        public static Task<T> DeserializeJsonAsync<T>(this string data, JsonSerializerSettings settings = null)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var result = stream.DeserializeJsonAsync<T>(settings);

            stream.Close();
            
            return result;
        }

        public static Task<T> DeserializeJsonAsync<T>(this Stream stream, JsonSerializerSettings settings = null)
        {
            return Task.Run(() =>
            {
                using (stream)
                using (var streamReader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var serializer = settings == null ? JsonSerializer.CreateDefault() : JsonSerializer.Create(settings);
                    var result = serializer.Deserialize<T>(jsonReader);

                    jsonReader.Close();
                    streamReader.Close();

                    return result;
                }
            });
        }

        public static async Task<string> SerializeJsonAsync<T>(this T instance, Formatting formatting = Formatting.None,
            JsonSerializerSettings settings = null, CancellationToken ct = default)
        {
            using (var stream = new MemoryStream())
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    await instance.SerializeJsonAsync(stream, formatting, settings, ct: ct).ConfigureAwait(false);
                }
                catch (OperationCanceledException e)
                {
#if UNITY_EDITOR
                    Debug.LogError($"Json serialization cancelled. Message: {e.Message}");
#endif
                    return string.Empty;
                }

                var result = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();

                return result;
            }
        }

        public async static Task SerializeJsonAsync<T>(this T instance, Stream toStream, Formatting formatting = Formatting.None,
            JsonSerializerSettings settings = null, CancellationToken ct = default)
        {
            var task = Task.Run(() =>
            {
                using (var streamWriter = new StreamWriter(toStream))
                {
                    ct.ThrowIfCancellationRequested();

                    var serializer = settings == null ? JsonSerializer.CreateDefault() : JsonSerializer.Create(settings);
                    serializer.Formatting = formatting;

                    serializer.Serialize(streamWriter, instance);
                    streamWriter.Close();
                }
            });

            try
            {
                await task;
            }
            catch (OperationCanceledException e)
            {
#if UNITY_EDITOR
                Debug.LogError($"Json serialization cancelled. Message: {e.Message}");
#endif
            }
        }
    }
}