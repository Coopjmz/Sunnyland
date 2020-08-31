using UnityEngine;

namespace Sunnyland.Game.Entities.Enemies
{
	abstract class Enemy : Entity
	{
		private protected AudioSource DeathSFX { private get; set; }

		internal override void Die()
		{
			//Death animation
			enabled = false;
			_rigidbody.bodyType = RigidbodyType2D.Static;
			_boxCollider.enabled = false;
			_animator.SetTrigger("Death");

			DeathSFX.Play();
		}

		private void Death() => Destroy(gameObject);
	}
}