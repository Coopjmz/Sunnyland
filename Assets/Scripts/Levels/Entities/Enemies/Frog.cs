using UnityEngine;

namespace Levels
{
    class Frog: Enemy
    {
        //Frog states
        enum State
        {
            Idle,
            Jumping,
            Falling,
        }

        //Current frog state
        State state;

        //Variables (initialized from Unity)
        [SerializeField] float jumpForce = 30f;
        [SerializeField] float leftBound = default;
        [SerializeField] float rightBound = default;

        //Variables
        sbyte scale;

        //Methods
        new void Start()
        {
            //Initialize components and sound effects from base class
            base.Start();

            //Initialize variables
            scale = (sbyte)transform.localScale.x;

            //Add sound effects
            sfx.Add("Jump", GetComponents<AudioSource>()[1]);
        }

        void Update()
        {
            //Update the animation
            AnimationUpdate();
        }

        void AnimationUpdate()
        {
            //Frog is falling
            if(rigidBody.velocity.y < -EPSILON)
                state = State.Falling;
            //Frog is jumping
            else if(rigidBody.velocity.y > EPSILON)
                state = State.Jumping;
            //Frog is idle
            else state = State.Idle;

            //Animate frog
            animator.SetInteger("State", (int)state);
        }

        //Events
        protected override void MovementUpdate()
        {
            //If frog is facing left is at or past the left border
            if(scale == 1 && transform.position.x <= leftBound)
            {
                //Turn right
                scale = -1;
                transform.localScale = new Vector3(scale, 1f);
            }
            //If frog is facing right is at or past the right border
            else if(scale == -1 && transform.position.x >= rightBound)
            {
                //Turn left
                scale = 1;
                transform.localScale = new Vector3(scale, 1f);
            }

            //Set jump direction and Y-axis velocity
            rigidBody.velocity = new Vector2(-scale * speed, jumpForce);

            //Play jump sound
            sfx["Jump"].Play();
        }
    }
}