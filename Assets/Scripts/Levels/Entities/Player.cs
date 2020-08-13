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
        static float drag, gravityScale;

        //Variables (initialized from Unity)
        [SerializeField] float climbSpeed = 5f;
        [SerializeField] float jumpForce = 40f;
        [SerializeField] float jumpBoost = 10f;
        [SerializeField] float scaleBoost = 1.5f;
        [SerializeField] float powerUpTime = 10f;

        //Variables
        bool crouching, climbing, powerUp;
        byte cherries;

        //Components
        SpriteRenderer spriteRenderer;
        Transform ladder;

        //Layers (initialized from Unity)
        [SerializeField] LayerMask ground = default;
        [SerializeField] LayerMask ladderPlatform = default;
        [SerializeField] LayerMask enemy = default;

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

            //Initialize static variables
            if(drag == 0f && gravityScale == 0f)
                drag = rigidBody.drag;
                gravityScale = rigidBody.gravityScale;

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
                if(climbing) ToggleClimbing();
                ladder = null;
            } 
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //If player interacts with an enemy
            if(collision.gameObject.CompareTag("Enemy"))
            {
                Collider2D jumpOnEnemy =
                    Physics2D.BoxCast(boxCollider.bounds.center - new Vector3(0f, boxCollider.bounds.extents.y),
                    new Vector2(boxCollider.bounds.size.x, 1f), 0f, Vector2.down, 0f, enemy).collider;

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

        protected override void MovementUpdate()
	    {
            //Get axis input
            float xAxis = GetAxis("Horizontal");
            float yAxis = GetAxis("Vertical");

            //If player moves
            if(xAxis != 0f)
                Move(xAxis);

            //If player climbs
            if(climbing && yAxis != 0f)
                Climb(yAxis);
            //If player attempts to climb
            else if(ladder &&
                ((yAxis > 0f && !boxCollider.IsTouchingLayers(ladderPlatform)) ||
                 (yAxis < 0f && (boxCollider.IsTouchingLayers(ladderPlatform) ||
                                !boxCollider.IsTouchingLayers(ground)))))
                ToggleClimbing();
            //If player Jumps
            else if(GetButtonDown("Jump") && Abs(rigidBody.velocity.y) < EPSILON)
                Jump();
            //If player crouches
            else if(GetButton("Crouch") && Abs(rigidBody.velocity.y) < EPSILON)
                Crouch(true);
            //If player stands up
            else if(GetButtonUp("Crouch"))
                Crouch(false);
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

        void Move(float xAxis)
        {
            //Player direction
            sbyte direction = (sbyte)(xAxis / Abs(xAxis));

            //Set scale and X-axis velocity
            transform.localScale =
                new Vector3(direction * Abs(transform.localScale.x), transform.localScale.y);
            rigidBody.velocity = new Vector2(direction * speed, rigidBody.velocity.y);

            //If player was climbing
            if(climbing)
                //Stop climbing
                ToggleClimbing();
        }

		void Crouch(bool enabled)
        {
            //Decrease movement speed and jump height
            if(enabled && !crouching)
			{
                speed /= 2f;
                jumpForce /= 2f;
            }
            //Increse movement speed and jump height
            else if(!enabled && crouching)
            {
                speed *= 2f;
                jumpForce *= 2f;
            }

            crouching = enabled;
        }

        void Jump(bool sound = true)
        {
            //Set Y-axis velocity
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

            if(sound)
                //Play jump sound
                sfx["Jump"].Play();
        }

        void ToggleClimbing()
        {
            climbing = !climbing;

            //Toggle ladder platform
            ladder.GetChild(0).GetComponentInChildren<BoxCollider2D>().enabled = !climbing;

            if(climbing)
			{
                //Disable crouching
                Crouch(false);

                //Set player position and constraints
                transform.position = new Vector3(ladder.position.x, transform.position.y);
                rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX |
                                        RigidbodyConstraints2D.FreezeRotation;

                //Set linear drag and gravity scale
                rigidBody.drag = 20f;
                rigidBody.gravityScale = 0f;
            }
            else
			{
                //Set player velocity and constraints
                rigidBody.velocity = new Vector2(0f, 0f);
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

                //Set linear drag and gravity scale
                rigidBody.drag = drag;
                rigidBody.gravityScale = gravityScale;
            }
        }

        void Climb(float yAxis)
		{
            //If player is at the bottom of the ladder
            if(yAxis < 0f && boxCollider.IsTouchingLayers(ground))
			{
                //Get off the ladder
                ToggleClimbing();
                return;
            }

            //Player direction
            sbyte direction = (sbyte)(yAxis / Abs(yAxis));

            //Set Y-axis velocity
            rigidBody.velocity = new Vector2(0f, direction * climbSpeed);
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