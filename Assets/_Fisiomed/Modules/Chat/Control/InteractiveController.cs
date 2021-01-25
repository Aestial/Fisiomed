using UnityEngine;

namespace Fisiomed.Chat
{
    using App;
    using Loader;

    public class InteractiveController : BubbleController
    {
        string url;
        bool firstTime = true;
        public void Set(Interactive interactive, Character character, Sprite sprite)
        {           
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    url = interactive.ios;
                    break;
                case RuntimePlatform.WebGLPlayer:
                    url = interactive.web;
                    break;
                // Android Default
                case RuntimePlatform.Android:
                default:
                    url = interactive.and;
                    break;
            }
            base.Set(interactive.text, character, sprite);            
        }
        public void Play()
        {
            AppManager.Instance.ShowLoader(true);
            StartCoroutine(Downloader.Instance.LoadAsset(url, OnDownloadCompleted));
        }
        void OnDownloadCompleted(GameObject prefab)
        {
            GameObject currentGO = Instantiate(prefab);
            if (firstTime)
            {
                ChatController.Instance.NextBubble();
                firstTime = false;
            }
            AppManager.Instance.ShowLoader(false);
        }        
    }
}
