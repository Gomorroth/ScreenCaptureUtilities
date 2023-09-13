using System;
using UnityEngine;

namespace gomoru.su.ScreenCaptureUtilities
{
    [Serializable]
    public struct CaptureSettings
    {
        public Vector2Int Size;

        [Range(1, 8)]
        public float Scale;

        public CaptureSettings(Vector2Int size, float scale)
        {
            Size = size;
            Scale = scale;
        }

        public static CaptureSettings Default => new CaptureSettings
        (
            size: new Vector2Int(1920, 1080),
            scale: 1
        );
    }
}