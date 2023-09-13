using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace gomoru.su.ScreenCaptureUtilities
{
    [CustomEditor(typeof(ScreenCapture))]
    internal sealed class ScreenCaptureEditor : Editor
    {
        private SerializedProperty Settings;

        public void OnEnable()
        {
            Settings = serializedObject.FindProperty(nameof(ScreenCapture.CaptureSettings));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(Settings, GUIContent.none);

            serializedObject.ApplyModifiedProperties();

            var camera = (serializedObject.targetObject as Component)?.GetComponent<Camera>();

            using (new EditorGUI.DisabledGroupScope(camera == null))
            {
                if (GUILayout.Button("Capture"))
                {
                    var path = Utils.ShowSavePanel(camera.gameObject.scene.name);
                    if (!string.IsNullOrEmpty(path))
                    {
                        camera.Capture((target as ScreenCapture).CaptureSettings, path);
                    }
                }
            }
        }
    }
}