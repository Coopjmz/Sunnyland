namespace Sunnyland.Ending.UI.Menus
{
	sealed class EndMenu : Menu
	{
		public void PlayAgain()
		{
			SceneLoader.Load(Scene.FirstLevel);
		}
	}
}