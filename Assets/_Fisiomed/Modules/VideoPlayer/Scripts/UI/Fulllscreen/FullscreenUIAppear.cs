using System.Collections;
using UnityEngine;

namespace Fisiomed.Video
{
    /// <summary>
	/// <c>FullscreenUIAppear</c> sets appear and disappear behaviour for the
    /// playback UI on Fullscreen mode. Animmates (dis)appearing effect when
    /// touch detected and after the given Wait Time.
	/// 
	/// \Author: Jaime Hernandez
	/// \Date: May 2019
	/// </summary>
    public class FullscreenUIAppear : MonoBehaviour
    {
        public VideoManager video;

        [SerializeField] CanvasGroup canvasGroup;

        [SerializeField] float waitTime = 3.0f;
        [SerializeField] float appearSpeed = 0.5f;
        [SerializeField] float disappearSpeed = 1.0f;

        bool isShown;
        bool hasTriggered;
        bool canTrigger;
        float startTime;

        void Start()
        {
            isShown = true;
            hasTriggered = false;
            startTime = Time.time;
        }

        void OnEnable()
        {
            video.onPlaying += OnPlaying;
            video.onEndReached += OnEndReached;
        }

        void OnDisable()
        {
            video.onPlaying -= OnPlaying;
            video.onEndReached -= OnEndReached;
        }

        void Update()
        {
    #if UNITY_EDITOR
            if (isShown && Input.GetMouseButton(0))
    #else
            if (isShown && Input.touchCount > 0)
    #endif
            {
                startTime = Time.time;
            }
    #if UNITY_EDITOR
            if (!isShown && Input.GetMouseButtonDown(0))
    #else
            if (!isShown && Input.touchCount > 0)
    #endif
            {
                Start();
                Appear(true);
            }
            if ((Time.time - startTime > waitTime) && canTrigger && !hasTriggered && isShown)
            {
                hasTriggered = true;
                Appear(false);
            }
    	}

        public void Appear(bool appear)
        {
            StartCoroutine(AppearCoroutine(appear));
        }

        private void OnPlaying()
        {
            canTrigger = true;
        }

        private void OnEndReached()
        {
            canTrigger = false;
        }

        IEnumerator AppearCoroutine(bool isAppearing)
        {
            float animTime = 0;
            float startValue = isAppearing ? 0.0f : 1.0f;
            float endValue = isAppearing ? 1.0f : 0.0f;
            float speed = isAppearing ? appearSpeed : disappearSpeed;
            while (animTime < 1.0f)
            {
                animTime += Time.deltaTime * speed;
                canvasGroup.alpha = Mathf.Lerp(startValue, endValue, animTime);
                yield return new WaitForEndOfFrame();
            }
            isShown = isAppearing;
            canvasGroup.interactable = isShown;
        }
    }
}
