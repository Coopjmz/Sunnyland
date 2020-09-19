namespace Sunnyland.Ending.Menus
{
	sealed class EndMenu : Menu
	{
		public void NextLevel() => SceneLoader.Load(Scene.Next);
	}
}