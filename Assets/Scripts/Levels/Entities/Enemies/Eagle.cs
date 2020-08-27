using UnityEngine;

namespace Levels
{
	sealed class Eagle : Enemy
	{
		[SerializeField] private float range = 7f;

		[Header("Sounds")]
		[SerializeField] private AudioSource deathSFX = default;

		private bool _chasing;

		private Player _player;

		private new void Start()
		{
			base.Start();

			_deathSFX = deathSFX;
			_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}

		private void FixedUpdate()
		{
			if(_player.IsAlive)
				MovementUpdate();
		}

		private protected override void MovementUpdate()
		{
			Vector2 vector = new Vector2(_player.transform.position.x - transform.position.x,
										 _player.transform.position.y - transform.position.y);

			if(!_chasing && vector.magnitude <= range)
				_chasing = true;

			if(_chasing)
			{
				_rigidBody.velocity = speed * vector.normalized;
				transform.localScale = new Vector3(-vector.x / Mathf.Abs(vector.x), 1f);
			}
		}
	}
}