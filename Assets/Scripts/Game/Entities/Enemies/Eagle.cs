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

		private new void Awake()
		{
			base.Awake();
			_player = FindObjectOfType<PlayerController>();
		}

		private void Start() => DeathSFX = _deathSFX;

		private void FixedUpdate()
		{
			if (_player.IsAlive)
				MovementUpdate();
		}

		protected override void MovementUpdate()
		{
			Vector2 vector = new Vector2(_player.transform.position.x - transform.position.x,
										 _player.transform.position.y - transform.position.y);

			if (!_chasing && vector.magnitude <= _range)
				_chasing = true;

			if (_chasing)
			{
				Rigidbody.velocity = _speed * vector.normalized;
				transform.localScale = new Vector3(-vector.x / Mathf.Abs(vector.x), 1f);
			}
		}
	}
}