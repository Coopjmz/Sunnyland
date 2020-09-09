namespace Sunnyland.Game.UI.Menus
{
	sealed class GameOver : Menu
	{
		private void Start() => UI.Display("HUD", false);

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