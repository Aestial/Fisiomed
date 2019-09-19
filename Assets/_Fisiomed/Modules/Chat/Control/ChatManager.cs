using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using TMPro;
using Loader;

namespace Fisiomed.Chat
{
	public class ChatManager : MonoBehaviour 
	{	
		[Header("Data")]
		[SerializeField] string defaultUrl;
		[Header("Visual")]
		[SerializeField] GameObject messageBubblePrefab;
		[SerializeField] GameObject questionBubblePrefab;
		[SerializeField] Transform containerPanel;
		[SerializeField] Button nextButton;
		List<Sprite> characterSprites = new List<Sprite>();
		int lenght = 0, index = 0;
		Chat chat;
		#region Public Methods
		public void NextBubble()
		{
			if (++index < lenght)
				ShowElement(index);
			if (index >= lenght - 1)
				nextButton.interactable = false;
		}
		#endregion
		#region MonoBehaviour
		void Start()
		{
			AppManager.Instance.ShowLoader(true);
			string url = PlayerPrefs.GetString("url", defaultUrl);
			StartCoroutine(Downloader.Instance.LoadJSON(url, OnMetadataLoaded));
		}
		#endregion
		private void ShowElement(int index)
		{
			// Dialog dialog = chat.dialogs[index];
			Element element = chat.dialogue[index];
			Character character = chat.characters[element.character];
			Sprite sprite = characterSprites[element.character];
			switch ((ElementType)element.type)
			{
				case ElementType.Message:
					Message message = chat.messages[element.index];
					GameObject newMessage = Instantiate(messageBubblePrefab, containerPanel);
					MessageController messageC = newMessage.GetComponent<MessageController>();
					messageC.Set(message, character, sprite);
				break;
				case ElementType.Question:
					Question question = chat.questions[element.index];
					GameObject newQuestion = Instantiate(questionBubblePrefab, containerPanel);
					QuestionController questionC = newQuestion.GetComponent<QuestionController>();
					questionC.Set(question, character, sprite);
				break;
				default:
				break;
			}
			
		}

		void OnMetadataLoaded(string json)
		{
			chat = JsonUtility.FromJson<Chat>(json);
			Debug.Log(chat);
			Debug.Log(chat.dialogue);
			Debug.Log(chat.questions);
			lenght = chat.dialogue.Length;
			Character[] characters = chat.characters;						
			// Download Character Images
			StartCoroutine(DownloadSprites());
		}

		IEnumerator DownloadSprites()
		{
			for (int i = 0; i < chat.characters.Length; i++)
			{
				string url = chat.characters[i].imageUrl;
				yield return StartCoroutine(Downloader.Instance.LoadTexture(url, OnTextureLoaded));
			}
			yield return null;
			AppManager.Instance.ShowLoader(false);
			ShowElement(index);			
		}

		void OnTextureLoaded(Texture texture)
		{
			Texture2D tex = (Texture2D) texture;
			Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
			characterSprites.Add(sprite);
		}

		
	}
}
