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
                OnEnd();
            };
            AudioManager.Instance.PlayMusic("Bamboo");
        }

        private void Start()
        {
            videoPlayer.Play();
        }
        
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Escape))
            {
                videoPlayer.Stop();
                OnEnd();
            }
        }

        private void OnEnd()
        {
            SceneManager.LoadScene("hat1a");
        }
    }
}