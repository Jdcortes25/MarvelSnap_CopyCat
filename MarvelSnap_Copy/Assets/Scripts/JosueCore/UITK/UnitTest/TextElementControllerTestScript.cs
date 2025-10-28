using JosueCore.UITK.Controllers;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace JosueCore.UITK.UnitTest
{
    public class TextElementControllerTestScript : BaseUIControllerTestScript<TextElementController, TextElement>
    {
        [UnityTest]
        public IEnumerator SetText()
        {          
            yield return InitializeController();

            string text = "This is a test label";
            uiController.SetText(text);
            Assert.IsTrue(text == uiController.Text, "The text that was set did not equal to the text given");
        }
    }
}
