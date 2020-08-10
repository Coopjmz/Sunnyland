namespace MainMenu
{
	class Credits: Menu
	{
		//Events
		public new void ToMainMenu()
		{
			UI.Display("Credits", false);
			UI.Display("Main Menu");
		}
	}
}