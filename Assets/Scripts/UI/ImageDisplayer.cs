using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageDisplayer : MonoBehaviour
    {
        public Image image;

        private void Awake()
        {
            GameEvents.Instance.onImageShowRequested.AddListener((id) => Show(id));
            GameEvents.Instance.onImageHideRequested.AddListener(Hide);

            if (image == null)
                image = GetComponent<Image>(); // try to look for it in this object 
            
            Hide();
        }

        private void SetImage(string imageId)
        {
            Debug.Log($"Loading shown image {imageId}");
            image.sprite = ResourceLoader.LoadShownImage(imageId);
        }
        
        public void Show(string imageId)
        {
            Debug.Log($"{nameof(ImageDisplayer)}: Show");
            gameObject.SetActive(true);
            SetImage(imageId);
        }

        public void Hide()
        {
            Debug.Log($"{nameof(ImageDisplayer)}: Hide");
            gameObject.SetActive(false);
        }
    }
}