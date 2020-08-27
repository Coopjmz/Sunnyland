namespace Levels
{
	sealed class GameOver : Menu
	{
		private void Awake()
		{
			UI.Display("HUD", false);
		}

		public void PlayAgain()
		{
			Game.Restart();
			SceneLoader.Load(Scene.FirstLevel);
		}

		public new void ToMainMenu()
		{
			Game.Restart();
			base.ToMainMenu();
		}
	}
}