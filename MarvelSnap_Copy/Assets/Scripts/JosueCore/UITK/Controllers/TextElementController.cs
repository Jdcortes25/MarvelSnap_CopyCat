using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace JosueCore.UITK.Controllers
{
    [Serializable]
    public class TextElementController : BaseUIController<TextElement>
    {
        public void SetText(string text)
        {
            Element.text = text;
            Debugger?.Log($"Text set to: {text}");
        }

        public string Text => Element.text;
    }
}