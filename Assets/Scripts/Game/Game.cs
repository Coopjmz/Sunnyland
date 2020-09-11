using UnityEngine;

using static Sunnyland.Game.Entities.Player.PlayerStats;
using static Sunnyland.Game.UI.UI;

namespace Sunnyland.Game
{
	static class Game
	{
		public const float EPSILON = .01f;
		public const float DRAG = 5f;
		public const float GRAVITY = 10f;
		
		public static bool IsTutorialEnabled { get; private set; } = true;
		public static bool IsGameOver { get; private set; } = false;

		public static void DisableTutorial()
		{
			IsTutorialEnabled = false;
			DisableTutorialText();
		}

		public static void GameOver()
		{
			IsGameOver = true;
			GameObject.FindGameObjectWithTag("Player").SetActive(false);
			Display("Game Over");
		}

		public static void Restart()
		{
			IsTutorialEnabled = true;
			IsGameOver = false;

			ResetLives();
			ResetUI();
		}
	}
}