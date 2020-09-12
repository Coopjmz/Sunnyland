using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using static Sunnyland.Game.Entities.Player.PlayerStats;
using static Sunnyland.Game.Menus.PauseMenu;

namespace Sunnyland.Game
{
	sealed class UI : Displayable
	{
		private Dictionary<string, TextMeshProUGUI> _textFields;

		[Header("Objects")]
		[SerializeField] private GameObject _hud = default;
		[SerializeField] private GameObject _sign = default;
		[SerializeField] private GameObject _interact = default;
		[SerializeField] private GameObject _tutorial = default;

		[Header("Menus")]
		[SerializeField] private GameObject _pauseMenu = default;
		[SerializeField] private GameObject _gameOver = default;

		[Header("Text")]
		[SerializeField] private TextMeshProUGUI _lifeCount = default;
		[SerializeField] private TextMeshProUGUI _cherryCount = default;
		[SerializeField] private TextMeshProUGUI _signText = default;
		[SerializeField] private TextMeshProUGUI _interactText = default;

		public static UI Instance { get; private set; }

		private void Start()
		{
			if (!Instance)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);

				Objects = new Dictionary<string, GameObject>
				{
					{"HUD", _hud},
					{"Sign", _sign},
					{"Interact", _interact},
					{"Tutorial", _tutorial},
					{"Pause Menu", _pauseMenu},
					{"Game Over", _gameOver}
				};

				_textFields = new Dictionary<string, TextMeshProUGUI>
				{
					{"Life", _lifeCount},
					{"Cherry", _cherryCount},
					{"Sign", _signText},
					{"Interact", _interactText}
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

		public void UpdateText(string key, object value) => _textFields[key].text = value.ToString();
		public void DisableTutorialText() => Destroy(_tutorial);

		public void ResetUI()
		{
			OnStatChange -= Instance.UpdateText;
			Destroy(Instance.gameObject);
		}
	}
}