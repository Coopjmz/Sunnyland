using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Sunnyland.Game.Entities.Player;

namespace Sunnyland.Game.Menus
{
	sealed class PauseMenu : Menu
	{
		private static bool _paused;

		private static Dictionary<string, bool> _textFieldHidden;

		private void Awake()
		{
			_textFieldHidden = new Dictionary<string, bool>
			{
				{"HUD", default},
				{"Sign", default},
				{"Interact", default},
				{"Tutorial", default}
			};
		}

		public static void Toggle()
		{
			_paused = !_paused;

			UI.Instance.Display("Pause Menu", _paused);
			Time.timeScale = _paused ? 0f : 1f;
			FindObjectOfType<PlayerInput>().enabled = !_paused;

			ToggleTextFields();
		}

		private static void ToggleTextFields()
		{
			if (_paused)
			{
				foreach (var key in _textFieldHidden.Keys.ToList())
					if (UI.Instance.IsActive(key))
					{
						_textFieldHidden[key] = true;
						UI.Instance.Display(key, false);
					}
			}
			else
			{
				foreach (var key in _textFieldHidden.Keys.ToList())
					if (_textFieldHidden[key])
					{
						_textFieldHidden[key] = false;
						UI.Instance.Display(key);
					}
			}
		}

		public void Resume() => Toggle();

		public new void ToMainMenu()
		{
			Toggle();
			Game.Restart();
			base.ToMainMenu();
		}
	}
}