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
    }
    
    private void Start()
    {
        GameEvents.Instance.onYarnLineStart.AddListener(args =>
        {
            if (!isShown)
                Hide();
            else
                Show();
        });
        //Notific.Subscribe(NotificType.DialogueContainerShowRequested, args => Show());
        GameEvents.Instance.onDialogueContainerShowRequested.AddListener(Show);
        //Notific.Subscribe(NotificType.YarnDialogueEnd, args => Hide());
        GameEvents.Instance.onYarnDialogueEnd.AddListener(Hide);
        //Notific.Subscribe(NotificType.DialogueContainerHideRequested, args => ForceHide());
        GameEvents.Instance.onDialogueContainerHideRequested.AddListener(ForceHide);

        nextPhraseButton.onClick.AddListener(
            () => GameEvents.Instance.onDialogueNextPhraseRequested.Invoke()
        );
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