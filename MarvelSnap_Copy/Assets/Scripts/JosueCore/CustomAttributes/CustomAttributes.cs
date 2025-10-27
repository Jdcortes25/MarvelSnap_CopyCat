using System;
using UnityEngine;

namespace JosueCore
{
    [Serializable]
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
}
