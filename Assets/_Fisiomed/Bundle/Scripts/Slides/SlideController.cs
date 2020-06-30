using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class SlideController : MonoBehaviour
{
    [SerializeField] float appearSpeed = 0.5f;
    [SerializeField] float disappearSpeed = 1.0f;    
    [SerializeField] bool isVisible = default;
    [SerializeField] UnityEvent onFadedIn = default;
    [SerializeField] UnityEvent onFadedOut = default;
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        SetVisible(isVisible);
    }

    public void SetVisible(bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1.0f : 0.0f;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(true, onFadedIn));
    }
    public void FadeOut()
    {
        StartCoroutine(Fade(false, onFadedOut));
    }

    IEnumerator Fade(bool isFadeIn, UnityEvent callback)
    {
        if (isFadeIn != isVisible)
        {
            float animTime = 0;
            float startValue = isFadeIn ? 0.0f : 1.0f;
            float endValue = isFadeIn ? 1.0f : 0.0f;
            float speed = isFadeIn ? appearSpeed : disappearSpeed;
            canvasGroup.interactable = isFadeIn;
            while (animTime < 1.0f)
            {
                animTime += Time.deltaTime * speed;
                canvasGroup.alpha = Mathf.Lerp(startValue, endValue, animTime);
                yield return new WaitForEndOfFrame();
            }
            canvasGroup.blocksRaycasts = isFadeIn;                       
            isVisible = isFadeIn;            
            callback.Invoke();
        }        
    }
}
