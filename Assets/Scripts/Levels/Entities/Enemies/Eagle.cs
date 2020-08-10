using UnityEngine;

namespace Levels
{
    class Eagle: Enemy
    {
        //Variables (initialized from Unity)
        [SerializeField] float range = 7f;

        //Variables
        bool chasing;

        //Components
        Player player;

		//Methods
		new void Start()
		{
            //Initialize components from base class
            base.Start();

            //Initialize components
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		void Update()
        {
            //While eagle and player are alive
            if(IsAlive && player.IsAlive)
                //Update eagle
                MovementUpdate();
        }

        protected override void MovementUpdate()
	    {
            //Vector between the eagle and the player
            Vector2 vector = new Vector2(player.transform.position.x - transform.position.x,
                                         player.transform.position.y - transform.position.y);

            //If the distance is less than the range of the eagle
            if(!chasing && vector.magnitude <= range)
                chasing = true;

            if(chasing)
		    {
                //Eagle chases the player
                rigidBody.velocity = speed * vector.normalized;
                transform.localScale = new Vector3(-vector.x / Mathf.Abs(vector.x), 1f);
            }
        }
    }
}