using UnityEngine;

namespace Levels
{
	static class Game
	{
		internal const float DRAG = 5f;
		internal const float GRAVITY = 10f;

		internal static bool IsInputEnabled { get; set; } = true;
		internal static bool IsTutorialEnabled { get; private set; } = true;
		internal static bool IsGameOver { get; private set; } = false;

		internal static void DisableTutorial()
		{
			IsTutorialEnabled = false;
			UI.DisableTutorialText();
		}

		internal static void GameOver()
		{
			IsGameOver = true;
			GameObject.FindGameObjectWithTag("Player").SetActive(false);
			UI.Display("Game Over");
		}

		internal static void Restart()
		{
			IsTutorialEnabled = true;
			IsGameOver = false;

			Player.ResetLives();
			UI.ResetUI();
		}
	}
}