using UnityEngine;

namespace Sunnyland.Game
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Animator))]
	sealed class Item : MonoBehaviour
	{
		[SerializeField] private AudioSource _pickupSFX = default;

		private SpriteRenderer _spriteRenderer;
		private BoxCollider2D _boxCollider;
		private Animator _animator;

		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_boxCollider = GetComponent<BoxCollider2D>();
			_animator = GetComponent<Animator>();
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (!collider.CompareTag("Player")) return;

			_boxCollider.enabled = false;
			_animator.SetTrigger("Pickup");

			_pickupSFX.Play();
			Destroy(gameObject, _pickupSFX.clip.length);
		}

		private void Pickup() => _spriteRenderer.enabled = false;
	}
}