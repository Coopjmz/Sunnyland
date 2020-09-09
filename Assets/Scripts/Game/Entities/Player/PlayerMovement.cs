using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private float _speed = 10f;
		[SerializeField] private float _jumpForce = 40f;
		[SerializeField] private float _climbSpeed = 5f;

		private bool _crouching, _climbing;

		private PlayerController _player;

		internal float JumpForce
		{
			get => _jumpForce;
			set => _jumpForce = value;
		}

		internal sbyte Xaxis { private get; set; }
		internal sbyte Yaxis { private get; set; }

		internal bool Jumping { get; set; }

		internal bool Crouching
		{
			get => _crouching;
			set
			{
				if (value)
				{
					_speed /= 2f;
					_jumpForce /= 2f;
				}
				else
				{
					_speed *= 2f;
					_jumpForce *= 2f;
				}

				_crouching = value;
			}
		}

		internal bool Climbing
		{
			get => _climbing;
			set
			{
				if (value)
				{
					transform.position = new Vector3(_player.Interact.Ladder.position.x, transform.position.y);
					_player.Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX |
													RigidbodyConstraints2D.FreezeRotation;

					_player.Rigidbody.drag = 20f;
					_player.Rigidbody.gravityScale = 0f;

					Xaxis = 0;
					_player.Input.DisableJumpAndCrouch();
				}
				else
				{
					_player.Rigidbody.velocity = Vector2.zero;
					_player.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

					_player.Rigidbody.drag = Game.DRAG;
					_player.Rigidbody.gravityScale = Game.GRAVITY;

					_player.Input.EnableJumpAndCrouch();
				}

				_player.Interact.Ladder.GetChild(0).GetComponent<BoxCollider2D>().enabled = !value;

				_climbing = value;
			}
		}

		private void Awake() => _player = GetComponent<PlayerController>();
		private void FixedUpdate() => MovementUpdate();

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

			if (Climbing)
			{
				print("Movement climbing: false");
				Climbing = false;
			}
		}

		internal void Jump(bool sound = true)
		{
			Jumping = false;
			_player.Rigidbody.velocity = new Vector2(_player.Rigidbody.velocity.x, _jumpForce);

			if (sound) _player.Sounds.PlayJumpSound();
		}

		private void Climb()
		{
			if (Yaxis == -1 && _player.BoxCollider.IsTouchingLayers(Layers.BottomLadder))
				Climbing = false;
			else _player.Rigidbody.velocity = new Vector2(0f, Yaxis * _climbSpeed);
		}
	}
}