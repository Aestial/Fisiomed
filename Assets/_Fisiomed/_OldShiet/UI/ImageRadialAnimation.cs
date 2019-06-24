using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageRadialAnimation : MonoBehaviour 
{
	[SerializeField] private float speed;
	private float fill;
	private Image image;

	// Use this for initialization
	void Start () 
	{
		this.image = GetComponent<Image>();
		// StartCoroutine(this.AnimateFillCoroutine());	
	}

	// Coroutine not needed because of smooth animation with Update method
	// private IEnumerator AnimateFillCoroutine()
	// {
	// 	this.ChangeImageFill(Time.time);
	// }
	
	// Change image fill function (- COS easing -)
	private void ChangeImageFill(float time) 
	{
		float newFill = Mathf.Abs(Mathf.Cos(time * speed));
		this.image.fillClockwise = newFill < this.fill;
		this.image.fillAmount = newFill;
		this.fill = newFill;
	}
	// Update is called once per frame
	void Update () 
	{
		this.ChangeImageFill(Time.time);
	}
}
