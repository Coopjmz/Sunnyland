using UnityEngine;

using Sunnyland.Game.Entities.Player;

namespace Sunnyland.Game
{
	sealed class MapBorders : MonoBehaviour
	{
		[SerializeField] private float _killBorder = -10f;
		[SerializeField] private float _resetBorder = -20f;

		private PlayerController _player;

		private void Awake() => _player = FindObjectOfType<PlayerController>();
		private void Start() => enabled = _player != null;

		private void Update()
		{
			if (_player.IsAlive && _player.transform.position.y < _killBorder)
				_player.Die();
			else if (_player.transform.position.y < _resetBorder)
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