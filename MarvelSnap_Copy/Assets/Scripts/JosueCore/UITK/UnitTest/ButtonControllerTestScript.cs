using JosueCore.UITK.Controllers;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace JosueCore.UITK.UnitTest
{
    public class ButtonControllerTestScript : BaseUIControllerTestScript<ButtonController, Button>
    {
        [UnityTest]
        public IEnumerator RegisterClickedEvent()
        {
            yield return InitializeController();

            int number = 0;

            uiController.RegisterButtonClickedEvent(AddNumberByOne);
            uiController.InvokeButtonClickedEvents();
            Assert.IsTrue(number == 1);
            
            void AddNumberByOne()
            {
                number++;
            }
        }

        [UnityTest]
        public IEnumerator UnRegisterClickedEvent()
        {
            yield return InitializeController();

            int number = 0;
            uiController.RegisterButtonClickedEvent(AddNumberByOne);
            uiController.UnRegisterButtonClickedEvent(AddNumberByOne);
            uiController.InvokeButtonClickedEvents();
            Assert.IsTrue(number == 0);

            void AddNumberByOne()
            {
                number++;
            }
        }

        [UnityTest]
        public IEnumerator RegisterToggleEvent()
        {
            yield return InitializeController();

            int number = -1;
            uiController.RegisterButtonSelectedEvent(SetNumberToOne);
            uiController.RegisterButtonUnSelectedEvent(SetNumberToZero);
            uiController.IsToggleButton = true;
            uiController.InvokeButtonClickedEvents();
            Assert.IsTrue(number == 1);
            uiController.InvokeButtonClickedEvents();
            Assert.IsTrue(number == 0);

            void SetNumberToOne()
            {
                number = 1;
            }

            void SetNumberToZero()
            {
                number = 0;
            }
        }

        [UnityTest]
        public IEnumerator UnRegisterToggleEvent()
        {
            yield return InitializeController();

            int number = -1;
            uiController.RegisterButtonSelectedEvent(SetNumberToOne);
            uiController.RegisterButtonUnSelectedEvent(SetNumberToZero);
            uiController.UnRegisterButtonSelectedEvent(SetNumberToOne);
            uiController.UnRegisterButtonUnSelectedEvent(SetNumberToZero);
            uiController.IsToggleButton = true;
            uiController.InvokeButtonClickedEvents();
            Assert.IsTrue(number == -1);
            uiController.InvokeButtonClickedEvents();
            Assert.IsTrue(number == -1);

            void SetNumberToOne()
            {
                number = 1;
            }

            void SetNumberToZero()
            {
                number = 0;
            }
        }

        [UnityTest]
        public IEnumerator ToggleEventWontFireIfIsToggleSetToFalse()
        {
            yield return InitializeController();

            int number = -1;
            uiController.RegisterButtonSelectedEvent(SetNumberToOne);
            uiController.RegisterButtonUnSelectedEvent(SetNumberToZero);
            uiController.IsToggleButton = false;
            uiController.InvokeButtonClickedEvents();
            Assert.IsTrue(number == -1);
            uiController.InvokeButtonClickedEvents();
            Assert.IsTrue(number == -1);

            void SetNumberToOne()
            {
                number = 1;
            }

            void SetNumberToZero()
            {
                number = 0;
            }
        }
    }
}
