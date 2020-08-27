using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

enum Scene
{
	Active,
	Next,
	MainMenu,
	FirstLevel
}

sealed class SceneLoader : MonoBehaviour
{
	[SerializeField] private string nextScene = default;

	private static string _nextScene;

	private void Start()
	{
		_nextScene = nextScene;
	}

	internal static void Load(Scene scene)
	{
		switch(scene)
		{
			case Scene.Active:
				LoadScene(GetActiveScene().name);
				break;
			case Scene.Next:
				LoadScene(_nextScene);
				break;
			case Scene.MainMenu:
				LoadScene("Main Menu");
				break;
			case Scene.FirstLevel:
				LoadScene("Level 1");
				break;
		}
	}
}