namespace MainMenu
{
	class MainMenu: Menu
	{
		//Events
		public void Play()
		{
			SceneLoader.Load(Scene.FirstLevel);
		}

		public void Credits()
		{
			UI.Display("Main Menu", false);
			UI.Display("Credits");
		}
	}
}