using UnityEngine;

namespace Sunnyland.Game.Entities.Enemies.Patrol
{
	sealed class Opossum : PatrolEnemy
	{
		[Header("Bounds")]
		[SerializeField] private Transform _leftBound = default;
		[SerializeField] private Transform _rightBound = default;

		[Header("Sounds")]
		[SerializeField] private AudioSource _deathSFX = default;

		private new void Start()
		{
			base.Start();
			DeathSFX = _deathSFX;
			LeftBoundX = _leftBound.position.x;
			RightBoundX = _rightBound.position.x;
		}

		private void FixedUpdate() => MovementUpdate();

		private protected override void MovementUpdate()
		{
			PatrolAI();
			Rigidbody.velocity = new Vector2(-Direction * _speed, 0f);
		}
	}
}