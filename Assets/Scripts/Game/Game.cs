using UnityEngine;

using static Sunnyland.Game.Entities.Player.PlayerController;
using static Sunnyland.Game.UI.UI;

namespace Sunnyland.Game
{
	static class Game
	{
		internal const float DRAG = 5f;
		internal const float GRAVITY = 10f;
		
		internal static bool IsTutorialEnabled { get; private set; } = true;
		internal static bool IsGameOver { get; private set; } = false;

		internal static void DisableTutorial()
		{
			IsTutorialEnabled = false;
			DisableTutorialText();
		}

		internal static void GameOver()
		{
			IsGameOver = true;
			GameObject.FindGameObjectWithTag("Player").SetActive(false);
			Display("Game Over");
		}

		internal static void Restart()
		{
			IsTutorialEnabled = true;
			IsGameOver = false;

			ResetLives();
			ResetUI();
		}
	}
}