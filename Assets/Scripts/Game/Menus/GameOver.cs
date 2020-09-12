namespace Sunnyland.Game.Menus
{
	sealed class GameOver : Menu
	{
		private void Start() => UI.Instance.Display("HUD", false);

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