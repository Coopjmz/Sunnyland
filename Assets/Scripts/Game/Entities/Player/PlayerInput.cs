using UnityEngine;

using Sunnyland.Game.Input;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerInput : MonoBehaviour
	{
		public const sbyte UP = 1;
		public const sbyte DOWN = -1;

		private bool _holdCrouchButton;

		private PlayerController _player;
		private Controls _controls;

		private bool IsTouchingGround => Mathf.Abs(_player.Rigidbody.velocity.y) < Game.EPSILON;

		private void Awake()
		{
			_player = GetComponent<PlayerController>();
			InitControls();
		}

		private void OnEnable() => _controls.Enable();
		private void OnDisable() => _controls.Disable();

		private void Update() => CrouchCheck();

		public void EnableJumpAndCrouch()
		{
			_controls.Player.Jump.Enable();
			_controls.Player.Crouch.Enable();
		}

		public void DisableJumpAndCrouch()
		{
			_controls.Player.Jump.Disable();
			_controls.Player.Crouch.Disable();
		}

		private void InitControls()
		{
			_controls = new Controls();

			//X-axis
			_controls.Player.Move.performed += context => _player.Movement.Xaxis = (sbyte)context.ReadValue<float>();
			_controls.Player.Move.canceled += _ => _player.Movement.Xaxis = 0;

			//Y-axis
			_controls.Player.Climb.started += context => _player.Movement.Yaxis = (sbyte)context.ReadValue<float>();
			_controls.Player.Climb.canceled += _ => _player.Movement.Yaxis = 0;

			//Get on a ladder
			_controls.Player.Climb.performed += context =>
			{
				if (IsAbleToClimb((sbyte)context.ReadValue<float>()))
					_player.Movement.SetClimbing(true);
			};

			//Jump
			_controls.Player.Jump.performed += _ =>
			{
				if (!IsAbleToClimb(UP) && IsTouchingGround)
					_player.Movement.Jumping = true;
			};

			//Crouch
			_controls.Player.Crouch.started += _ =>
			{
				_holdCrouchButton = true;
				if (!IsAbleToClimb(DOWN) && IsTouchingGround)
					_player.Movement.SetCrouching(true);
			};

			//Stand up
			_controls.Player.Crouch.canceled += _ =>
			{
				_holdCrouchButton = false;
				if (_player.Movement.Crouching)
					_player.Movement.SetCrouching(false);
			};

			//Interact
			_controls.Player.Interact.performed += _ => _player.Interact.Interact();
		}

		private void CrouchCheck()
		{
			if (_holdCrouchButton && !_player.Movement.Crouching && IsTouchingGround)
				_player.Movement.SetCrouching(true);
		}

		private bool IsAbleToClimb(sbyte yAxis)
		{
			return !_player.Movement.Climbing && _player.Interact.Ladder &&
				((yAxis == UP && !_player.Interact.Ladder.IsTouchingLadderPart(LadderPart.Top)) ||
				 (yAxis == DOWN && !_player.Interact.Ladder.IsTouchingLadderPart(LadderPart.Bottom)));
		}
	}
}