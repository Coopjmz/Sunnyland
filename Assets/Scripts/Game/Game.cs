using UnityEngine;

using Sunnyland.Game.Entities.Player;
using Sunnyland.Game.Tutorials;

namespace Sunnyland.Game
{
	static class Game
	{
		public const float EPSILON = .01f;
		public const float DRAG = 5f;
		public const float GRAVITY = 10f;

		public static bool IsGameOver { get; private set; }

		public static void GameOver()
		{
			IsGameOver = true;

			GameObject.FindGameObjectWithTag("Player").SetActive(false);
			UI.Instance.Display("Game Over");
		}

		public static void Restart()
		{
			IsGameOver = false;

			PlayerStats.ResetLives();
			UI.Instance.ResetUI();
			TutorialManager.Instance.ResetTutorials();
		}
	}
}