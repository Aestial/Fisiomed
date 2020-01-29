using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace Fisiomed.Video 
{
    public enum VideoMode
    {
        Off,
        Fullscreen
    };

    /// <summary>
    /// <c>VideoPlayerController</c> controls the VideoPlayer object and components
    /// It is used to play videos with two modes: OnTarget and Fullscreen
    /// 
    /// \Author: Jaime Hernandez
    /// \Date: May 2019
    /// </summary>
    public class VideoPlayerController : Singleton<VideoPlayerController>
    {
        public VideoPlayer video;
        public bool isActive;
        [SerializeField] VideoMode currentMode = VideoMode.Off;
        [SerializeField] FullscreenUIController fullscreenHUD;
        public UnityAction<int, int> onPrepared;
        public UnityAction onEndReached;
        public UnityAction onPlaying;
        public UnityAction onPaused;
        public UnityAction onPositionChanged;

        Notifier notifier;

        #region Properties
        public bool IsPrepared
        {
            get { return video.isPrepared; }
        }

        public bool IsPlaying
        {
            get { return video.isPlaying; }
        }

        public bool IsLooping
        {
            get { return video.isLooping; }
            set { video.isLooping = value; }
        }

        public bool IsDone { get; private set; }

        public double Time
        {
            get { return video.time; }
            set { video.time = value; }
        }

        public double Length
        {
            get { return video.length; }
        }
        #endregion

        #region MonoBehaviourMethods
        void Awake()
        {
            notifier = new Notifier();            
            //notifier.Subscribe(NotificationEvents.ON_ASSET_CLOSED, HandleOnAssetClosed);
        }

        void Start()
        {
            SwitchToMode(currentMode);
        }
       
        void OnEnable()
        {
            onEndReached += OnEndReached;
            onPrepared += OnPrepared;

            video.prepareCompleted += PrepareCompleted;
            video.loopPointReached += LoopPointReached;
            video.started += Started;
            video.seekCompleted += SeekCompleted;
        }

        void OnDisable()
        {
            onEndReached -= OnEndReached;
            onPrepared -= OnPrepared;

            video.prepareCompleted -= PrepareCompleted;
            video.loopPointReached -= LoopPointReached;
            video.started -= Started;
            video.seekCompleted -= SeekCompleted;
        }

        void OnDestroy()
        {
            notifier.UnsubcribeAll();
            //base.OnDestroy();
        }
        #endregion

        #region PublicMethods
        public void Load(string url)
        {
            isActive = true;
            video.url = url;
            video.Prepare();
        }

        public void Play()
        {
            if (!IsPrepared) return;
            video.Play();
        }

        public void Pause()
        {
            video.Pause();
            onPaused();
        }

        public void Seek(float time)
        {
            Time = time;
        }

        public void Seek(double time)
        {
            Time = time;
        }

       public void SwitchToMode(VideoMode mode)
        {
            Log.Color("Switch to: " + mode, this);
            switch (mode)
            {               
                case VideoMode.Fullscreen:
                    SetOrientation(IsPrepared);
                    fullscreenHUD.Show(IsPrepared);
                    break;
                default:
                    SetOrientation(false);
                    fullscreenHUD.Show(false);
                    break;
            }
            currentMode = mode;
        }
        #endregion

        #region NotifierHandlers
        void HandleOnAssetClosed(params object[] args)
        {
            SwitchToMode(VideoMode.Off);
            video.Stop();
            isActive = false;
            Log.Color("Closed, Switched off.", this);
        }
        #endregion


        #region PrivateMethods
        private void OnPrepared(int width, int height)
        {
            IsDone = false;
            SwitchToMode(currentMode);
            Play();
        }

        private void OnEndReached()
        {
            IsDone = !IsLooping;
        }

        private void SetOrientation(bool canRotate)
        {
            if (canRotate)
            {
                Screen.orientation = ScreenOrientation.AutoRotation;
                Screen.autorotateToPortrait = true;
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;
                Screen.autorotateToPortraitUpsideDown = false;
            }
            else 
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
        }
        #endregion

        #region VideoPlayerEvents
        private void LoopPointReached(VideoPlayer vp)
        {
            onEndReached();
        }

        private void PrepareCompleted(VideoPlayer vp)
        {
            onPrepared((int)vp.width, (int)vp.height);
        }

        private void Started(VideoPlayer vp)
        {
            onPlaying();
        }

        private void SeekCompleted(VideoPlayer vp)
        {
            onPositionChanged();
        }
        #endregion

    }
}
