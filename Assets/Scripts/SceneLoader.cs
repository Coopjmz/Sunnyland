using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace Sunnyland
{
	sealed class Scene
	{
		internal static Scene MainMenu => new Scene("Main Menu");
		internal static Scene FirstLevel => new Scene("Level 1");
		internal static Scene Active => new Scene(GetActiveScene().name);
		internal static Scene Next { get; set; }

		internal string Name { get; }

		internal Scene(string sceneName) => Name = sceneName;
	}

	sealed class SceneLoader : MonoBehaviour
	{
		[SerializeField] private string _nextScene = default;

		private void Start() => Scene.Next = new Scene(_nextScene);

		internal static void Load(Scene scene) => LoadScene(scene.Name);
	}
}