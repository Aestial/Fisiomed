using UnityEngine;
using UnityEngine.UI;

namespace Fisiomed.Video
{
    /// <summary>
	/// <c>FullscreenImageResize</c> sets correct aspect ratio for video Image
    /// object on Fullscreen Canvas. Computes current video aspect ratio from
    /// its width and height.
	/// 
	/// \Author: Jaime Hernandez
	/// \Date: May 2019
	/// </summary>
    public class FullscreenImageResize : MonoBehaviour
    {
        public VideoPlayerController video;

        [Header("UI Elements")]
        [SerializeField] RawImage videoOutputImage;
        [SerializeField] AspectRatioFitter aspectRatioFitter;

        void Awake()
        {
            videoOutputImage.enabled = false;
        }

        void OnEnable()
        {
            video.onPrepared += EnableVideoImage;
        }

        void OnDisable()
        {
            video.onPrepared -= EnableVideoImage;
        }

        void EnableVideoImage(int width, int height)
        {
            videoOutputImage.enabled = true;
            float aspectRatio = width / (float)height;
            aspectRatioFitter.aspectRatio = aspectRatio;
            Log.Color("Aspect Ratio: " + aspectRatio, this);
        }
    }
}
