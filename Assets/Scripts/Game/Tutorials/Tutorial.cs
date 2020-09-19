using UnityEngine;

namespace Sunnyland.Game.Tutorials
{
	[RequireComponent(typeof(BoxCollider2D))]
	sealed class Tutorial : MonoBehaviour
	{
		[Header("Text")]
		[TextArea] [SerializeField] private string _text = "[Enter text here]";

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (!collider.CompareTag("Player")) return;

			UI.Instance.UpdateText("Tutorial", _text);
			TutorialManager.Instance.DisplayTutorial(true);
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if (!collider.CompareTag("Player")) return;

			TutorialManager.Instance.DisplayTutorial(false);
			Destroy(gameObject);
		}
	}
}