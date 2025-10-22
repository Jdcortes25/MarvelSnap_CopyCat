using System;
using UnityEngine;
using UnityEngine.UIElements;

public class VisualElementController<T> : MonoBehaviour where T : VisualElement
{
    [Serializable]
    public class BaseFields
    {
        public UIDocument Document;
        public string ElementName;
    }

    [SerializeField] BaseFields baseFields;

    protected T Element;
   
    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        Element = baseFields.Document.rootVisualElement.Query<T>(baseFields.ElementName);
    }
}
