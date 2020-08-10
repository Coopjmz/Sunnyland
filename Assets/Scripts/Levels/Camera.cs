using UnityEngine;
using Cinemachine;

namespace Levels
{
    class Camera: MonoBehaviour
    {
        //Components
        Player player;
        CinemachineBrain cinemachine;

		//Methods
		void Start()
		{
            //Initialize components
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            cinemachine = GetComponent<CinemachineBrain>();
		}

		void Update()
        {
            //When the player dies
            if(!player.IsAlive)
			{
                //Camera stops following the player
                enabled = false;
                cinemachine.enabled = false;
            }
        }
    }
}