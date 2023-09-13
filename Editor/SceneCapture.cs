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
        private static bool IsOpen { get; set; } = true;
        private static float Scale { get; set; } = 1;

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
                    boxRect.height = EditorGUIUtility.singleLineHeight * (IsOpen ? 3 : 1) + (IsOpen ? 15 : 10);

                    GUI.Box(boxRect, GUIContent.none, GUI.skin.window);

                    var innerRect = boxRect;
                    innerRect.x += 5;
                    innerRect.y += 5;
                    innerRect.width -= 10;
                    innerRect.height = EditorGUIUtility.singleLineHeight;

                    if (IsOpen = EditorGUI.BeginFoldoutHeaderGroup(innerRect, IsOpen, "Screenshot"))
                    {
                        innerRect.y += innerRect.height;
                        var labelRect = innerRect;
                        labelRect.width = EditorGUIUtility.fieldWidth;
                        GUI.Label(labelRect, "Scale");
                        var sliderRect = innerRect;
                        sliderRect.x += labelRect.width;
                        sliderRect.width -= labelRect.width;
                        Scale = EditorGUI.Slider(sliderRect, Scale, 1, 8);

                        innerRect.y += innerRect.height + 5;
                        if (GUI.Button(innerRect, "Capture"))
                        {
                            var settings = new CaptureSettings(Vector2Int.FloorToInt(rect.size), Scale);
                            var path = Utils.ShowSavePanel(scene.titleContent.text);
                            scene.camera.Capture(settings, path);
                        }
                    }
                    EditorGUI.EndFoldoutHeaderGroup();
                }
                finally
                {
                    Handles.EndGUI();
                }
            };
        }

    }
}