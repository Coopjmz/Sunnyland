using UnityEngine;

namespace Level
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(BoxCollider2D))]
	[RequireComponent(typeof(Animator))]
	sealed class Collectable : MonoBehaviour
	{
		[SerializeField] private AudioSource pickupSFX = default;

		private SpriteRenderer _spriteRenderer;
		private BoxCollider2D _boxCollider;

		void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_boxCollider = GetComponent<BoxCollider2D>();
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.CompareTag("Player"))
			{
				_spriteRenderer.enabled = false;
				_boxCollider.enabled = false;
				Destroy(gameObject, pickupSFX.clip.length);

				pickupSFX.Play();
			}
		}
	}
}