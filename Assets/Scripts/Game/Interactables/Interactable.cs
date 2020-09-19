using UnityEngine;

using Sunnyland.Game.Tutorials;

namespace Sunnyland.Game.Interactables
{
	abstract class Interactable : MonoBehaviour
	{
		[SerializeField] private string _interactText = "Press SPACE to interact";

		private bool _tutorialHidden;

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (!collider.CompareTag("Player")) return;

			UI.Instance.UpdateText("Interact", _interactText);
			UI.Instance.Display("Interact");

			if (TutorialManager.Instance.IsTutorialDisplayed)
			{
				_tutorialHidden = true;
				TutorialManager.Instance.DisplayTutorial(false);
			}
		}

		protected void OnTriggerExit2D(Collider2D collider)
		{
			if (!collider.CompareTag("Player")) return;

			UI.Instance.Display("Interact", false);

			if (_tutorialHidden)
			{
				_tutorialHidden = false;
				TutorialManager.Instance.DisplayTutorial(true);
			}
		}

		public virtual void Interact() => UI.Instance.Display("Interact", false);
	}
}