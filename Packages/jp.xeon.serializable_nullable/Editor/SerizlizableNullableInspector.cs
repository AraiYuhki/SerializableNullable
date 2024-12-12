using UnityEditor;
using UnityEngine;
namespace Xeon.Utility.Editor
{

    [CustomPropertyDrawer(typeof(NullableInt))]
    [CustomPropertyDrawer(typeof(NullableUint))]
    [CustomPropertyDrawer(typeof(NullableNint))]
    [CustomPropertyDrawer(typeof(NullableNuint))]
    [CustomPropertyDrawer(typeof(NullableFloat))]
    [CustomPropertyDrawer(typeof(NullableDouble))]
    [CustomPropertyDrawer(typeof(NullableLong))]
    [CustomPropertyDrawer(typeof(NullableUlong))]
    [CustomPropertyDrawer(typeof(NullableShort))]
    [CustomPropertyDrawer(typeof(NullableUshort))]
    [CustomPropertyDrawer(typeof(NullableDecimal))]
    [CustomPropertyDrawer(typeof(NullableBool))]
    public class SerializableNullableInspector : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var hasValueProperty = property.FindPropertyRelative("hasValue");
            var isNull = !hasValueProperty.boolValue;
            var width = position.width;
            position.width *= 0.8f;
            EditorGUI.BeginDisabledGroup(isNull);
            EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), label);
            EditorGUI.EndDisabledGroup();

            position.x += position.width + 5f;
            position.width = width * 0.2f - 5f;

            var labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 30f;

            EditorGUI.BeginChangeCheck();
            isNull = EditorGUI.Toggle(position, "Null", isNull);
            if (EditorGUI.EndChangeCheck())
                hasValueProperty.boolValue = !isNull;
            EditorGUIUtility.labelWidth = labelWidth;
        }
    }
}
