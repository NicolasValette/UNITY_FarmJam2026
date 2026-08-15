using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    public class SaveGame : MonoBehaviour
    {
        public static SaveGame Instance { get; private set; }
        private readonly List<ISaveable> _saveables = new List<ISaveable>();

        private SaveData SaveData;
        public bool IsGameContinue = false;
        public bool IsSaveExist = false;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            this.transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            IsGameContinue = false;
            
            SaveData = ScriptableObject.CreateInstance<SaveData>();
            IsSaveExist = LoadData();
        }
        public void RegisterSaveable(ISaveable itemToSave)
        {
            if (_saveables.Contains(itemToSave))
                return;

            Debug.Log("Register saveable - " + itemToSave.Name);
            _saveables.Add(itemToSave);
        }

        public void UnregisterSaveable(ISaveable itemToSave)
        {
            _saveables.Remove(itemToSave);
        }

        public void Save()
        {
            SaveData = ScriptableObject.CreateInstance<SaveData>();
            SaveData.VersionSaved = Application.version;
            _saveables.ForEach(x => x.Save(ref SaveData));
            

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new ColorJsonConverter() }
            };

            string json = JsonConvert.SerializeObject(SaveData, settings);
            Debug.Log("JSON : \n" + json);

            PlayerPrefs.SetString("save", json);
            PlayerPrefs.Save();

            Debug.Log($"[SAVE] game successfully saved !");

                
        }
        public bool LoadData()
        {
            string json = PlayerPrefs.GetString("save");
            Debug.Log("json loaded = \n" + json);
            if (string.IsNullOrEmpty(json)) return false;
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new List<JsonConverter> { new ColorJsonConverter(), new ScripableObjectConverter() }
            };
            SaveData = ScriptableObject.CreateInstance<SaveData>();
            JsonConvert.PopulateObject(json, SaveData, settings);

            if (SaveData.VersionSaved != Application.version) return false;
            return true;
        }
        public void Load()
        {
            foreach (var saveable in _saveables)
            {
                saveable.Load(SaveData);
            }

            Debug.Log("[SAVE] Loading complete !");
            
        }
        public void SetContinue()
        {
            IsGameContinue = true;
        }
    }
}
