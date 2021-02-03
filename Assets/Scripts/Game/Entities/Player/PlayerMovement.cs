using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerMovement : MonoBehaviour
	{
		private float _speed;

		private PlayerController _player;

		public float JumpForce { private get; set; }

		public sbyte Xaxis { private get; set; }
		public sbyte Yaxis { private get; set; }
		public Vector2 Axes { private get; set; }

		public bool Jumping { private get; set; }
		public bool Crouching { get; private set; }
		public bool Climbing { get; private set; }

		private void Awake() => _player = GetComponent<PlayerController>();

		private void Start()
		{
			_speed = _player.Data.RunSpeed;
			JumpForce = _player.Data.DefaultJumpForce;
		}

		private void FixedUpdate() => MovementUpdate();

		public void SetCrouching(bool enabled)
		{
			if (enabled)
			{
				_speed = _player.Data.CrouchSpeed;
				JumpForce = _player.Data.CrouchSpeed;
			}
			else
			{
				_speed = _player.Data.RunSpeed;
				JumpForce = _player.Data.DefaultJumpForce;
			}

			Crouching = enabled;
		}

		public void SetClimbing(bool enabled)
		{
			if (enabled)
			{
				transform.position = new Vector3(_player.Interact.Ladder.transform.position.x, transform.position.y);
				_player.Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX |
												RigidbodyConstraints2D.FreezeRotation;

				_player.Rigidbody.drag = 20f;
				_player.Rigidbody.gravityScale = 0f;

				_player.Input.DisableJumpAndCrouch();
				_player.Interact.Ladder.DisablePlatform();

				Xaxis = 0;
				_speed = _player.Data.ClimbSpeed;
			}
			else
			{
				_player.Rigidbody.velocity = Vector2.zero;
				_player.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

				_player.Rigidbody.drag = Game.DRAG;
				_player.Rigidbody.gravityScale = Game.GRAVITY;

				_player.Input.EnableJumpAndCrouch();
				_player.Interact.Ladder.EnablePlatform();

				_speed = _player.Data.RunSpeed;
			}

			Climbing = enabled;
		}

		private void MovementUpdate()
		{
			if (Xaxis != 0)
				Move();

			if (Jumping)
				Jump();
			else if (Climbing && Yaxis != 0)
				Climb();
		}

		private void Move()
		{
			_player.Rigidbody.velocity = new Vector2(Xaxis * _speed, _player.Rigidbody.velocity.y);

			if (Xaxis * transform.localScale.x < 0f)
				transform.localScale = new Vector3(Xaxis * Mathf.Abs(transform.localScale.x), transform.localScale.y);

			if (Climbing) SetClimbing(false);
		}

		public void Jump(bool sound = true)
		{
			Jumping = false;
			_player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, JumpForce);

			if (sound) _player.Sounds.PlayJumpSound();
		}

		private void Climb()
		{
			if (Yaxis == PlayerInput.DOWN && _player.Interact.Ladder.IsTouchingLadderPart(LadderPart.Bottom))
				SetClimbing(false);
			else _player.Rigidbody.velocity = new Vector2(0f, Yaxis * _speed);
		}
	}
}