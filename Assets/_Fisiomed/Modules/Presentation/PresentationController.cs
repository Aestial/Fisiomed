using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fisiomed.Case;
using Loader;

public class PresentationController : Singleton<PresentationController>
{
    [SerializeField] Canvas canvas;
    [SerializeField] TMP_Text text;
    [SerializeField] Image image;
    public void Set(Info info)
    {                
        StartCoroutine(Downloader.Instance.LoadTexture(info.imageUrl, OnTextureLoaded));
        text.text = TextProcess.Process(info.description);
    }
    public void Close()
    {
        canvas.enabled = false;
    }
    void OnTextureLoaded(Texture texture)
    {
        Texture2D tex = (Texture2D) texture;
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);		
        image.sprite = sprite;        
    }
}
