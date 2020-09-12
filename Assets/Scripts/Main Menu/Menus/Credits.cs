namespace Sunnyland.MainMenu.Menus
{
	sealed class Credits : Menu
	{
		public new void ToMainMenu()
		{
			UI.Instance.Display("Credits", false);
			UI.Instance.Display("Main Menu");
		}
	}
}