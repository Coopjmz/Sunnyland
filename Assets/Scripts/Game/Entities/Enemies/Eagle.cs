using UnityEngine;

using Sunnyland.Game.Entities.Player;

namespace Sunnyland.Game.Entities.Enemies
{
	sealed class Eagle : Enemy
	{
		[SerializeField] private float _range = 7f;

		[Header("Sounds")]
		[SerializeField] private AudioSource _deathSFX = default;

		private bool _chasing;

		private PlayerController _player;

		private new void Start()
		{
			base.Start();

			DeathSFX = _deathSFX;
			_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		}

		private void FixedUpdate()
		{
			if (_player.IsAlive)
				MovementUpdate();
		}

		private protected override void MovementUpdate()
		{
			Vector2 vector = new Vector2(_player.transform.position.x - transform.position.x,
										 _player.transform.position.y - transform.position.y);

			if (!_chasing && vector.magnitude <= _range)
				_chasing = true;

			if (_chasing)
			{
				_rigidbody.velocity = _speed * vector.normalized;
				transform.localScale = new Vector3(-vector.x / Mathf.Abs(vector.x), 1f);
			}
		}
	}
}