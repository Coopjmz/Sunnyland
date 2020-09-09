using UnityEngine;

using Sunnyland.Game.Interactables;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerInteract : MonoBehaviour
	{
		internal Transform Ladder { get; set; }
		internal Interactable Interactable { private get; set; }

		internal void Interact()
		{
			if (Interactable == null) return;

			Interactable.Interact();
			Interactable = null;
		}
	}
}