using UnityEngine;

namespace Levels
{
    class Opossum: Enemy
    {
        //Variables (initialized from Unity)
        [SerializeField] float leftBound = default;
        [SerializeField] float rightBound = default;

        //Variables
        sbyte scale;

        new void Start()
		{
            //Initialize variables and components from base class
            base.Start();

            //Initialize variables
            scale = (sbyte)transform.localScale.x;
        }

		//Methods
        void Update()
        {
            //Update opossum
            MovementUpdate();
        }

        protected override void MovementUpdate()
        {
            //If opossum is going left and is at or past the left border
            if(scale == 1 && transform.position.x <= leftBound)
            {
                //Turn right
                scale = -1;
                transform.localScale = new Vector3(scale, 1f);
            }
            //If opossum is going right is at or past the right border
            else if(scale == -1 && transform.position.x >= rightBound)
            {
                //Turn left
                scale = 1;
                transform.localScale = new Vector3(scale, 1f);
            }
            
            //Set scale and X-axis velocity
            rigidBody.velocity = new Vector2(-scale * speed, 0f);
        }
    }
}