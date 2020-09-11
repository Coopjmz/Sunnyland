using UnityEngine;

namespace Sunnyland.Game.Entities.Enemies.Patrol
{
	sealed class Frog : PatrolEnemy
	{
		[SerializeField] private float jumpForce = 30f;

		[Header("Bounds")]
		[SerializeField] private Transform _leftBound = default;
		[SerializeField] private Transform _rightBound = default;

		[Header("Sounds")]
		[SerializeField] private AudioSource _jumpSFX = default;
		[SerializeField] private AudioSource _deathSFX = default;

		private enum State
		{
			Idle,
			Jumping,
			Falling,
		}

		private State _state;

		private new void Start()
		{
			base.Start();
			DeathSFX = _deathSFX;
			LeftBoundX = _leftBound.position.x;
			RightBoundX = _rightBound.position.x;
		}

		private void Update() => AnimationUpdate();

		protected override void MovementUpdate()
		{
			PatrolAI();
			Rigidbody.velocity = new Vector2(-Direction * _speed, jumpForce);
			_jumpSFX.Play();
		}

		private void AnimationUpdate()
		{
			if(Rigidbody.velocity.y < -Game.EPSILON)
				_state = State.Falling;
			else if(Rigidbody.velocity.y > Game.EPSILON)
				_state = State.Jumping;
			else _state = State.Idle;

			Animator.SetInteger("State", (int)_state);
		}
	}
}