using System.Collections.Generic;

using UnityEngine;
using TMPro;

using Sunnyland.Game.Controls;
using static Sunnyland.Game.Entities.Player.PlayerController;
using static Sunnyland.Game.UI.Menus.PauseMenu;

namespace Sunnyland.Game.UI
{
	sealed class UI : MonoBehaviour
	{
		private static UI _ui;

		private static GameControls _gameControls;

		private static Dictionary<string, TextMeshProUGUI> _textFields;
		private static Dictionary<string, GameObject> _objects;

		[Header("Text Fields - Text")]
		[SerializeField] private TextMeshProUGUI _lifeCount = default;
		[SerializeField] private TextMeshProUGUI _cherryCount = default;
		[SerializeField] private TextMeshProUGUI _signText = default;
		[SerializeField] private TextMeshProUGUI _interactText = default;

		[Header("Text Fields - Objects")]
		[SerializeField] private GameObject _hud = default;
		[SerializeField] private GameObject _sign = default;
		[SerializeField] private GameObject _interact = default;
		[SerializeField] private GameObject _tutorial = default;

		[Header("Menus")]
		[SerializeField] private GameObject _pauseMenu = default;
		[SerializeField] private GameObject _gameOver = default;

		private void Start()
		{
			if (!_ui)
			{
				_ui = this;
				DontDestroyOnLoad(gameObject);

				_textFields = new Dictionary<string, TextMeshProUGUI>
				{
					{"Life", _lifeCount},
					{"Cherry", _cherryCount},
					{"Sign", _signText},
					{"Interact", _interactText}
				};

				_objects = new Dictionary<string, GameObject>
				{
					{"HUD", _hud},
					{"Sign", _sign},
					{"Interact", _interact},
					{"Tutorial", _tutorial},
					{"Pause Menu", _pauseMenu},
					{"Game Over", _gameOver}
				};

				OnStatChange += UpdateText;
			}
			else Destroy(gameObject);

			if (_gameControls == null)
			{
				_gameControls = new GameControls();
				_gameControls.Enable();
			}
		}

		private void Update()
		{
			if (_gameControls.UI.PauseMenu.triggered && !Game.IsGameOver)
				TogglePauseMenu();
		}

		internal static bool IsActive(string key) => _objects[key] && _objects[key].activeSelf;
		internal static void UpdateText(string key, object value) => _textFields[key].text = value.ToString();
		internal static void Display(string key, bool enable = true) => _objects[key].SetActive(enable);
		internal static void DisableTutorialText() => Destroy(_ui._tutorial);
		internal static void ResetUI()
		{
			Destroy(_ui.gameObject);
			OnStatChange -= UpdateText;
		}
	}
}