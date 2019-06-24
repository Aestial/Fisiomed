using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ImageAlphaParent : MonoBehaviour 
{
	private Image[] children;
	private Image image;
	// Use this for initialization
	void Start () 
	{
		this.image = GetComponent<Image>();
		this.children = GetComponentsInChildren<Image>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < this.children.Length; i++)
		{
			this.children[i].color = this.image.color;
		}
	}
}
