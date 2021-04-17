using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Where phrase is displayed in dialogue interface
public class YarnPhrase : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
        GameEvents.Instance.onYarnLineStart.AddListener(
            args =>
            {
                // var argsSplit = args.Split(':');
                // text.text = argsSplit[1].Trim();
            });
    }
    
    private void Start()
    {
        
    }
}