using UnityEngine;

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
		internal bool IsAlive => BoxCollider.isActiveAndEnabled;

		internal PlayerStats Stats { get; private set; }
		internal PlayerInput Input { get; private set; }
		internal PlayerMovement Movement { get; private set; }
		internal PlayerCollision Collision { get; private set; }
		internal PlayerAnimation Animation { get; private set; }
		internal PlayerSounds Sounds { get; private set; }
		internal PlayerInteract Interact { get; private set; }
		internal PlayerPowerUp PowerUp { get; private set; }

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

		private void Update()
		{
			if (Game.IsTutorialEnabled && Mathf.Abs(transform.position.x) > 10f)
			{
				Game.DisableTutorial();
				enabled = false;
			}
		}

		internal override void Die()
		{
			Stats.Lives--;

			//Disable player
			Input.enabled = false;
			Movement.enabled = false;

			if (Movement.Crouching)
				Movement.Crouching = false;

			//Death animation
			Rigidbody.velocity = new Vector2(0f, Movement.JumpForce);
			BoxCollider.enabled = false;
			Animator.SetTrigger("Death");

			Sounds.PlayDeathSound();

			if (Game.IsTutorialEnabled)
				Game.DisableTutorial();
		}
	}
}