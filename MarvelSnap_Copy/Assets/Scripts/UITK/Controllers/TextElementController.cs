using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UITK.Controllers
{
    [Serializable]
    public class TextElementController : VisualElementController<TextElement>
    {
        public void SetText(string text)
        {
            Element.text = text;

            Debugger.Log($"Text set to: {text}");
        }
    }
}