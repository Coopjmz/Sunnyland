namespace Sunnyland.MainMenu.Menus
{
	sealed class MainMenu : Menu
	{
		public void Play() => SceneLoader.Load(Scene.FirstLevel);

		public void Credits()
		{
			UI.Instance.Display("Main Menu", false);
			UI.Instance.Display("Credits");
		}
	}
}