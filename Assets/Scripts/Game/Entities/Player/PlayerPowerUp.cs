using System.Collections;

using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerPowerUp : MonoBehaviour
	{
		private PlayerController _player;

		public bool IsActive { get; private set; }

		private void Awake() => _player = GetComponent<PlayerController>();

		public void Activate()
		{
			IsActive = true;

			_player.Movement.JumpForce = _player.Data.PowerUpJumpForce;
			transform.localScale *= _player.Data.PowerUpScale;
			_player.SpriteRenderer.color = Color.yellow;

			StartCoroutine(Timer());
		}

		private void Deactivate()
		{
			IsActive = false;

			_player.Movement.JumpForce = _player.Data.DefaultJumpForce;
			transform.localScale /= _player.Data.PowerUpScale;
			_player.SpriteRenderer.color = Color.white;
		}

		private IEnumerator Timer()
		{
			float timer = _player.Data.Duration;
			do
			{
				if (timer == 2f)
					StartCoroutine(_player.Animation.Flicker(2f));

				timer--;
				yield return new WaitForSeconds(1f);
			}
			while (timer > 0f);

			Deactivate();
		}
	}
}