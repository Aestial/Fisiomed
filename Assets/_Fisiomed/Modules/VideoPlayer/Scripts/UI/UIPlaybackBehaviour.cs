using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Fisiomed.Video
{
    /// <summary>
    /// <c>UIPlaybackBehaviour</c> controls VideoPlayer's playback UI elements
    /// state depending on the VideoPlayer current state.
    /// 
    /// \Author: Jaime Hernandez
    /// \Date: May 2019
    /// </summary>
    public class UIPlaybackBehaviour : MonoBehaviour
    {
        public VideoManager video;

        [Header("UI Elements")]
        [SerializeField] Slider slider = default;
        [SerializeField] TMP_Text timeText = default;
        [SerializeField] Image playButton = default;
        [SerializeField] Image pauseButton = default;

        bool isDragging;

        public void SetDragging(bool value)
        {
            isDragging = value;
        }

        public void SetPosition()
        {
            video.Seek(slider.value * video.Length);
        }

        void OnEnable()
        {
            video.onPaused += OnPaused;
            video.onPlaying += OnPlaying;
            video.onEndReached += OnEndReached;
            video.onPositionChanged += OnPositionChanged;
        }

        void OnDisable()
        {
            video.onPaused -= OnPaused; 
            video.onPlaying -= OnPlaying;
            video.onEndReached -= OnEndReached;
            video.onPositionChanged -= OnPositionChanged;
        }

        void Start()
        {
            TogglePlayButton(false);
        }

        void Update()
        {
            if (video.IsPlaying)
            {
                SetTimer();
                OnPositionChanged();
            }
        }

        #region EventHandlers
        private void OnPlaying()
        {
            TogglePlayButton(true);
        }

        private void OnPaused()
        {
            TogglePlayButton(false);
        }

        private void OnEndReached()
        {
            if (!video.IsLooping)
                TogglePlayButton(false);
        }

        private void OnPositionChanged()
        {
            if (!isDragging)
                slider.value = (float) (video.Time / video.Length);
        }
        #endregion

        private void TogglePlayButton(bool isPlaying)
        {
            playButton.enabled = !isPlaying;
            pauseButton.enabled = isPlaying;

            CanvasGroup playCanvasGroup = playButton.GetComponent<CanvasGroup>();
            CanvasGroup pauseCanvasGroup = pauseButton.GetComponent<CanvasGroup>();

            if (playCanvasGroup)
            {
                playCanvasGroup.alpha = !isPlaying ? 1.0f : 0.0f;
                playCanvasGroup.interactable = !isPlaying;
                playCanvasGroup.blocksRaycasts = !isPlaying;
            }
            if (pauseCanvasGroup)
            {
                pauseCanvasGroup.alpha = isPlaying ? 1.0f : 0.0f;
                pauseCanvasGroup.interactable = isPlaying;
                pauseCanvasGroup.blocksRaycasts = isPlaying;
            }
        }

        private void SetTimer()
        {
            TimeSpan t = TimeSpan.FromSeconds(video.Time);
            TimeSpan l = TimeSpan.FromSeconds(video.Length);
            string timeStr = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            string lengthStr = string.Format("{0:D2}:{1:D2}", l.Minutes, l.Seconds);
            timeText.text = timeStr + " / " + lengthStr;
        }
    }
}
