using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gomoru.su.ScreenCaptureUtilities
{
    [RequireComponent(typeof(Camera))]
    public sealed class ScreenCapture : MonoBehaviour
    {
        public CaptureSettings CaptureSettings = CaptureSettings.Default;
    }
}