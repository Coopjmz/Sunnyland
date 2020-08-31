using UnityEngine;

namespace Sunnyland.Game.Entities.Enemies
{
	sealed class Opossum : Enemy
	{
		[Header("Bounds")]
		[SerializeField] private Transform _leftBound = default;
		[SerializeField] private Transform _rightBound = default;

		[Header("Sounds")]
		[SerializeField] private AudioSource _deathSFX = default;

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

		private void FixedUpdate() => MovementUpdate();

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

			_rigidbody.velocity = new Vector2(-_direction * _speed, 0f);
		}
	}
}