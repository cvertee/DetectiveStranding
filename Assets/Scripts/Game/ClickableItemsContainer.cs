using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class ClickableItemsContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.Instance.onSceneSwitchRequested.AddListener(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
