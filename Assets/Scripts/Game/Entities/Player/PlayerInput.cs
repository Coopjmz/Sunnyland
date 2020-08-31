using UnityEngine;

using Sunnyland.Game.Controls;

namespace Sunnyland.Game.Entities.Player
{
	sealed class PlayerInput : MonoBehaviour
	{
		/*
		private static PlayerControls _playerControls;

		private void Awake() => _playerControls ??= new PlayerControls();
		private void OnEnable() => _playerControls.Enable();
		private void OnDisable() => _playerControls.Disable();

		private void Update()
		{
			if (IsAlive && IsInputEnabled)
				MovementUpdate();
		}

		private protected override void MovementUpdate()
		{
			_xAxis = (sbyte)_playerControls.Player.Move.ReadValue<float>();
			_yAxis = (sbyte)_playerControls.Player.Climb.ReadValue<float>();

			if (!_climbing)
			{
				if (_ladder &&
					((_yAxis == 1 && !_boxCollider.IsTouchingLayers(_ladderPlatform)) ||
					 (_yAxis == -1 && (_boxCollider.IsTouchingLayers(_ladderPlatform) ||
									  !_boxCollider.IsTouchingLayers(_ground)))))
					SetClimbing(true);
				else if (_playerControls.Player.Jump.triggered && TouchingGround)
					_jumping = true;
				else if (_playerControls.Player.Crouch.triggered && TouchingGround)
					SetCrouching(true);
				else if (_playerControls.Player.Crouch.triggered)
					SetCrouching(false);
			}
		}
		*/
	}
}