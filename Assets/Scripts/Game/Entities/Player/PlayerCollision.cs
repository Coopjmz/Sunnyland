using UnityEngine;

using Sunnyland.Game.Interactables;
using Sunnyland.Game.Entities.Enemies;
using Sunnyland.Game.Map;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerCollision : MonoBehaviour
	{
		private PlayerController _player;

		private void Awake() => _player = GetComponent<PlayerController>();

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag("Interactable"))
				_player.Interact.Interactable = collider.GetComponent<Interactable>();
			else if (collider.CompareTag("Ladder"))
				_player.Interact.Ladder = collider.transform;
			else if (collider.CompareTag("Collectable"))
				_player.Stats.Cherries++;
			else if (collider.CompareTag("Power Up"))
				_player.PowerUp.Activate();
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if (collider.CompareTag("Interactable"))
				_player.Interact.Interactable = null;
			if (collider.CompareTag("Ladder"))
			{
				if (_player.Movement.Climbing)
					_player.Movement.SetClimbing(false);

				_player.Interact.Ladder = null;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Enemy"))
			{
				Collider2D jumpOnEnemy = Physics2D.BoxCast(_player.BoxCollider.bounds.center,
					_player.BoxCollider.bounds.size, 0f, Vector2.down, .1f, Layers.Enemy).collider;

				if (jumpOnEnemy || _player.PowerUp.IsActive)
				{
					collision.gameObject.GetComponent<Enemy>().Die();

					if (jumpOnEnemy) _player.Movement.Jump(sound: false);
				}
				else _player.Die();
			}
		}
	}
}