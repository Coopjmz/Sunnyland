using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
	class Interactable: MonoBehaviour
    {
        //Variables
        bool InRange;

        //Components (initialized from Unity)
        [SerializeField] UnityEvent action = default;

        //Methods
        void OnTriggerEnter2D(Collider2D collider)
        {
            //If the player is in range
            if(collider.CompareTag("Player"))
			{
                InRange = true;
                UI.Display("Interact");

                //Disable the tutorial
                if(Game.IsTutorialEnabled)
                    Game.DisableTutorial();
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            //It the player is not in range
            if(collider.CompareTag("Player"))
			{
                InRange = false;
                UI.Display("Interact", false);
            }
        }

        void Update()
        {
            //If player is in range and presses the interact key, it invokes the action
            if(Input.GetButtonDown("Interact") && Game.IsInputEnabled && InRange)
			{
                UI.Display("Interact", false);
                action.Invoke();
            } 
        }
    }
}