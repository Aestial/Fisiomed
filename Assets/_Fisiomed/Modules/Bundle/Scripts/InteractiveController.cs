using UnityEngine;
using UnityEngine.UI;
using Loader;

namespace Fisiomed.Chat
{
    public class InteractiveController : Singleton<InteractiveController>
    {
        [SerializeField] Transform parent;
        [Header("Customize Components")]
        [SerializeField] Image characterImage;
        [SerializeField] Image characterBubble;
        [SerializeField] Image textBubble;
        [SerializeField] Image textBubbleShadow;        
        [SerializeField] TMPro.TMP_Text text;
        string url;
        bool firstTime = true;
        public void Set(Interactive interactive, Character character, Sprite sprite)
        {           
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    url = interactive.ios;
                    break;
                // Android Default
                case RuntimePlatform.Android:
                default:
                    url = interactive.and;
                    break;
            }
            SetColors(character.textColor, character.textBColor, character.faceBColor);
            SetContent(interactive.text);
            SetSide(character.side);
            SetSprite(sprite);
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
