using UnityEngine;

using Sunnyland.Game.ScriptableObjects;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerStats))]
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(PlayerMovement))]
	[RequireComponent(typeof(PlayerCollision))]
	[RequireComponent(typeof(PlayerAnimation))]
	[RequireComponent(typeof(PlayerSounds))]
	[RequireComponent(typeof(PlayerInteract))]
	[RequireComponent(typeof(PlayerPowerUp))]
	sealed class PlayerController : Entity
	{
		[SerializeField] private PlayerData _data = default;

		public bool IsAlive => BoxCollider.isActiveAndEnabled;

		public PlayerData Data => _data;

		public PlayerStats Stats { get; private set; }
		public PlayerInput Input { get; private set; }
		public PlayerMovement Movement { get; private set; }
		public PlayerCollision Collision { get; private set; }
		public PlayerAnimation Animation { get; private set; }
		public PlayerSounds Sounds { get; private set; }
		public PlayerInteract Interact { get; private set; }
		public PlayerPowerUp PowerUp { get; private set; }

		private new void Awake()
		{
			base.Awake();

			Stats = GetComponent<PlayerStats>();
			Input = GetComponent<PlayerInput>();
			Movement = GetComponent<PlayerMovement>();
			Collision = GetComponent<PlayerCollision>();
			Animation = GetComponent<PlayerAnimation>();
			Sounds = GetComponent<PlayerSounds>();
			Interact = GetComponent<PlayerInteract>();
			PowerUp = GetComponent<PlayerPowerUp>();
		}

		public override void Die()
		{
			Stats.Lives--;

			//Disable player
			Input.enabled = false;
			Movement.enabled = false;

			if (Movement.Crouching)
				Movement.SetCrouching(false);

			//Death animation
			Rigidbody.velocity = new Vector2(0f, Data.DefaultJumpForce);
			BoxCollider.enabled = false;
			Animator.SetTrigger("Death");

			Sounds.PlayDeathSound();
		}
	}
}