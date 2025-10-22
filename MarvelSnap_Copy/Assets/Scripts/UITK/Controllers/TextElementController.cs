using UnityEngine;
using UnityEngine.UIElements;

public class TextElementController : VisualElementController<TextElement>
{
    public void SetText(string text) => Element.text = text;
}
