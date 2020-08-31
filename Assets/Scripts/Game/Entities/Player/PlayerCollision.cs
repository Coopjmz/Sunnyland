using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	sealed class PlayerCollision : MonoBehaviour
	{
		/*
		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag("Collectable"))
				Cherries++;
			else if (collider.CompareTag("Power Up"))
			{
				//Buff player
				_powerUp = true;
				_jumpForce += _jumpBoost;
				transform.localScale *= _scaleBoost;
				_spriteRenderer.color = Color.yellow;

				StartCoroutine(PowerUpTimer());
			}
			else if (collider.CompareTag("Ladder"))
				_ladder = collider.transform;
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if (collider.CompareTag("Ladder"))
			{
				if (_climbing) SetClimbing(false);
				_ladder = null;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Enemy"))
			{
				Collider2D jumpOnEnemy = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,
					0f, Vector2.down, .5f, _enemy).collider;

				if (jumpOnEnemy || _powerUp)
				{
					collision.gameObject.GetComponent<Enemy>().Die();

					if (jumpOnEnemy) Jump(false);
				}
				else Die();
			}
		}

		private IEnumerator PowerUpTimer()
		{
			float timer = _powerUpTime;
			do
			{
				if (timer == 2f)
					StartCoroutine(Flicker(2f));

				timer--;
				yield return new WaitForSeconds(1f);
			}
			while (timer > 0f);

			//Nerf player
			_powerUp = false;
			_jumpForce -= _jumpBoost;
			transform.localScale /= _scaleBoost;
			_spriteRenderer.color = Color.white;
		}
		*/
	}
}