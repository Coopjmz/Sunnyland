using UnityEngine;

namespace Sunnyland.Game.Entities.Enemies
{
	abstract class Enemy : Entity
	{
		[Header("Stats")]
		[SerializeField] private protected float _speed = 5f;

		private protected AudioSource DeathSFX { private get; set; }

		internal override void Die()
		{
			//Death animation
			enabled = false;
			Rigidbody.bodyType = RigidbodyType2D.Static;
			BoxCollider.enabled = false;
			Animator.SetTrigger("Death");

			DeathSFX.Play();
		}

		private void Death() => Destroy(gameObject);

		private protected abstract void MovementUpdate();
	}
}