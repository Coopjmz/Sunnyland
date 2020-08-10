using UnityEngine;

namespace Levels
{
	class Sign: MonoBehaviour
	{
		//Varibles (initialized from Unity)
		[TextArea][SerializeField] string text = "[Enter text here]";

		//Methods
		void OnTriggerExit2D(Collider2D collider)
		{
			//If the player isn't near the sign
			if(collider.CompareTag("Player"))
				UI.Display("Sign", false);
		}

		//Events
		public void Read()
		{
			UI.UpdateText("Sign", text);
			UI.Display("Sign");
		}
	}
}