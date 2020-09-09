namespace Sunnyland.MainMenu.UI.Menus
{
	sealed class MainMenu : Menu
	{
		public void Play() => SceneLoader.Load(Scene.FirstLevel);

		public void Credits()
		{
			UI.Display("Main Menu", false);
			UI.Display("Credits");
		}
	}
}