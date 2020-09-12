﻿using UnityEngine;

namespace Sunnyland.Game.Interactables
{
	sealed class Sign : Interactable
	{
		[TextArea] [SerializeField] private string _text = "[Enter text here]";

		private new void OnTriggerExit2D(Collider2D collider)
		{
			base.OnTriggerExit2D(collider);
			if (collider.CompareTag("Player"))
				UI.Instance.Display("Sign", false);
		}

		public override void Interact()
		{
			base.Interact();
			UI.Instance.UpdateText("Sign", _text);
			UI.Instance.Display("Sign");
		}
	}
}