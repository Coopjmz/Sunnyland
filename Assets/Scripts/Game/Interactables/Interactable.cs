using UnityEngine;

using static Sunnyland.Game.UI.UI;

namespace Sunnyland.Game.Interactables
{
	abstract class Interactable : MonoBehaviour
	{
		[SerializeField] private string _interactText = "Press SPACE to interact";

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag("Player"))
			{
				UpdateText("Interact", _interactText);
				Display("Interact");

				if (Game.IsTutorialEnabled)
					Game.DisableTutorial();
			}
		}

		private protected void OnTriggerExit2D(Collider2D collider)
		{
			if (collider.CompareTag("Player"))
				Display("Interact", false);
		}

		internal virtual void Interact() => Display("Interact", false);
	}
}