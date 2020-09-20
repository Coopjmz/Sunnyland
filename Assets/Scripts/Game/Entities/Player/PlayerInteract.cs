using UnityEngine;

using Sunnyland.Game.Interactables;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerInteract : MonoBehaviour
	{
		public Ladder Ladder { get; set; }
		public Interactable Interactable { private get; set; }

		public void Interact()
		{
			if (Interactable == null) return;

			Interactable.Interact();
			Interactable = null;
		}
	}
}