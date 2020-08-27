using UnityEngine;

namespace Levels
{
	sealed class Sign : MonoBehaviour
	{
		[TextArea] [SerializeField] private string text = "[Enter text here]";

		private void OnTriggerExit2D(Collider2D collider)
		{
			if(collider.CompareTag("Player"))
				UI.Display("Sign", false);
		}

		public void Read()
		{
			UI.UpdateText("Sign", text);
			UI.Display("Sign");
		}
	}
}