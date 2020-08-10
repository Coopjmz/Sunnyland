using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Levels
{
	class PauseMenu: Menu
	{
		//Static variables
		static bool paused;

		//Text fields
		static Dictionary<string, bool> textFieldStates;

		//Methods
		void Awake()
		{
			//Initialize text field states
			textFieldStates = new Dictionary<string, bool>
			{
				{"Stats", true},
				{"Sign", false},
				{"Interact", false},
				{"Tutorial", false}
			};
		}

		//Events
		public void Resume()
		{
			TogglePauseMenu();
		}

		public new void ToMainMenu()
		{
			TogglePauseMenu();
			base.ToMainMenu();
		}

		//Static methods
		internal static void TogglePauseMenu()
		{
			paused = !paused;

			//Toggle Puse Menu screen
			UI.Display("Pause Menu", paused);

			//Toggle game time
			Time.timeScale = paused ? 0f : 1f;

			//Toggle input
			Game.IsInputEnabled = !paused;

			//Toggle text fields
			if(paused)
			{
				foreach(var key in textFieldStates.Keys.ToList())
					if(UI.IsActive(key))
					{
						textFieldStates[key] = true;
						UI.Display(key, false);
					}
			}
			else
			{
				foreach(var key in textFieldStates.Keys.ToList())
					if(textFieldStates[key])
					{
						textFieldStates[key] = false;
						UI.Display(key);
					}
			}
		}
	}
}