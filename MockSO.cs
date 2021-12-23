using System;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace anry.Mock
{
    [Serializable]
    public class MockSO : ScriptableObject
    {
        public bool Enabled = true;
        public MockSOData[] Mocks;
        
        private static MockSO _instance;
        private static MockSO _soFile;

        private static void Init()
        {
            if (_instance == null)
            {
                _instance = new MockSO();
                _soFile = Resources.Load<MockSO>("MockSOFile");
            }

            _instance.Mocks = _soFile.Mocks;
            _instance.Enabled = _soFile.Enabled;
        }

        public static T Get<T>(string id, T defaultValue)
        {
            Init();

            if (_instance.Enabled == false || _instance.Mocks == null)
                return defaultValue;
            
            MockSOData data = _instance.Mocks.FirstOrDefault(e => e.ID == id);
            if (data == null || data.Enabled == false)
                return defaultValue;

            return JsonConvert.DeserializeObject<T>(data.Value);
        }
    }

    [Serializable]
    public class MockSOData
    {
        public string ID;
        [Multiline]
        public string Value;
        public bool Enabled = true;
    }
}
