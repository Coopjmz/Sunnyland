using UnityEngine;

using Sunnyland.Game.Entities.Player;

namespace Sunnyland.Game.Map
{
	sealed class Bounds : MonoBehaviour
	{
		[SerializeField] private Transform _killBound = default;
		[SerializeField] private Transform _resetBound = default;
		
		private float _killBoundY, _resetBoundY;

		private PlayerController _player;

		private void Awake() => _player = FindObjectOfType<PlayerController>();

		private void Start()
		{
			_killBoundY = _killBound.position.y;
			_resetBoundY = _resetBound.position.y;
		}

		private void Update() => CheckBounds();

		private void CheckBounds()
		{
			if (_player.IsAlive && _player.transform.position.y < _killBoundY)
				_player.Die();
			else if (_player.transform.position.y < _resetBoundY)
				ResetPlayer();
		}

		private void ResetPlayer()
		{
			enabled = false;

			if (_player.Stats.Lives > 0)
				SceneLoader.Load(Scene.Active);
			else Game.GameOver();
		}
	}
}