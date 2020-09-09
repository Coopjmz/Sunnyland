using UnityEngine;

namespace Sunnyland.Game.Entities
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Animator))]
	abstract class Entity : MonoBehaviour
	{
		internal SpriteRenderer SpriteRenderer { get; private set; }
		internal Rigidbody2D Rigidbody { get; private set; }
		internal BoxCollider2D BoxCollider { get; private set; }
		internal Animator Animator { get; private set; }

		private protected void Awake()
		{
			SpriteRenderer = GetComponent<SpriteRenderer>();
			Rigidbody = GetComponent<Rigidbody2D>();
			BoxCollider = GetComponent<BoxCollider2D>();
			Animator = GetComponent<Animator>();
		}

		internal abstract void Die();
	}
}