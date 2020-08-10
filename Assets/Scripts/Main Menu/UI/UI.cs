using System.Collections.Generic;

using UnityEngine;

namespace MainMenu
{
    class UI: MonoBehaviour
    {
		//Menus
		[SerializeField] GameObject mainMenu = default;
		[SerializeField] GameObject credits = default;
		static Dictionary<string, GameObject> objects;

		//Methods
		void Start()
		{
			//Initialize menus
			objects = new Dictionary<string, GameObject>
			{
				{"Main Menu", mainMenu},
				{"Credits", credits}
			};
		}

		internal static void Display(string key, bool enable = true)
		{
			objects[key].SetActive(enable);
		}
	}
}