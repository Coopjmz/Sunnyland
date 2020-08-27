using UnityEngine;

namespace Levels
{
	sealed class Frog : Enemy
	{
		[SerializeField] private float jumpForce = 30f;

		[Header("Bounds")]
		[SerializeField] private Transform leftBound = default;
		[SerializeField] private Transform rightBound = default;

		[Header("Sounds")]
		[SerializeField] private AudioSource jumpSFX = default;
		[SerializeField] private AudioSource deathSFX = default;

		private enum State
		{
			Idle,
			Jumping,
			Falling,
		}

		private State _state;

		private sbyte _direction;
		private float _leftBound, _rightBound;

		private new void Start()
		{
			base.Start();

			_deathSFX = deathSFX;
			_direction = (sbyte)transform.localScale.x;
			_leftBound = leftBound.position.x;
			_rightBound = rightBound.position.x;
		}

		private void Update()
		{
			AnimationUpdate();
		}

		private void AnimationUpdate()
		{
			if(_rigidBody.velocity.y < -EPSILON)
				_state = State.Falling;
			else if(_rigidBody.velocity.y > EPSILON)
				_state = State.Jumping;
			else _state = State.Idle;

			_animator.SetInteger("State", (int)_state);
		}

		private protected override void MovementUpdate()
		{
			//Patrol AI
			if(_direction == 1 && transform.position.x <= _leftBound)
			{
				_direction = -1;
				transform.localScale = new Vector3(_direction, 1f);
			}
			else if(_direction == -1 && transform.position.x >= _rightBound)
			{
				_direction = 1;
				transform.localScale = new Vector3(_direction, 1f);
			}

			_rigidBody.velocity = new Vector2(-_direction * speed, jumpForce);
			jumpSFX.Play();
		}
	}
}