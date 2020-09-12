using UnityEngine;

using Sunnyland.Game.Entities.Player;

namespace Sunnyland.Game.Map
{
	sealed class Bounds : MonoBehaviour
	{
		[Header("Tutorial")]
		[SerializeField] private Transform _tutorialLeftBound = default;
		[SerializeField] private Transform _tutorialRightBound = default;

		[Header("Kill \\ Reset")]
		[SerializeField] private Transform _killBound = default;
		[SerializeField] private Transform _resetBound = default;

		private float _tutorialLeftBoundX, _tutorialRightBoundX;
		private float _killBoundY, _resetBoundY;

		private PlayerController _player;

		private void Awake() => _player = FindObjectOfType<PlayerController>();
		private void Start()
		{
			_tutorialLeftBoundX = _tutorialLeftBound.position.x;
			_tutorialRightBoundX = _tutorialRightBound.position.x;
			_killBoundY = _killBound.position.y;
			_resetBoundY = _resetBound.position.y;

			enabled = _player != null;
		}

		private void Update()
		{
			if (Game.IsTutorialEnabled)
				CheckTutorialBound();

			CheckKillResetBounds();
		}

		private void CheckTutorialBound()
		{
			if (_player.transform.position.x < _tutorialLeftBoundX ||
				_player.transform.position.x > _tutorialRightBoundX)
				Game.DisableTutorial();
		}

		private void CheckKillResetBounds()
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