using System.Collections.Generic;

using UnityEngine;

namespace Levels
{
    abstract class Enemy: Entity
    {
        //Methods
        protected new void Start()
        {
            //Initialize components from base class
            base.Start();

            //Initialize sound effects
            sfx = new Dictionary<string, AudioSource>
            {
                {"Death", GetComponent<AudioSource>()}
            };
        }

        //Events
        void Death()
        {
            Destroy(gameObject);
        }

        //Internal methods
        internal override void Kill()
        {
            //Death animation
            enabled = false;
            rigidBody.bodyType = RigidbodyType2D.Static;
            boxCollider.enabled = false;
            animator.SetTrigger("Death");

            //Death sound
            sfx["Death"].Play();
        }
    }
}