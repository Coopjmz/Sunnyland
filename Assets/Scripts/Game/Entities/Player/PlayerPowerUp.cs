using System.Collections;
using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerPowerUp : MonoBehaviour
	{
		[SerializeField] private float _jumpBoost = 10f;
		[SerializeField] private float _scaleBoost = 1.5f;
		[SerializeField] private float _duration = 10f;

		private PlayerController _player;

		internal bool IsActive { get; private set; }

		private void Awake() => _player = GetComponent<PlayerController>();

		internal void Activate()
		{
			IsActive = true;

			_player.Movement.JumpForce += _jumpBoost;
			transform.localScale *= _scaleBoost;
			_player.SpriteRenderer.color = Color.yellow;

			StartCoroutine(Timer());
		}

		private void Deactivate()
		{
			IsActive = false;

			_player.Movement.JumpForce -= _jumpBoost;
			transform.localScale /= _scaleBoost;
			_player.SpriteRenderer.color = Color.white;
		}

		private IEnumerator Timer()
		{
			float timer = _duration;
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