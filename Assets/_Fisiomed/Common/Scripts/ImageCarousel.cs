using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageCarousel : MonoBehaviour 
{
	[SerializeField] private float waitTime = default;
	[SerializeField] private Sprite[] sprites = default;
	private int index;
	private Image image;

	// Use this for initialization
	void Start () 
	{
		this.index = 0;
		this.image = GetComponent<Image>();
		StartCoroutine(this.ChangeSpriteCoroutine());
	}

	// Coroutine for waiting an certain amount of time
	private IEnumerator ChangeSpriteCoroutine()
	{
		this.ChangeSprite(this.index);
		// Wait and call it again (infinite loop)
		yield return new WaitForSeconds(this.waitTime);
		StartCoroutine(this.ChangeSpriteCoroutine());
	}

	// Change sprite and index function
	private void ChangeSprite(int index)
	{
		this.image.sprite = sprites[index];
		this.index = (index == this.sprites.Length - 1) ? 0 : index + 1; 
	}
	
	// Update is called once per frame
	// void Update () 
	// {
		
	// }
}
