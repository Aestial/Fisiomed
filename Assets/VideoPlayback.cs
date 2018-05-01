using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;

public class VideoPlayback : MonoBehaviour {

	private VideoPlayer videoPlayer;

	// Use this for initialization
	void Start () {
		this.videoPlayer = GetComponent<VideoPlayer>();
		Debug.Log(this.videoPlayer.clip.frameCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
