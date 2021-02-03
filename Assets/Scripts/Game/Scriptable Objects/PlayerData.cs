using UnityEngine;

namespace Sunnyland.Game.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Data/Player", fileName = "Player")]
	sealed class PlayerData : ScriptableObject
	{
		[Header("Life")]
		[SerializeField] private byte _maxLives = 3;

		[Header("Speed")]
		[SerializeField] private float _runSpeed = 10f;
		[SerializeField] private float _crouchSpeed = 5f;
		[SerializeField] private float _climbSpeed = 5f;

		[Header("Jump Force")]
		[SerializeField] private float _defaultJumpForce = 40f;
		[SerializeField] private float _crouchJumpForce = 20f;

		[Header("Power Up")]
		[SerializeField] private float _powerUpJumpForce = 50f;
		[SerializeField] private float _powerUpScale = 1.5f;
		[SerializeField] private float _duration = 10f;

		// Life
		public byte MaxLives => _maxLives;

		// Speed
		public float RunSpeed => _runSpeed;
		public float CrouchSpeed => _crouchSpeed;
		public float ClimbSpeed => _climbSpeed;

		// Jump Force
		public float DefaultJumpForce => _defaultJumpForce;
		public float CrouchJumpForce => _crouchJumpForce;

		// Power Up
		public float PowerUpJumpForce => _powerUpJumpForce;
		public float PowerUpScale => _powerUpScale;
		public float Duration => _duration;
	}
}