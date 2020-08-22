using System.Collections.Generic;

using UnityEngine;

namespace Levels
{
    abstract class Entity: MonoBehaviour
    {
        //Constants
        protected const float EPSILON = .01f;

        //Variables (initialized from Unity)
        [SerializeField] protected float speed = 5f;

        //Components
        protected Rigidbody2D rigidBody;
        protected BoxCollider2D boxCollider;
        protected Animator animator;

        //Sound effects
        protected Dictionary<string, AudioSource> sfx;
        
        //Methods
        protected void Start()
        {
            //Initialize components
            rigidBody = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
        }

        //Abstract methods
        protected abstract void MovementUpdate();
        internal abstract void Kill();
    }
}