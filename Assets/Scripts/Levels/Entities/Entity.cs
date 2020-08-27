using UnityEngine;

namespace Levels
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Animator))]
	abstract class Entity : MonoBehaviour
	{
		[Header("Stats")]
		[SerializeField] private protected float speed = 5f;

		private protected const float EPSILON = .01f;

		private protected SpriteRenderer _spriteRenderer;
		private protected Rigidbody2D _rigidBody;
		private protected BoxCollider2D _boxCollider;
		private protected Animator _animator;

		private protected void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_rigidBody = GetComponent<Rigidbody2D>();
			_boxCollider = GetComponent<BoxCollider2D>();
			_animator = GetComponent<Animator>();
		}

		private protected abstract void MovementUpdate();
		internal abstract void Kill();
	}
}