using UnityEngine;

namespace Sunnyland.Game.Entities
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Animator))]
	abstract class Entity : MonoBehaviour
	{
		private protected const float EPSILON = .01f;

		[Header("Stats")]
		[SerializeField] private protected float _speed = 5f;

		private protected SpriteRenderer _spriteRenderer;
		private protected Rigidbody2D _rigidbody;
		private protected BoxCollider2D _boxCollider;
		private protected Animator _animator;

		private protected void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_rigidbody = GetComponent<Rigidbody2D>();
			_boxCollider = GetComponent<BoxCollider2D>();
			_animator = GetComponent<Animator>();
		}

		internal abstract void Die();
		private protected abstract void MovementUpdate();
	}
}