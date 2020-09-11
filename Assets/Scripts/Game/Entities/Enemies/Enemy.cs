using UnityEngine;

namespace Sunnyland.Game.Entities.Enemies
{
	abstract class Enemy : Entity
	{
		[Header("Stats")]
		[SerializeField] protected float _speed = 5f;

		protected AudioSource DeathSFX { private get; set; }

		public override void Die()
		{
			//Death animation
			enabled = false;
			Rigidbody.bodyType = RigidbodyType2D.Static;
			BoxCollider.enabled = false;
			Animator.SetTrigger("Death");

			DeathSFX.Play();
		}

		private void Death() => Destroy(gameObject);

		protected abstract void MovementUpdate();
	}
}