using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEditor;

namespace gomoru.su.ScreenCaptureUtilities
{
    internal static class Utils
    {
        public static string ShowSavePanel(string prefix) => EditorUtility.SaveFilePanel("Save", "", $"{prefix} {DateTime.Now:yyyy-MM-dd HH-mm-ss.fff}", "png");
        
        public static void Capture(this Camera camera, in CaptureSettings settings, string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            var (width, height) = Vector2Int.FloorToInt((Vector2)settings.Size * settings.Scale);

            var rt = RenderTexture.GetTemporary(width, height);
            var targetTexture = camera.targetTexture;
            try
            {
                camera.targetTexture = rt;
                camera.RenderDontRestore();

                var request = AsyncGPUReadback.Request(rt);
                request.WaitForCompletion();

                var data = ImageConversion.EncodeNativeArrayToPNG(request.GetData<Color>(), rt.graphicsFormat, (uint)width, (uint)height);
                var fileInfo = new FileInfo(path);
                if (!fileInfo.Directory.Exists)
                    fileInfo.Directory.Create();

                using (var fs = fileInfo.Create())
                {
                    var buffer = _buffer;
                    if (buffer == null || buffer.Length < data.Length)
                    {
                        buffer = _buffer = new byte[data.Length];
                    }
                    unsafe
                    {
                        fixed (byte* p = buffer)
                        {
                            UnsafeUtility.MemCpy(p, data.GetUnsafeReadOnlyPtr(), data.Length);
                        }
                    }
                    fs.Write(buffer, 0, data.Length);
                }
            }
            finally
            {
                camera.targetTexture = targetTexture;
                RenderTexture.ReleaseTemporary(rt);
            }
        }

        private static byte[] _buffer;

        private static void Deconstruct(this Vector2Int vector, out int x, out int y)
        {
            x = vector.x;
            y = vector.y;
        }
    }
}
