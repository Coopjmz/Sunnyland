using UnityEngine;

namespace Sunnyland.Game.Interactables
{
	abstract class Interactable : MonoBehaviour
	{
		[SerializeField] private string _interactText = "Press SPACE to interact";

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag("Player"))
			{
				UI.Instance.UpdateText("Interact", _interactText);
				UI.Instance.Display("Interact");

				if (Game.IsTutorialEnabled)
					Game.DisableTutorial();
			}
		}

		protected void OnTriggerExit2D(Collider2D collider)
		{
			if (collider.CompareTag("Player"))
				UI.Instance.Display("Interact", false);
		}

		public virtual void Interact() => UI.Instance.Display("Interact", false);
	}
}