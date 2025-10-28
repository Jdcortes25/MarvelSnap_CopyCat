using JosueCore.UITK.Controllers;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace JosueCore.UITK.UnitTest
{
    public class BaseUIControllerTestScript<T1, T2> where T1 : BaseUIController<T2> where T2: VisualElement
    {
        private const string testEnviromentPrefabGuid = "3ace2adf8f1b5034490823ec98943c4f";
        private static GameObject testEnviromentPrefab;
        private static GameObject testEnviorment;
        protected static T1 uiController;

        [SetUp]
        public void LoadPrefab()
        {
            testEnviromentPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(testEnviromentPrefabGuid));
            Assert.IsNotNull(testEnviromentPrefab, $"Failed to find prefab with given GUID: {testEnviromentPrefabGuid}");
        }

        [SetUp]
        public void InstantiatePrefab()
        {
            testEnviorment = GameObject.Instantiate(testEnviromentPrefab);
            Assert.IsNotNull(testEnviromentPrefab, "Failed to instantiate test eviorment");
        }

        [SetUp]
        public void GetUIController()
        {
            uiController = testEnviorment.GetComponentInChildren<T1>();
            Assert.IsNotNull(uiController, $"Failed to find controller of type: {nameof(T1)}");
        }

        [UnityTest]
        public IEnumerator InitializeController()
        {
            //Wait for start
            yield return null;

            Assert.IsTrue(uiController.Initialized);
        }

        [TearDown]
        public void CleanUp()
        {
            GameObject.Destroy(testEnviorment);
        }
    }
}
