using System.IO;
using Core;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Save
{
    public static class SaveSystem
    {
        public static readonly string SAVE_FILE = Application.persistentDataPath + "/save.sav";
            
        public static void Save()
        {
            var json = JsonConvert.SerializeObject(GameData.GetSaveData());

            Debug.Log($"Saving json to {SAVE_FILE} | {json}");

            if (!File.Exists(SAVE_FILE))
            {
                var handle = File.Create(SAVE_FILE);
                handle.Close();
            }
            
            File.WriteAllText(SAVE_FILE, json);
        }

        public static void Load()
        {
            if (!File.Exists(SAVE_FILE))
                return;
            
            var json = File.ReadAllText(SAVE_FILE);
            var saveData = JsonConvert.DeserializeObject<SaveData>(json);

            GameData.UpdateSaveData(saveData);
            SceneManager.LoadScene("hat1a", LoadSceneMode.Single);
        }
    }
}