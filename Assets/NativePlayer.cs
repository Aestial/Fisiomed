using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativePlayer : MonoBehaviour {

	public void PlayVideoFullscreen(string name)
    {
        Handheld.PlayFullScreenMovie(name, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
    }
}
