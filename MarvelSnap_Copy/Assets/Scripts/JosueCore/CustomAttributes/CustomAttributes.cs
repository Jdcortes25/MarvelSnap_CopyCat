using System;
using UnityEngine;

namespace JosueCore
{
    public class ConditionalFieldVisibilityAttribute : PropertyAttribute
    {
        public string FieldName;
        public string FieldValue;

        public ConditionalFieldVisibilityAttribute(string fieldName, string fieldValue)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
        }
    }

    public class SerializableInterfaceAttribute : PropertyAttribute
    {
        public Type InterfaceType;

        public SerializableInterfaceAttribute(Type interfaceType)
        {
            if(!interfaceType.IsInterface)
            {
                Debug.LogError($"{interfaceType} is not a interface");
                return;
            }

            InterfaceType = interfaceType;
        }
    }
}
