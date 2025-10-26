using JosueCore.DebuggerSystem;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JosueCore.UITK.Controllers
{
    [Serializable]
    public class BaseUIController<T> : MonoBehaviour, IDebuggable where T : VisualElement
    {
        [Serializable]
        public class BaseFields
        {
            public UIDocument Document;
            public string ElementName;
        }

        [SerializeField] private BaseFields baseFields;

        protected T Element;
        protected Debugger Debugger;

        private bool initialized = false;

        public bool Initialized => initialized;

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
                Debugger?.LogError($"Failed to find Element with given name");
            }
            else
            {
                Debugger?.Log("Element found successfully");
            }

            initialized = true;
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