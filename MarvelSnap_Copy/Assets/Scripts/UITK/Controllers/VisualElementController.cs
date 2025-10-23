using DebuggerSystem;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UITK.Controllers
{
    [Serializable]
    public class VisualElementController<T> : MonoBehaviour, IDebuggable where T : VisualElement
    {
        [Serializable]
        public class BaseFields
        {
            public UIDocument Document;
            public string ElementName;
        }

        [SerializeField] BaseFields baseFields;

        protected T Element;
        protected Debugger Debugger;

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
            string elementName = baseFields.ElementName;
            Element = baseFields.Document.rootVisualElement.Query<T>(elementName);

            if (Element == null)
            {
                Debugger.LogError($"Failed to find Element with given name");
            }
            else
            {
                Debugger.Log("Element found successfully");
            }
        }

        public void SetDebugger(Debugger debugger)
        {
            Debugger = debugger;
        }

        public string GetDebuggerName()
        {
            return baseFields.ElementName;
        }
    }
}