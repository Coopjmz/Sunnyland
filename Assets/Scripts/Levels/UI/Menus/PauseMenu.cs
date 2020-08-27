using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Levels
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

		public void Resume()
		{
			TogglePauseMenu();
		}

		public new void ToMainMenu()
		{
			TogglePauseMenu();
			Game.Restart();
			base.ToMainMenu();
		}

		internal static void TogglePauseMenu()
		{
			_paused = !_paused;

			UI.Display("Pause Menu", _paused);
			Time.timeScale = _paused ? 0f : 1f;
			Game.IsInputEnabled = !_paused;

			//Toggles all text fields on screen
			if(_paused)
			{
				foreach (var key in _textFieldStates.Keys.ToList())
					if(UI.IsActive(key))
					{
						_textFieldStates[key] = true;
						UI.Display(key, false);
					}
			}
			else
			{
				foreach (var key in _textFieldStates.Keys.ToList())
					if(_textFieldStates[key])
					{
						_textFieldStates[key] = false;
						UI.Display(key);
					}
			}
		}
	}
}