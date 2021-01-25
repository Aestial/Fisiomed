using UnityEngine;
using Loader;
using Fisiomed.Video;

namespace Fisiomed.Chat
{
    using App;

    public class VideoController : BubbleController
    {
        string url;
        string filename;
        bool firstTime = true;
        public void Set(Video video, Character character, Sprite sprite)
        {
            url = video.url;
            string[] split = url.Split('/');
            filename = split[split.Length - 1];
            Log.Color("Video name in storage: " + filename, this);
            base.Set(video.text, character, sprite);
        }
        public void Play()
        {
            AppManager.Instance.ShowLoader(true);
            StartCoroutine(Downloader.Instance.LoadVideo(url, filename, OnDownloadCompleted));
        }
        void OnDownloadCompleted(string filePath)
        {
            VideoManager.Instance.Load(filePath);
            if (firstTime)
            {
                ChatController.Instance.NextBubble();
                firstTime = false;
            }            
        }        
    }
}
