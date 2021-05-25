using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageDisplayer : MonoBehaviour
    {
        private static ImageDisplayer current;
        
        public Image image;

        private const string LoadPath = "misc/si/";

        private void Awake()
        {
            current = this;

            if (image == null)
                image = GetComponent<Image>(); // try to look for it in this object 
            
            Hide();
        }

        private void SetImage(string imageId)
        {
            var fullPath = LoadPath + imageId;
            Debug.Log($"Loading image from {fullPath}");
            image.sprite = Resources.Load<Sprite>(fullPath);
        }
        
        public static void Show(string imageId)
        {
            current.gameObject.SetActive(true);
            current.SetImage(imageId);
        }

        public static void Hide()
        {
            Debug.Log("ImageDisplayer: hide");
            current.gameObject.SetActive(false);
        }
    }
}