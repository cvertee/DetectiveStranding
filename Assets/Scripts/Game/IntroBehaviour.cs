using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Game
{
    public class IntroBehaviour : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        
        private void Awake()
        {
            videoPlayer = FindObjectOfType<VideoPlayer>();
            videoPlayer.loopPointReached += source =>
            {
                SceneManager.LoadScene("hat1a");
            };
            AudioManager.Instance.PlayMusic("MainTheme");
        }

        private void Start()
        {
            videoPlayer.Play();
        }
        
        private void Update()
        {
            if (!videoPlayer.isPlaying)
            {
            //    SceneManager.LoadSceneAsync("hat1a", LoadSceneMode.Single);
            }
        }
    }
}