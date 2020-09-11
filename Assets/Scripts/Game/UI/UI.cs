using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using static Sunnyland.Game.Entities.Player.PlayerStats;
using static Sunnyland.Game.UI.Menus.PauseMenu;

namespace Sunnyland.Game.UI
{
	sealed class UI : MonoBehaviour
	{
		private static UI _instance;

		private static Dictionary<string, TextMeshProUGUI> _textFields;
		private static Dictionary<string, GameObject> _objects;

		[Header("Text")]
		[SerializeField] private TextMeshProUGUI _lifeCount = default;
		[SerializeField] private TextMeshProUGUI _cherryCount = default;
		[SerializeField] private TextMeshProUGUI _signText = default;
		[SerializeField] private TextMeshProUGUI _interactText = default;

		[Header("Objects")]
		[SerializeField] private GameObject _hud = default;
		[SerializeField] private GameObject _sign = default;
		[SerializeField] private GameObject _interact = default;
		[SerializeField] private GameObject _tutorial = default;

		[Header("Menus")]
		[SerializeField] private GameObject _pauseMenu = default;
		[SerializeField] private GameObject _gameOver = default;

		private void Start()
		{
			if (!_instance)
			{
				_instance = this;
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
		}

		private void Update()
		{
			if (Keyboard.current.escapeKey.wasPressedThisFrame && !Game.IsGameOver)
				TogglePauseMenu();
		}

		public static bool IsActive(string key) => _objects[key] && _objects[key].activeSelf;
		public static void UpdateText(string key, object value) => _textFields[key].text = value.ToString();
		public static void Display(string key, bool enable = true) => _objects[key].SetActive(enable);
		public static void DisableTutorialText() => Destroy(_instance._tutorial);

		public static void ResetUI()
		{
			Destroy(_instance.gameObject);
			OnStatChange -= UpdateText;
		}
	}
}