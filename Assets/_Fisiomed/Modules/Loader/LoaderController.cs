using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderController : Singleton<LoaderController>
{    
    [SerializeField] Canvas loaderCanvas;
    [SerializeField] Animator animator;
    [SerializeField] float resetTime = 1.0f;
    float enabledTime, disabledTime;
    public void SetLoader(bool enabled)
    {
        loaderCanvas.enabled = enabled;
        if (enabled)
        {
            enabledTime = Time.time;
        }
        else
        {
            disabledTime = Time.time;
        }
        if (enabledTime - disabledTime > resetTime)
        {
            animator.Rebind();
        }            
    }
}
