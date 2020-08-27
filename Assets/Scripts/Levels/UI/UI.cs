using System.Collections.Generic;

using UnityEngine;
using TMPro;

namespace Levels
{
	sealed class UI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI lifeCount = default;
		[SerializeField] private TextMeshProUGUI cherryCount = default;
		[SerializeField] private TextMeshProUGUI signText = default;

		[SerializeField] private GameObject hud = default;
		[SerializeField] private GameObject sign = default;
		[SerializeField] private GameObject interact = default;
		[SerializeField] private GameObject tutorial = default;

		[SerializeField] private GameObject pauseMenu = default;
		[SerializeField] private GameObject gameOver = default;

		private static UI _ui;

		private static Dictionary<string, TextMeshProUGUI> _textFields;
		private static Dictionary<string, GameObject> _objects;

		private void Start()
		{
			if(!_ui)
			{
				_ui = this;
				DontDestroyOnLoad(gameObject);

				_textFields = new Dictionary<string, TextMeshProUGUI>
				{
					{"Life", lifeCount},
					{"Cherry", cherryCount},
					{"Sign", signText}
				};

				_objects = new Dictionary<string, GameObject>
				{
					{"HUD", hud},
					{"Sign", sign},
					{"Interact", interact},
					{"Tutorial", tutorial},
					{"Pause Menu", pauseMenu},
					{"Game Over", gameOver}
				};
			}
			else
			{
				UpdateText("Cherry", 0);
				Destroy(gameObject);
			}
		}

		private void Update()
		{
			if(Input.GetButtonDown("Pause Menu") && !Game.IsGameOver)
				PauseMenu.TogglePauseMenu();
		}

		internal static void UpdateText(string key, object value)
		{
			_textFields[key].text = value.ToString();
		}

		internal static bool IsActive(string key)
		{
			return _objects[key] && _objects[key].activeSelf;
		}

		internal static void Display(string key, bool enable = true)
		{
			_objects[key].SetActive(enable);
		}

		internal static void DisableTutorialText()
		{
			Destroy(_ui.tutorial);
		}

		internal static void ResetUI()
		{
			Destroy(_ui.gameObject);
		}
	}
}