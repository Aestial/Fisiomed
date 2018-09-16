/*
TODO: Doesn't work for ExecuteInEditMode as desired, otherwise is useless.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;

[ExecuteInEditMode]
public class VideoPlayback : MonoBehaviour 
{
	[Range(0,1)][SerializeField] private float desiredPercent;
	private float percentage;
	private VideoPlayer videoPlayer;
	private long frameCount;
	private long currentFrame;

	// Use this for initialization
	void Start () 
	{
		this.videoPlayer = GetComponent<VideoPlayer>();
		
		this.frameCount = (long)this.videoPlayer.clip.frameCount;
		Debug.Log(this.frameCount);

		this.currentFrame = this.frameCount / 2;
		Debug.Log(this.currentFrame);

		this.videoPlayer.frame = this.currentFrame;
		// This breaks Unity editor:
		// this.videoPlayer.Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// this.currentFrame = (long)Mathf.RoundToInt(this.percentage * (this.frameCount - 1));
		this.currentFrame = this.videoPlayer.frame;
		Debug.Log("Current Frame: " + this.currentFrame);
		if (this.desiredPercent != this.percentage) {
			this.percentage = this.desiredPercent;
		} else {
			this.percentage = this.currentFrame / (float)this.frameCount;
		}
		Debug.Log("Playback Percentage: " + this.percentage);
		// This breaks Unity editor also:
		// this.videoPlayer.frame = 0;
		// this.videoPlayer.frame = this.currentFrame;
		// this.videoPlayer.Play();
		// this.videoPlayer.Pause();
	}
}
