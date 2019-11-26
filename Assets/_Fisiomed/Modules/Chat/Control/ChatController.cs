using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Loader;

namespace Fisiomed.Chat
{
	public class ChatController : Singleton<ChatController> 
	{	
		[Header("Visual")]
		[SerializeField] Canvas canvas;
		[SerializeField] GameObject messageBubblePrefab;
		[SerializeField] GameObject questionBubblePrefab;
		[SerializeField] Transform containerPanel;
		[SerializeField] Button nextButton;
		public List<Sprite> characterSprites = new List<Sprite>();
		int lenght = 0, index = 0;
		Chat chat;
		#region Public Methods
		public void Set(Chat chat)
		{
			this.chat = chat;
			Debug.Log(chat);
			Debug.Log(chat.sequence);
			Debug.Log(chat.questions);
			lenght = chat.sequence.Length;
			Character[] characters = chat.characters;						
			// Download Character Images
			StartCoroutine(DownloadSprites());
		}
		public void NextBubble()
		{
			if (++index < lenght)
				ShowElement(index);
			if (index >= lenght - 1)
				nextButton.interactable = false;
			if (index >= lenght)
				canvas.enabled = false;
		}
		#endregion
		private void ShowElement(int index)
		{
			// Dialog dialog = chat.dialogs[index];
			Element element = chat.sequence[index];
			Character character = chat.characters[element.character];
			Sprite sprite = characterSprites[element.character];
			switch ((ElementType)element.type)
			{
				case ElementType.Message:
					Message message = chat.messages[element.index];
					GameObject newMessage = Instantiate(messageBubblePrefab, containerPanel);
					MessageController messageC = newMessage.GetComponent<MessageController>();
					messageC.Set(message, character, sprite);
					nextButton.interactable = true;
				break;
				case ElementType.Question:
					Question question = chat.questions[element.index];
					GameObject newQuestion = Instantiate(questionBubblePrefab, containerPanel);
					QuestionController questionC = newQuestion.GetComponent<QuestionController>();
					questionC.Set(this, question, character, sprite);
					nextButton.interactable = false;
				break;
				case ElementType.Interactive:
					string url;
					switch(Application.platform)
					{
						case RuntimePlatform.Android:
							url = chat.interactives[element.index].url;
							break;
						default:
							url =  chat.interactives[element.index].ios;
							break;
					}
					InteractiveController.Instance.Play(url);
				break;
				default:
				break;
			}			
		}		
		IEnumerator DownloadSprites()
		{
			for (int i = 0; i < chat.characters.Length; i++)
			{
				string url = chat.characters[i].imageUrl;
				yield return StartCoroutine(Downloader.Instance.LoadTexture(url, OnTextureLoaded));
			}
			yield return null;
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
