using System.Collections;

using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Mathf;

using static Levels.Game;

namespace Levels
{
	class Player : Entity
	{
		[SerializeField] private float jumpForce = 40f;
		[SerializeField] private float climbSpeed = 5f;

		[Header("Power Up")]
		[SerializeField] private float jumpBoost = 10f;
		[SerializeField] private float scaleBoost = 1.5f;
		[SerializeField] private float powerUpTime = 10f;

		[Header("Sounds")]
		[SerializeField] private AudioSource footstepsSFX = default;
		[SerializeField] private AudioSource jumpSFX = default;
		[SerializeField] private AudioSource deathSFX = default;

		[Header("Layers")]
		[SerializeField] private LayerMask ground = default;
		[SerializeField] private LayerMask ladderPlatform = default;
		[SerializeField] private LayerMask enemy = default;

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

		private const byte MAX_LIVES = 3;
		private static byte _lives = MAX_LIVES;

		private sbyte _xAxis, _yAxis;
		private bool _jumping, _crouching, _climbing;

		private byte _cherries;
		private bool _powerUp;

		private Transform _ladder;

		private bool TouchingGround => Abs(_rigidBody.velocity.y) < EPSILON;
		internal bool IsAlive => _boxCollider.isActiveAndEnabled;

		private new void Start()
		{
			base.Start();
			StartCoroutine(Flicker(1f));
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.CompareTag("Collectable"))
				UI.UpdateText("Cherry", ++_cherries);
			else if(collider.CompareTag("Power Up"))
			{
				//Buff player
				_powerUp = true;
				jumpForce += jumpBoost;
				transform.localScale *= scaleBoost;
				_spriteRenderer.color = Color.yellow;

				StartCoroutine(PowerUpTimer());
			}
			else if(collider.CompareTag("Ladder"))
				_ladder = collider.transform;
		}

		private void OnTriggerExit2D(Collider2D collider)
		{
			if(collider.CompareTag("Ladder"))
			{
				if(_climbing) SetClimbing(false);
				_ladder = null;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if(collision.gameObject.CompareTag("Enemy"))
			{
				Collider2D jumpOnEnemy = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,
					0f, Vector2.down, .5f, enemy).collider;

				if(jumpOnEnemy || _powerUp)
				{
					collision.gameObject.GetComponent<Enemy>().Kill();

					if(jumpOnEnemy) Jump(false);
				}
				else Kill();
			}
		}

		private void Update()
		{
			if(IsAlive)
			{
				if(IsInputEnabled)
					MovementUpdate();
				AnimationUpdate();

				if(transform.position.y < -10f) Kill();
			}
			else if(transform.position.y < -20f) ResetPlayer();

			if(IsTutorialEnabled && Abs(transform.position.x) > 10f)
				DisableTutorial();
		}

		private void FixedUpdate()
		{
			if(IsAlive)
			{
				if(_xAxis != 0)
					Move();

				if(_jumping)
				{
					Jump();
					_jumping = false;
				}
				else if(_climbing && _yAxis != 0)
					Climb();
			}
		}

		private protected override void MovementUpdate()
		{
			_xAxis = (sbyte)GetAxisRaw("Horizontal");
			_yAxis = (sbyte)GetAxisRaw("Vertical");

			if(!_climbing)
			{
				if(_ladder &&
				 ((_yAxis == 1 && !_boxCollider.IsTouchingLayers(ladderPlatform)) ||
				  (_yAxis == -1 && (_boxCollider.IsTouchingLayers(ladderPlatform) ||
								  !_boxCollider.IsTouchingLayers(ground)))))
					SetClimbing(true);
				else if(GetButtonDown("Jump") && TouchingGround)
					_jumping = true;
				else if(GetButton("Crouch") && TouchingGround)
					SetCrouching(true);
				else if(GetButtonUp("Crouch"))
					SetCrouching(false);
			}
		}

		private void Move()
		{
			transform.localScale =
				new Vector3(_xAxis * Abs(transform.localScale.x), transform.localScale.y);
			_rigidBody.velocity = new Vector2(_xAxis * speed, _rigidBody.velocity.y);

			if(_climbing) SetClimbing(false);
		}

		private void Jump(bool sound = true)
		{
			_rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpForce);

			if(sound) jumpSFX.Play();
		}

		private void Climb()
		{
			if(_yAxis == -1 && _boxCollider.IsTouchingLayers(ground))
				SetClimbing(false);
			else _rigidBody.velocity = new Vector2(0f, _yAxis * climbSpeed);
		}

		private void SetCrouching(bool enabled)
		{
			if(enabled && !_crouching)
			{
				speed /= 2f;
				jumpForce /= 2f;
			}
			else if(!enabled && _crouching)
			{
				speed *= 2f;
				jumpForce *= 2f;
			}

			_crouching = enabled;
		}

		private void SetClimbing(bool enabled)
		{
			if(enabled && !_climbing)
			{
				if(_crouching) SetCrouching(false);

				transform.position = new Vector3(_ladder.position.x, transform.position.y);
				_rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX |
										RigidbodyConstraints2D.FreezeRotation;

				_rigidBody.drag = 20f;
				_rigidBody.gravityScale = 0f;
			}
			else if(!enabled && _climbing)
			{
				_rigidBody.velocity = Vector2.zero;
				_rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

				_rigidBody.drag = DRAG;
				_rigidBody.gravityScale = GRAVITY;
			}

			_ladder.GetChild(0).GetComponent<BoxCollider2D>().enabled = !enabled;

			_climbing = enabled;
		}

		private void AnimationUpdate()
		{
			_animator.speed = 1f;

			if(_climbing)
			{
				_state = State.Climbing;

				if(Abs(_rigidBody.velocity.y) < 2f)
					_animator.speed = 0f;
			}
			else if(_rigidBody.velocity.y < -EPSILON)
				_state = State.Falling;
			else if(_rigidBody.velocity.y > EPSILON)
				_state = State.Jumping;
			else if(_crouching)
				_state = State.Crouching;
			else if(Abs(_rigidBody.velocity.x) > EPSILON)
				_state = State.Running;
			else _state = State.Idle;

			_animator.SetInteger("State", (int)_state);
		}

		private void ResetPlayer()
		{
			if(_lives > 0)
				SceneLoader.Load(Scene.Active);
			else GameOver();
		}

		private void Footsteps()
		{
			footstepsSFX.Play();
		}

		private IEnumerator Flicker(float timer)
		{
			do
			{
				_spriteRenderer.enabled = !_spriteRenderer.enabled;

				timer -= .1f;
				yield return new WaitForSeconds(.1f);
			}
			while(timer > 0f);

			_spriteRenderer.enabled = true;
		}

		private IEnumerator PowerUpTimer()
		{
			float timer = powerUpTime;
			do
			{
				if(timer == 2f)
					StartCoroutine(Flicker(2f));

				timer--;
				yield return new WaitForSeconds(1f);
			}
			while(timer > 0f);

			//Nerf player
			_powerUp = false;
			jumpForce -= jumpBoost;
			transform.localScale /= scaleBoost;
			_spriteRenderer.color = Color.white;
		}

		internal override void Kill()
		{
			UI.UpdateText("Life", --_lives);

			//Death animation
			_rigidBody.velocity = new Vector2(0f, jumpForce);
			_boxCollider.enabled = false;
			_animator.SetTrigger("Death");

			deathSFX.Play();

			if(IsTutorialEnabled)
				DisableTutorial();
		}

		internal static void ResetLives()
		{
			_lives = MAX_LIVES;
		}
	}
}