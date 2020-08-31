using UnityEngine;

namespace Sunnyland
{
	abstract class Menu : MonoBehaviour
	{
		public void ToMainMenu() => SceneLoader.Load(Scene.MainMenu);
		public void Quit() => Application.Quit();
	}
}