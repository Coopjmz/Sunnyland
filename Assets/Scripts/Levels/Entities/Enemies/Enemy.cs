using UnityEngine;

namespace Levels
{
	abstract class Enemy : Entity
	{
		private protected AudioSource _deathSFX;

		private void Death()
		{
			Destroy(gameObject);
		}

		internal override void Kill()
		{
			//Death animation
			enabled = false;
			_rigidBody.bodyType = RigidbodyType2D.Static;
			_boxCollider.enabled = false;
			_animator.SetTrigger("Death");

			_deathSFX.Play();
		}
	}
}