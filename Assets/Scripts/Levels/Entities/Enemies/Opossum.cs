using UnityEngine;

namespace Levels
{
    class Opossum: Enemy
    {
        //Variables (initialized from Unity)
        [SerializeField] float leftBound = default;
        [SerializeField] float rightBound = default;

        //Variables
        sbyte scale = 1; //Opossum is facing left (default)

		//Methods
        void Update()
        {
            //While opossum is alive
            if(IsAlive)
                //Update opossum
                MovementUpdate();
        }

        protected override void MovementUpdate()
        {
            //If opossum is going left
            if(scale == 1)
            {
                //If opossum is at or past the left border
                if(transform.position.x <= leftBound)
                {
                    //Turn right
                    scale = -1;
                    transform.localScale = new Vector3(scale, 1f);
                }
            }
            //If opossum is going right
            else if(scale == -1)
            {
                //If opossum is at or past the right border
                if(transform.position.x >= rightBound)
                {
                    //Turn left
                    scale = 1;
                    transform.localScale = new Vector3(scale, 1f);
                }
            }
            
            //Set scale and X-axis velocity
            rigidBody.velocity = new Vector2(-scale * speed, 0f);
        }
    }
}