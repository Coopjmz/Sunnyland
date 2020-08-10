using UnityEngine;

namespace Level
{
	class Collectable: MonoBehaviour
	{
		//Components
		SpriteRenderer spriteRenderer;
		BoxCollider2D boxCollider;
		AudioSource sound;

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			boxCollider = GetComponent<BoxCollider2D>();
			sound = GetComponent<AudioSource>();
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			if(collider.CompareTag("Player"))
			{
				//Get the collectable
				spriteRenderer.enabled = false;
				boxCollider.enabled = false;
				Destroy(gameObject, sound.clip.length);

				//Play a pick up sound
				sound.Play();
			}
		}
	}
}