using JosueCore.UITK.Controllers;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace JosueCore.UITK.UnitTest
{
    public class TextElementControllerTestScript
    {
        private const string testEnviromentPrefabGuid = "3ace2adf8f1b5034490823ec98943c4f";
        private static GameObject testEnviromentPrefab;
        private static GameObject testEnviorment;
        private static TextElementController textElementController;

        [OneTimeSetUp]
        public void LoadPrefab()
        {
            testEnviromentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(testEnviromentPrefabGuid));
            Assert.IsNotNull(testEnviromentPrefab, $"Failed to find prefab with given GUID: {testEnviromentPrefabGuid}");
        }

        [OneTimeSetUp]
        public void InstantiatePrefab()
        {
            testEnviorment = GameObject.Instantiate(testEnviromentPrefab);
            Assert.IsNotNull(testEnviromentPrefab, "Failed to instantiate test eviorment");
        }

        [OneTimeSetUp]
        public void GetTextElementController()
        {
            textElementController = testEnviorment.GetComponentInChildren<TextElementController>();
            Assert.IsNotNull(textElementController, "Failed to find TextElementController component");
        }

        [UnityTest]
        public IEnumerator InitializeController()
        {
            //Wait for start
            yield return null;

            Assert.IsTrue(textElementController.Initialized);
        }

        [Test]
        public void SetText()
        {
            string text = "This is a test label";
            textElementController.SetText(text);
            Assert.IsTrue(text == textElementController.Text, "The text that was set did not equal to the text given");
        }
    }
}
