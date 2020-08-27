using UnityEngine;

namespace Levels
{
	sealed class Opossum : Enemy
	{
		[Header("Bounds")]
		[SerializeField] private Transform leftBound = default;
		[SerializeField] private Transform rightBound = default;

		[Header("Sounds")]
		[SerializeField] private AudioSource deathSFX = default;

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

		private void FixedUpdate()
		{
			MovementUpdate();
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

			_rigidBody.velocity = new Vector2(-_direction * speed, 0f);
		}
	}
}