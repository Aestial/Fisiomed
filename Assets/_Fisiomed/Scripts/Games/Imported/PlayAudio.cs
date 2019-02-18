using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour 
{
	[SerializeField] AudioClip[] clips;
	AudioSource audioSource;
	
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayClipIndexOneShot(int index)
	{
		audioSource.PlayOneShot(clips[index]);
	}

	public void PlayClipIndex(int index)
	{
		audioSource.clip = clips[index];
		audioSource.Play();
	}

	public void StopAllClips()
	{
		audioSource.Stop();
	}
}
