using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Mathf;

using static Levels.Game;

namespace Levels
{
	class Player: Entity
    {
        //Player states
        enum State
        {
            Idle,
            Running,
            Crouching,
            Jumping,
            Falling,
            Climbing
        }

        //Current player state
        State state;

        //Constants
        const byte MAX_LIVES = 3;

        //Static variables
        static byte lives = MAX_LIVES;

        //Variables (initialized from Unity)
        [SerializeField] float climbSpeed = 5f;
        [SerializeField] float jumpForce = 40f;
        [SerializeField] float jumpBoost = 10f;
        [SerializeField] float scaleBoost = 1.5f;
        [SerializeField] float powerUpTime = 10f;

        //Variables
        bool jumping, crouching, climbing, powerUp;
        byte cherries;

        //Axes
        sbyte xAxis, yAxis;

        //Components
        SpriteRenderer spriteRenderer;
        Transform ladder;

        //Layers (initialized from Unity)
        [SerializeField] LayerMask ground = default;
        [SerializeField] LayerMask ladderPlatform = default;
        [SerializeField] LayerMask enemy = default;

        //Properties
        bool TouchingGround => Abs(rigidBody.velocity.y) < EPSILON;

        //Internal properties
        internal bool IsAlive => boxCollider.isActiveAndEnabled;

        //Methods
        new void Start()
	    {
            //Initialize components from base class
            base.Start();
            
            //Initialize sound effects
            sfx = new Dictionary<string, AudioSource>
            {
                {"Footsteps", GetComponents<AudioSource>()[0]},
                {"Jump", GetComponents<AudioSource>()[1]},
                {"Death", GetComponents<AudioSource>()[2]}
            };

            //Initialize components
            spriteRenderer = GetComponent<SpriteRenderer>();

            //Make player flicker
            StartCoroutine(Flicker(1f));
	    }

	    void OnTriggerEnter2D(Collider2D collider)
	    {
            //If player touches a cherry
            if(collider.CompareTag("Collectable"))
                //Update UI text
                UI.UpdateText("Cherry", ++cherries);
            //If player touches a gem
            else if(collider.CompareTag("Power Up"))
            {
                //Buff player
                powerUp = true;
                jumpForce += jumpBoost;
                transform.localScale *= scaleBoost;
                spriteRenderer.color = Color.yellow;

                //Start timer
                StartCoroutine(PowerUpTimer());
            }
            //If player is near a ladder
            else if(collider.CompareTag("Ladder"))
                ladder = collider.transform;
        }

        void OnTriggerExit2D(Collider2D collider)
		{
            //If player isn't near a ladder
            if(collider.CompareTag("Ladder"))
			{
                if(climbing) SetClimbing(false);
                ladder = null;
            } 
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //If player interacts with an enemy
            if(collision.gameObject.CompareTag("Enemy"))
            {
                Collider2D jumpOnEnemy = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,
                    0f, Vector2.down, .5f, enemy).collider;

                //If the player jumps on one, or has power up
                if(jumpOnEnemy || powerUp)
                {
                    //Kill the enemy
                    collision.gameObject.GetComponent<Enemy>().Kill();

                    //If the player jumps on one
                    if(jumpOnEnemy) Jump(false);
                }
                //If the player touches one, kill the player
                else Kill();
            }
        }

        void Update()
        {
            //While the player is alive
            if(IsAlive)
            {
                //Update player
                if(IsInputEnabled)
                     MovementUpdate();
                AnimationUpdate();

                //Check if the player is out of bounds
                if(transform.position.y < -10f) Kill();
            }
            else if(transform.position.y < -20f) ResetPlayer();

            //Disable the tutorial
            if(IsTutorialEnabled && Abs(transform.position.x) > 10f)
                DisableTutorial();
        }

		void FixedUpdate()
		{
            //Rigidbody physics update
            if(IsAlive)
			{
                if(xAxis != 0)
                    Move();

                if(jumping)
			    {
                    Jump();
                    jumping = false;
			    }
                else if(climbing && yAxis != 0)
                    Climb();
			}
        }

		protected override void MovementUpdate()
	    {
            //Get axes input
            xAxis = (sbyte)GetAxisRaw("Horizontal");
            yAxis = (sbyte)GetAxisRaw("Vertical");

            if(!climbing)
			{
                //If player attempts to climb
                if(ladder &&
                 ((yAxis == 1 && !boxCollider.IsTouchingLayers(ladderPlatform)) ||
                  (yAxis == -1 && (boxCollider.IsTouchingLayers(ladderPlatform) ||
                                  !boxCollider.IsTouchingLayers(ground)))))
                    SetClimbing(true);
                //If player jumps
                else if(GetButtonDown("Jump") && TouchingGround)
                    jumping = true;
                //If player crouches
                else if(GetButton("Crouch") && TouchingGround)
                    SetCrouching(true);
                //If player stands up
                else if(GetButtonUp("Crouch"))
                    SetCrouching(false);
			}
        }

        void Move()
        {
            //Set scale and X-axis velocity
            transform.localScale =
                new Vector3(xAxis * Abs(transform.localScale.x), transform.localScale.y);
            rigidBody.velocity = new Vector2(xAxis * speed, rigidBody.velocity.y);

            //If player was climbing
            if(climbing)
                //Stop climbing
                SetClimbing(false);
        }

        void Jump(bool sound = true)
        {
            //Set Y-axis velocity
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

            if(sound)
                //Play jump sound
                sfx["Jump"].Play();
        }

        void Climb()
        {
            //If player is at the bottom of the ladder
            if(yAxis == -1 && boxCollider.IsTouchingLayers(ground))
                //Get off the ladder
                SetClimbing(false);
            else
                //Set Y-axis velocity
                rigidBody.velocity = new Vector2(0f, yAxis * climbSpeed);
        }

        void SetCrouching(bool enabled)
        {
            if(enabled && !crouching)
			{
                //Decrease movement speed and jump force
                speed /= 2f;
                jumpForce /= 2f;
            }
            else if(!enabled && crouching)
            {
                //Increse movement speed and jump force
                speed *= 2f;
                jumpForce *= 2f;
            }

            crouching = enabled;
        }

        void SetClimbing(bool enabled)
        {
            if(enabled && !climbing)
			{
                //Disable crouching
                if(crouching) SetCrouching(false);

                //Set player position and constraints
                transform.position = new Vector3(ladder.position.x, transform.position.y);
                rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX |
                                        RigidbodyConstraints2D.FreezeRotation;

                //Set linear drag and gravity scale
                rigidBody.drag = 20f;
                rigidBody.gravityScale = 0f;
            }
            else if(!enabled && climbing)
			{
                //Set player velocity and constraints
                rigidBody.velocity = Vector2.zero;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

                //Set linear drag and gravity scale
                rigidBody.drag = DRAG;
                rigidBody.gravityScale = GRAVITY;
            }

            //Toggle ladder platform
            ladder.GetChild(0).GetComponent<BoxCollider2D>().enabled = !enabled;

            climbing = enabled;
        }

		void AnimationUpdate()
	    {
            //Set animation speed
            animator.speed = 1f;

            //Player is climbing
            if(climbing)
			{
                state = State.Climbing;

                //Change animation speed
                if(Abs(rigidBody.velocity.y) < 2f)
                     animator.speed = 0f;
            }
            //Player is falling
            else if(rigidBody.velocity.y < -EPSILON)
                state = State.Falling;
            //Player is jumping
            else if(rigidBody.velocity.y > EPSILON)
                state = State.Jumping;
            //Player is crouching
            else if(crouching)
                state = State.Crouching;
            //Player is moving
            else if(Abs(rigidBody.velocity.x) > EPSILON)
                state = State.Running;
            //Player is idle
            else state = State.Idle;

            //Animate player
            animator.SetInteger("State", (int)state);
        }

        void ResetPlayer()
        {
            if(lives > 0)
                //Respawn player
                SceneLoader.Load(Scene.Active);
            else GameOver();
        }

        //Events
        void Footsteps()
        {
            //Play footsteps sound
            sfx["Footsteps"].Play();
        }

        //Coroutines
        IEnumerator Flicker(float timer)
	    {
            //Start the timer
            do
            {
                //Make the player flicker
                spriteRenderer.enabled = !spriteRenderer.enabled;

                //Reduce the time left
                timer -= .1f;
                yield return new WaitForSeconds(.1f);
            }
            while(timer > 0f);
        
            //Make player visible
            spriteRenderer.enabled = true;
        }

        IEnumerator PowerUpTimer()
	    {
            //Create a timer
            float timer = powerUpTime;

            //Start the timer
            do
		    {
                //If timer is about to run out, make player flicker
                if(timer == 2f)
                    StartCoroutine(Flicker(2f));

                //Reduce the time left
                timer--;
                yield return new WaitForSeconds(1f);
		    }
            while(timer > 0f);

            //Nerf player
            powerUp = false;
            jumpForce -= jumpBoost;
            transform.localScale /= scaleBoost;
            spriteRenderer.color = Color.white;
	    }

        //Internal methods
        internal override void Kill()
	    {
            //Decrement lives
            UI.UpdateText("Life", --lives);

            //Death animation
            rigidBody.velocity = new Vector2(0f, jumpForce);
            boxCollider.enabled = false;
            animator.SetTrigger("Death");

            //Death sound
            sfx["Death"].Play();

            //Disable the tutorial
            if(IsTutorialEnabled)
                DisableTutorial();
        }

        //Static methods
        internal static void ResetLives()
        {
            lives = MAX_LIVES;
        }
    }
}