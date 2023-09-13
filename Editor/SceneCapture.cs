using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace gomoru.su.ScreenCaptureUtilities 
{
    [InitializeOnLoad]
    internal static class SceneCapture
    {
        static SceneCapture()
        {
            SceneView.duringSceneGui += scene =>
            {
                Handles.BeginGUI();
                try
                {
                    var rect = scene.position;

                    var boxRect = default(Rect);
                    boxRect.x = 5;
                    boxRect.y = 5;
                    boxRect.width = 200;
                    boxRect.height = EditorGUIUtility.singleLineHeight;

                    GUI.Box(boxRect, GUIContent.none, GUI.skin.window);
                }
                finally
                {
                    Handles.EndGUI();
                }
            };
        }

    }
}