using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	sealed class PlayerMovement : MonoBehaviour
	{
		/*
		private void FixedUpdate()
		{
			if (IsAlive)
			{
				if (_xAxis != 0)
					Move();

				if (_jumping)
				{
					Jump();
					_jumping = false;
				}
				else if (_climbing && _yAxis != 0)
					Climb();
			}
		}

		private void Move()
		{
			if (_xAxis * transform.localScale.x < 0f)
				transform.localScale = new Vector3(_xAxis * Abs(transform.localScale.x), transform.localScale.y);
			_rigidbody.velocity = new Vector2(_xAxis * _speed, _rigidbody.velocity.y);

			if (_climbing) SetClimbing(false);
		}

		private void Jump(bool sound = true)
		{
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

			if (sound) _jumpSFX.Play();
		}

		private void Climb()
		{
			if (_yAxis == -1 && _boxCollider.IsTouchingLayers(_ground))
				SetClimbing(false);
			else _rigidbody.velocity = new Vector2(0f, _yAxis * _climbSpeed);
		}

		private void SetCrouching(bool enabled)
		{
			if (enabled && !_crouching)
			{
				_speed /= 2f;
				_jumpForce /= 2f;
			}
			else if (!enabled && _crouching)
			{
				_speed *= 2f;
				_jumpForce *= 2f;
			}

			_crouching = enabled;
		}

		private void SetClimbing(bool enabled)
		{
			if (enabled && !_climbing)
			{
				if (_crouching) SetCrouching(false);

				transform.position = new Vector3(_ladder.position.x, transform.position.y);
				_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX |
										RigidbodyConstraints2D.FreezeRotation;

				_rigidbody.drag = 20f;
				_rigidbody.gravityScale = 0f;
			}
			else if (!enabled && _climbing)
			{
				_rigidbody.velocity = Vector2.zero;
				_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

				_rigidbody.drag = DRAG;
				_rigidbody.gravityScale = GRAVITY;
			}

			_ladder.GetChild(0).GetComponent<BoxCollider2D>().enabled = !enabled;

			_climbing = enabled;
		}
		*/
	}
}