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

		private void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_boxCollider = GetComponent<BoxCollider2D>();
		}

		private void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.CompareTag("Player"))
			{
				_spriteRenderer.enabled = false;
				_boxCollider.enabled = false;
				Destroy(gameObject, _pickupSFX.clip.length);

				_pickupSFX.Play();
			}
		}
	}
}