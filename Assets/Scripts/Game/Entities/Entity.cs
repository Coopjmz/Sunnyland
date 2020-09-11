using UnityEngine;

namespace Sunnyland.Game.Entities
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Animator))]
	abstract class Entity : MonoBehaviour
	{
		public SpriteRenderer SpriteRenderer { get; private set; }
		public Rigidbody2D Rigidbody { get; private set; }
		public BoxCollider2D BoxCollider { get; private set; }
		public Animator Animator { get; private set; }

		protected void Awake()
		{
			SpriteRenderer = GetComponent<SpriteRenderer>();
			Rigidbody = GetComponent<Rigidbody2D>();
			BoxCollider = GetComponent<BoxCollider2D>();
			Animator = GetComponent<Animator>();
		}

		public abstract void Die();
	}
}