namespace Sunnyland.MainMenu.UI.Menus
{
	sealed class Credits : Menu
	{
		public new void ToMainMenu()
		{
			UI.Display("Credits", false);
			UI.Display("Main Menu");
		}
	}
}