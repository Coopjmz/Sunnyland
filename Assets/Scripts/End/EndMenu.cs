namespace End
{
	sealed class EndMenu : Menu
	{
		public void PlayAgain()
		{
			SceneLoader.Load(Scene.FirstLevel);
		}
	}
}