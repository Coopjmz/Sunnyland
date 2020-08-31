using UnityEngine;

using static Sunnyland.Game.UI.UI;

namespace Sunnyland.Game.Interactables
{
	sealed class Sign : Interactable
	{
		[TextArea] [SerializeField] private string _text = "[Enter text here]";

		private new void OnTriggerExit2D(Collider2D collider)
		{
			base.OnTriggerExit2D(collider);
			if (collider.CompareTag("Player"))
				Display("Sign", false);
		}

		internal override void Interact()
		{
			base.Interact();
			UpdateText("Sign", _text);
			Display("Sign");
		}
	}
}