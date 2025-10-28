using UnityEditor;
using UnityEngine;

namespace JosueCore
{
    [CustomPropertyDrawer(typeof(SerializableInterfaceAttribute))]
    public class SerializableInterfaceAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(property.propertyType != SerializedPropertyType.ObjectReference)
            {
                Debug.LogError("property must be a interface!");
                return;
            }

            SerializableInterfaceAttribute serializableInterfaceAttribute = (SerializableInterfaceAttribute)attribute;

            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
            Object interfaceFieldVal = EditorGUI.ObjectField(position, label, property.objectReferenceValue,
                serializableInterfaceAttribute.InterfaceType, true);

            if (EditorGUI.EndChangeCheck())
            {
                property.objectReferenceValue = interfaceFieldVal;
            }

            EditorGUI.EndProperty();
        }
    }
}
