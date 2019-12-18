using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TMPro;
using Loader;

namespace Fisiomed.Main
{
    [RequireComponent(typeof(Button))]
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] string nextScene;
        [SerializeField] string defaultUrl;
        [SerializeField] TMP_Text textView;
        [SerializeField] Gradient2 gradient;
        [SerializeField] Image[] dots;
        [SerializeField] Image image;
        Button button;

        void Awake()
        {
            button = GetComponent<Button>();
        }

        public void SetOption(Option option)
        {            
            SetColor(option.colorA, option.colorB);
            SetSprite(option.image);
            SetText(option.title);
            SetUrl(option);
        }

        void SetColor(string a, string b)
        {
            Color colorA = new Color();
            Color colorB = new Color();
            ColorUtility.TryParseHtmlString (a, out colorA);
            ColorUtility.TryParseHtmlString (b, out colorB);
            UnityEngine.Gradient effectGradient = new UnityEngine.Gradient() { 
                colorKeys = new GradientColorKey[] { 
                    new GradientColorKey(colorA, 0),
                    new GradientColorKey(colorB, 1) 
                } 
            };
            gradient.EffectGradient = effectGradient;
            dots[0].color = colorA;
            dots[2].color = colorB;
            dots[1].color = Color.Lerp(colorA, colorB, 0.5f);
        }

        void SetSprite(string url)
        {
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
                StartCoroutine(Downloader.Instance.LoadTexture(url, OnTextureLoaded));    
        }

        void SetText(string text)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
                textView.text = text;
        }

        void SetUrl(Option o)
        {
            if (o.isActive && (!string.IsNullOrEmpty(o.url) && !string.IsNullOrWhiteSpace(o.url)))
            {
                button.interactable = true;
                button.onClick.AddListener(() => ChangeSceneWithUrl(o.url));
            }
        }

        void OnTextureLoaded(Texture texture)
		{
			Texture2D tex = (Texture2D) texture;
			Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
			image.sprite = sprite;
		}

        void ChangeSceneWithUrl(string url)
        {
            AppManager.Instance.GetComponent<AppData>().PassUrl(url);
            AppManager.Instance.GetComponent<AppManager>().ChangeScene(nextScene);
        }
    }
}
