using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sunnyland
{
	abstract class Menu : MonoBehaviour
	{
		[SerializeField] private Button _defaultButton = default;

		private void OnEnable()
		{
			_defaultButton.Select();
			_defaultButton.OnSelect(null);
		}

		public void ToMainMenu() => SceneLoader.Load(Scene.MainMenu);
		public void Quit() => Application.Quit();

		protected void UpdateDefaultButton() =>
			_defaultButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
	}
}