using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using TMPro;

public class ChatManager : MonoBehaviour {

	/*Creamos una lista de "Message", la cual nos ayudara a instanciar
	cada diálogo, entre el doctor, paciente, familiar y jugador. El tamaño
	ha de depender del numero de dialogos (ver archivo dialogos*)*/

	List<Message> messageDialogosList = new List<Message>();

	public GameObject chatPanel;
	public GameObject dialogos_Dr;
	public GameObject dialogos_Paciente;
	public GameObject dialogos_Usuario;
	public GameObject dialogos_Familiar;
	[SerializeField] private TMP_Text dialogos_Dr_Text;
	[SerializeField] private TMP_Text dialogos_Paciente_Text;
	[SerializeField] private TMP_Text dialogos_Usuario_Text;
	[SerializeField] private TMP_Text dialogos_Familiar_Text;

	//Este contador, es solo ayuda visual, sirve para mostrar en la pantalla 
	//burbujas al azar
	private int _numRan;

	public void SiguienteBurbujaChat()
	{
		/*Solo sirve de ayuda visual, todavia no es lo que se planea con los dialogos de verdad*/
		_numRan = Random.Range (0, 4);
		if (_numRan == 0) {
			//Dialogo del Dr.
			PublicaMenssageChat (dialogos_Dr, dialogos_Dr_Text,"Bienvenido!");
		} else if (_numRan == 1) {
			//Dialogo del Paciente
			PublicaMenssageChat (dialogos_Paciente, dialogos_Paciente_Text,"Buen Dia Dr!");
		} else if (_numRan == 2) {
			//Dialogo del Usuario.
			PublicaMenssageChat (dialogos_Usuario, dialogos_Usuario_Text,"Mi diagnostico es ...");
		} else if (_numRan == 3) {
			//Dialogo del Familiar
			PublicaMenssageChat (dialogos_Familiar, dialogos_Familiar_Text,"Ya lleva tiempo así!");
		}
	}

	public void PublicaMenssageChat(GameObject burbuja, TMP_Text dialogo, string mensaje)
	{
		//Se crea un nuevo mensaje y se añade a la lista
		Message newMessage = new Message();
		newMessage.dialogue_text = mensaje;

		//Instanciamos el mensaje
		dialogo.text = newMessage.dialogue_text;
		GameObject newDialogue = Instantiate(burbuja, chatPanel.transform);
		messageDialogosList.Add (newMessage);
	}
}
