using System;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Murka.Mock
{
    [Serializable]
    public class MockSO : ScriptableObject
    {
        public MockSOData[] Mocks;
        public static MockSO _instance;

        private static void Init()
        {
            if (_instance == null)
            {
                _instance = new MockSO();
                MockSO soFile = Resources.Load<MockSO>("MockSOFile");
                _instance.Mocks = soFile.Mocks;
            }
        }

        public static T Get<T>(string id, T defaultValue)
        {
            Init();
            MockSOData data = _instance.Mocks.FirstOrDefault(e => e.ID == id);
            if (data == null)
            {
                return defaultValue;
            }

            return JsonConvert.DeserializeObject<T>(data.Value);
        }
    }

    [Serializable]
    public class MockSOData
    {
        public string ID;
        public string Value;
    }
}
