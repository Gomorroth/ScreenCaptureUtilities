using UnityEditor;
using UnityEngine;

namespace gomoru.su.ScreenCaptureUtilities
{
    [CustomPropertyDrawer(typeof(CaptureSettings))]
    internal sealed class CaptureSettingsPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * (string.IsNullOrEmpty(label.text) ? 2 : property.isExpanded ? 3 : 1);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var rect = position;
                rect.height = EditorGUIUtility.singleLineHeight;

                bool useFoldout = !string.IsNullOrEmpty(label.text);

                if (useFoldout)
                {
                    property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(rect, property.isExpanded, label);
                    rect.y += rect.height;
                    EditorGUI.indentLevel++;
                }

                bool isVisible = !useFoldout || property.isExpanded;

                if (isVisible)
                {
                    //EditorGUI.PropertyField(rect, property.FindPropertyRelative(nameof(CaptureSettings.Size)));
                    var labelRect = rect;
                    labelRect.width = EditorGUIUtility.labelWidth;
                    var fieldRect = rect;
                    fieldRect.width -= EditorGUIUtility.labelWidth;
                    fieldRect.x += labelRect.width;
                    EditorGUI.LabelField(labelRect, "Size");
                    EditorGUI.PropertyField(fieldRect, property.FindPropertyRelative(nameof(CaptureSettings.Size)), GUIContent.none);

                    rect.y += rect.height;

                    EditorGUI.PropertyField(rect, property.FindPropertyRelative(nameof(CaptureSettings.Scale)));
                    rect.y += rect.height;
                }

                if (useFoldout)
                {
                    EditorGUI.indentLevel--;
                    EditorGUI.EndFoldoutHeaderGroup();
                }
            }
        }
    }
}