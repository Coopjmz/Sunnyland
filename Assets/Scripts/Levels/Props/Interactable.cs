using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
	sealed class Interactable : MonoBehaviour
	{
		[SerializeField] private UnityEvent action = default;

		private bool _inRange;

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.CompareTag("Player"))
			{
				_inRange = true;
				UI.Display("Interact");

				if(Game.IsTutorialEnabled)
					Game.DisableTutorial();
			}
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if(collider.CompareTag("Player"))
			{
				_inRange = false;
				UI.Display("Interact", false);
			}
		}

		private void Update()
		{
			if(Input.GetButtonDown("Interact") && Game.IsInputEnabled && _inRange)
			{
				UI.Display("Interact", false);
				action.Invoke();
			}
		}
	}
}