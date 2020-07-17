using UnityEngine;
using UnityEngine.UI;
using Loader;
using Fisiomed.Video;

namespace Fisiomed.Chat
{
    using App;

    public class VideoController : MonoBehaviour
    {
        [Header("Customize Components")]
        [SerializeField] Image characterImage = default;
        [SerializeField] Image characterBubble = default;
        [SerializeField] Image textBubble = default;
        [SerializeField] Image textBubbleShadow = default;
        [SerializeField] TMPro.TMP_Text text = default;
        string url;
        string filename;
        bool firstTime = true;
        public void Set(Video video, Character character, Sprite sprite)
        {
            url = video.url;
            string[] split = url.Split('/');
            filename = split[split.Length - 1];
            Log.Color("Video name in storage: " + filename, this);

            SetColors(character.textColor, character.textBColor, character.faceBColor);
            SetContent(video.text);
            SetSide(character.side);
            SetSprite(sprite);
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
        void SetColors(string textCol, string textBack, string faceBack)
        {
            Color textColor = new Color();
            Color textBColor = new Color();
            Color faceBColor = new Color();
            ColorUtility.TryParseHtmlString(textCol, out textColor);
            ColorUtility.TryParseHtmlString(textBack, out textBColor);
            ColorUtility.TryParseHtmlString(faceBack, out faceBColor);
            SetColors(textColor, textBColor, faceBColor);
        }
        void SetColors(Color textColor, Color textBColor, Color charBColor)
        {
            text.color = textColor;
            textBubble.color = textBColor;
            characterBubble.color = charBColor;
        }
        void SetContent(string content)
        {
            text.text = TextProcess.Process(content);
        }
        void SetSide(string input)
        {
            BubbleSide side = input == "left" ? BubbleSide.Left : BubbleSide.Right;
            float radius = 60f;
            SetSide(side, radius);
        }
        void SetSide(BubbleSide side, float radius)
        {
            Vector4 radiusRect = new Vector4(radius, radius, radius, radius);
            switch (side)
            {
                case BubbleSide.Right:
                    radiusRect.y = 0.0f;
                    break;
                default:
                    radiusRect.x = 0.0f;
                    break;
            }
            textBubbleShadow.GetComponent<FreeModifier>().Radius = radiusRect;
            textBubble.GetComponent<FreeModifier>().Radius = radiusRect;
            if (side == BubbleSide.Right)
                transform.GetChild(0).SetAsLastSibling();
        }
        void SetSprite(Sprite sprite)
        {
            characterImage.sprite = sprite;
        }
    }
}
