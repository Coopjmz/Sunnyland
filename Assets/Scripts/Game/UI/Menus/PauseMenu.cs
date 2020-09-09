using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Sunnyland.Game.Entities.Player;

namespace Sunnyland.Game.UI.Menus
{
	sealed class PauseMenu : Menu
	{
		private static bool _paused;

		private static Dictionary<string, bool> _textFieldStates;

		private void Awake()
		{
			_textFieldStates = new Dictionary<string, bool>
			{
				{"HUD", true},
				{"Sign", false},
				{"Interact", false},
				{"Tutorial", false}
			};
		}

		internal static void TogglePauseMenu()
		{
			_paused = !_paused;

			UI.Display("Pause Menu", _paused);
			Time.timeScale = _paused ? 0f : 1f;
			FindObjectOfType<PlayerInput>().enabled = !_paused;

			ToggleTextFields();
		}

		private static void ToggleTextFields()
		{
			if (_paused)
			{
				foreach (var key in _textFieldStates.Keys.ToList())
					if (UI.IsActive(key))
					{
						_textFieldStates[key] = true;
						UI.Display(key, false);
					}
			}
			else
			{
				foreach (var key in _textFieldStates.Keys.ToList())
					if (_textFieldStates[key])
					{
						_textFieldStates[key] = false;
						UI.Display(key);
					}
			}
		}

		public void Resume() => TogglePauseMenu();

		public new void ToMainMenu()
		{
			TogglePauseMenu();
			Game.Restart();
			base.ToMainMenu();
		}
	}
}