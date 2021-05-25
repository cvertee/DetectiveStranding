using System.IO;
using Core;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Save
{
    public static class SaveSystem
    {
        public static string SAVE_FILE = Application.persistentDataPath + "/save.sav"; // TODO: probably several profiles!
            
        public static void Save()
        {
            var json = JsonConvert.SerializeObject(GameData.Data);

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

            GameData.Data = saveData;
            SceneManager.LoadScene("hat1a", LoadSceneMode.Single);
        }
        
        public static void LoadFrom(SaveData saveData)
        {
            //GameData.sceneName = saveData.sceneName;
            //GameData.sceneLoadedAdditively = saveData.sceneLoadedAdditively;
            //GameData.YarnVariables = saveData.yarnVariables;
            //GameData.yarnNode = saveData.yarnNode;

            
        }
    }
}