using UnityEngine;

namespace Sunnyland.Game.Entities.Enemies
{
	sealed class Frog : Enemy
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

		private sbyte _direction;
		private float _leftBoundX, _rightBoundX;

		private new void Start()
		{
			base.Start();

			DeathSFX = _deathSFX;
			_direction = (sbyte)transform.localScale.x;
			_leftBoundX = _leftBound.position.x;
			_rightBoundX = _rightBound.position.x;
		}

		private void Update() => AnimationUpdate();

		private protected override void MovementUpdate()
		{
			//Patrol AI
			if (_direction == 1 && transform.position.x <= _leftBoundX)
			{
				_direction = -1;
				transform.localScale = new Vector3(_direction, 1f);
			}
			else if (_direction == -1 && transform.position.x >= _rightBoundX)
			{
				_direction = 1;
				transform.localScale = new Vector3(_direction, 1f);
			}

			_rigidbody.velocity = new Vector2(-_direction * _speed, jumpForce);
			_jumpSFX.Play();
		}

		private void AnimationUpdate()
		{
			if(_rigidbody.velocity.y < -EPSILON)
				_state = State.Falling;
			else if(_rigidbody.velocity.y > EPSILON)
				_state = State.Jumping;
			else _state = State.Idle;

			_animator.SetInteger("State", (int)_state);
		}
	}
}