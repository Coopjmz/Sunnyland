using System;
using System.Collections;

using UnityEngine;
using static UnityEngine.Mathf;

using Sunnyland.Game.Controls;
using Sunnyland.Game.Entities.Enemies;
using Sunnyland.Game.Interactables;
using static Sunnyland.Game.Game;

namespace Sunnyland.Game.Entities.Player
{
	sealed class PlayerController : Entity
	{
		internal static event Action<string, object> OnStatChange;

		private const byte MAX_LIVES = 3;

		private static byte _lives = MAX_LIVES;
		private static byte _cherries = 0;

		private static PlayerControls _playerControls;

		[SerializeField] private float _jumpForce = 40f;
		[SerializeField] private float _climbSpeed = 5f;

		[Header("Power Up")]
		[SerializeField] private float _jumpBoost = 10f;
		[SerializeField] private float _scaleBoost = 1.5f;
		[SerializeField] private float _powerUpTime = 10f;

		[Header("Sounds")]
		[SerializeField] private AudioSource _footstepsSFX = default;
		[SerializeField] private AudioSource _jumpSFX = default;
		[SerializeField] private AudioSource _deathSFX = default;

		[Header("Layers")]
		[SerializeField] private LayerMask _ground = default;
		[SerializeField] private LayerMask _ladderPlatform = default;
		[SerializeField] private LayerMask _enemy = default;

		private enum State
		{
			Idle,
			Running,
			Crouching,
			Jumping,
			Falling,
			Climbing
		}

		private State _state;

		private sbyte _xAxis, _yAxis;
		private bool _jumping, _crouching, _climbing;
		private bool _powerUp;

		private Interactable _interactable;
		private Transform _ladder;

		private static byte Lives
		{
			get => _lives;
			set
			{
				_lives = value;
				OnStatChange?.Invoke("Life", value);
			}
		}

		private static byte Cherries
		{
			get => _cherries;
			set
			{
				_cherries = value;
				OnStatChange?.Invoke("Cherry", value);
			}
		}

		internal bool IsAlive => _boxCollider.isActiveAndEnabled;
		private bool TouchingGround => Abs(_rigidbody.velocity.y) < EPSILON;

		private void Awake() => _playerControls = _playerControls ?? new PlayerControls();
		private void OnEnable() => _playerControls.Enable();
		private void OnDisable() => _playerControls.Disable();

		private new void Start()
		{
			base.Start();
			StartCoroutine(Flicker(1f));
		}

		private void Update()
		{
			if (IsAlive)
			{
				MovementUpdate();
				UtilityUpdate();
				AnimationUpdate();

				if (transform.position.y < -10f) Die();
			}
			else if (transform.position.y < -20f) ResetPlayer();

			if (IsTutorialEnabled && Abs(transform.position.x) > 10f)
				DisableTutorial();
		}

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

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag("Interactable"))
				_interactable = collider.GetComponent<Interactable>();
			else if (collider.CompareTag("Ladder"))
				_ladder = collider.transform;
			else if (collider.CompareTag("Collectable"))
				Cherries++;
			else if (collider.CompareTag("Power Up"))
			{
				//Buff player
				_powerUp = true;
				_jumpForce += _jumpBoost;
				transform.localScale *= _scaleBoost;
				_spriteRenderer.color = Color.yellow;

				StartCoroutine(PowerUpTimer());
			}
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if (collider.CompareTag("Interactable"))
				_interactable = null;
			if (collider.CompareTag("Ladder"))
			{
				if (_climbing) SetClimbing(false);
				_ladder = null;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Enemy"))
			{
				Collider2D jumpOnEnemy = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,
					0f, Vector2.down, .5f, _enemy).collider;

				if (jumpOnEnemy || _powerUp)
				{
					collision.gameObject.GetComponent<Enemy>().Die();

					if (jumpOnEnemy) Jump(false);
				}
				else Die();
			}
		}

		private void OnDestroy() => Cherries = 0;
		internal static void ResetLives() => Lives = MAX_LIVES;

		internal override void Die()
		{
			Lives--;

			//Death animation
			SetCrouching(false);
			_rigidbody.velocity = new Vector2(0f, _jumpForce);
			_boxCollider.enabled = false;
			_animator.SetTrigger("Death");

			_deathSFX.Play();

			if (IsTutorialEnabled)
				DisableTutorial();
		}

		private protected override void MovementUpdate()
		{
			_xAxis = (sbyte)_playerControls.Movement.Move.ReadValue<float>();
			_yAxis = (sbyte)_playerControls.Movement.Climb.ReadValue<float>();

			if (!_climbing)
			{
				if (_ladder &&
					((_yAxis == 1 && !_boxCollider.IsTouchingLayers(_ladderPlatform)) ||
					 (_yAxis == -1 && (_boxCollider.IsTouchingLayers(_ladderPlatform) ||
									  !_boxCollider.IsTouchingLayers(_ground)))))
					SetClimbing(true);
				else if (_playerControls.Movement.Jump.triggered && TouchingGround)
					_jumping = true;
				else if (_playerControls.Movement.Crouch.triggered && TouchingGround)
					SetCrouching(true);
				else if (_playerControls.Movement.Crouch.triggered)
					SetCrouching(false);
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

		private void UtilityUpdate()
		{
			if (_interactable != null && _playerControls.Utility.Interact.triggered)
				_interactable.Interact();
		}

		private void AnimationUpdate()
		{
			_animator.speed = 1f;

			if (_climbing)
			{
				_state = State.Climbing;

				if (Abs(_rigidbody.velocity.y) < 2f)
					_animator.speed = 0f;
			}
			else if (_rigidbody.velocity.y < -EPSILON)
				_state = State.Falling;
			else if (_rigidbody.velocity.y > EPSILON)
				_state = State.Jumping;
			else if (_crouching)
				_state = State.Crouching;
			else if (Abs(_rigidbody.velocity.x) > EPSILON)
				_state = State.Running;
			else _state = State.Idle;

			_animator.SetInteger("State", (int)_state);
		}

		private void ResetPlayer()
		{
			if (Lives > 0)
				SceneLoader.Load(Scene.Active);
			else GameOver();
		}

		private void Footsteps() => _footstepsSFX.Play();

		private IEnumerator Flicker(float timer)
		{
			do
			{
				_spriteRenderer.enabled = !_spriteRenderer.enabled;

				timer -= .1f;
				yield return new WaitForSeconds(.1f);
			}
			while (timer > 0f);

			_spriteRenderer.enabled = true;
		}

		private IEnumerator PowerUpTimer()
		{
			float timer = _powerUpTime;
			do
			{
				if (timer == 2f)
					StartCoroutine(Flicker(2f));

				timer--;
				yield return new WaitForSeconds(1f);
			}
			while (timer > 0f);

			//Nerf player
			_powerUp = false;
			_jumpForce -= _jumpBoost;
			transform.localScale /= _scaleBoost;
			_spriteRenderer.color = Color.white;
		}
	}
}