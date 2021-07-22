using Core;
using UnityEngine;
using UnityEngine.UI;

public class YarnDialogueContainer : MonoBehaviour
{
    private bool isShown = true;
    
    private Button nextPhraseButton;

    private void Awake()
    {
        nextPhraseButton = GetComponent<Button>();
        
        GameEvents.Instance.onYarnLineStart.AddListener(args =>
        {
            if (!isShown)
                Hide();
            else
                Show();
        });
        GameEvents.Instance.onDialogueContainerShowRequested.AddListener(Show);
        GameEvents.Instance.onYarnDialogueEnd.AddListener(Hide);
        GameEvents.Instance.onDialogueContainerHideRequested.AddListener(ForceHide);

        nextPhraseButton.onClick.AddListener(
            () => GameEvents.Instance.onDialogueNextPhraseRequested.Invoke()
        );
    }
    
    private void Start()
    {
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
        isShown = true;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ForceHide()
    {
        gameObject.SetActive(false);
        isShown = false;
    }
}