using System.IO;
using Save;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ContinueStoryButton : MonoBehaviour
    {
        private void Start()
        {
            if (!File.Exists(SaveSystem.SAVE_FILE))
                Destroy(gameObject);
            
            GetComponent<Button>().onClick.AddListener(SaveSystem.Load);
        }
    }
}