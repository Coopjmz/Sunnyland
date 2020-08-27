using UnityEngine;

abstract class Menu : MonoBehaviour
{
	public void ToMainMenu()
	{
		SceneLoader.Load(Scene.MainMenu);
	}

	public void Quit()
	{
		Application.Quit();
	}
}