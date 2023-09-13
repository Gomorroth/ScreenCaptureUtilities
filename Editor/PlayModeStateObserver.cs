using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace gomoru.su.ScreenCaptureUtilities
{
    [InitializeOnLoad]
    internal static class PlayModeStateObserver
    {
        static PlayModeStateObserver()
        {
            EditorApplication.playModeStateChanged += x =>
            {
                if (x == PlayModeStateChange.EnteredPlayMode)
                {
                    var camera = Camera.main;
                    var c = camera.gameObject.GetComponent<ScreenCapture>();
                    if (c == null)
                    {
                        camera.gameObject.AddComponent<ScreenCapture>();
                    }
                }
            };
        }
    }
}
