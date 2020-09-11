using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace Sunnyland
{
	sealed class Scene
	{
		public static Scene MainMenu => new Scene("Main Menu");
		public static Scene FirstLevel => new Scene("Level 1");
		public static Scene Active => new Scene(GetActiveScene().name);
		public static Scene Next { get; set; }

		public string Name { get; }

		public Scene(string sceneName) => Name = sceneName;
	}

	sealed class SceneLoader : MonoBehaviour
	{
		[SerializeField] private string _nextScene = default;

		private void Start() => Scene.Next = new Scene(_nextScene);

		public static void Load(Scene scene) => LoadScene(scene.Name);
	}
}