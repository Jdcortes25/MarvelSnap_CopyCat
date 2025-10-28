using System;
using UnityEditor;
using UnityEngine;

namespace JosueCore
{
    [CustomPropertyDrawer(typeof(ConditionalFieldVisibilityAttribute))]
    public class ConditionalFieldVisibilityAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalFieldVisibilityAttribute conditionalAttribute = (ConditionalFieldVisibilityAttribute)attribute;
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionalAttribute.FieldName);

            if (conditionProperty == null)
            {              
                EditorGUI.PropertyField(position, property, label, true);
                Debug.LogWarning($"{nameof(ConditionalFieldVisibilityAttribute)} Field '{conditionalAttribute.FieldName}' not found on object '{property.serializedObject.targetObject}'.");
                return;
            }

            bool show = ShouldShow(conditionProperty, conditionalAttribute.FieldValue);

            if (!show)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionalFieldVisibilityAttribute conditionalAttribute = (ConditionalFieldVisibilityAttribute)attribute;
            SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionalAttribute.FieldName);

            if (conditionProperty == null)
            {
                return EditorGUI.GetPropertyHeight(property, label, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            bool show = ShouldShow(conditionProperty, conditionalAttribute.FieldValue);

            if (!show)
            {
                // Return zero so Unity collapses the row entirely
                return 0f;
            }

            return EditorGUI.GetPropertyHeight(property, label, true) + EditorGUIUtility.standardVerticalSpacing;
        }

        private bool ShouldShow(SerializedProperty conditionProperty, string expectedValue)
        {
            switch (conditionProperty.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    return conditionProperty.boolValue.ToString().ToLower() == expectedValue.ToLower();

                case SerializedPropertyType.Enum:
                    string enumName = conditionProperty.enumNames[conditionProperty.enumValueIndex];
                    return enumName.Equals(expectedValue, StringComparison.OrdinalIgnoreCase);

                case SerializedPropertyType.String:
                    return conditionProperty.stringValue == expectedValue;

                case SerializedPropertyType.Integer:
                    return conditionProperty.intValue.ToString() == expectedValue;

                default:
                    return false;
            }
        }
    }
}
