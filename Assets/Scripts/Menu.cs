using UnityEngine;

using Levels;

class Menu: MonoBehaviour
{
	//Events
	public void ToMainMenu()
	{
		Game.Restart();
		SceneLoader.Load(Scene.MainMenu);
	}

	public void Quit()
	{
		Application.Quit();
	}
}