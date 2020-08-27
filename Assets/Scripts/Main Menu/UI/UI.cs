using System.Collections.Generic;

using UnityEngine;

namespace MainMenu
{
	sealed class UI : MonoBehaviour
	{
		[SerializeField] private GameObject mainMenu = default;
		[SerializeField] private GameObject credits = default;
		private static Dictionary<string, GameObject> _objects;

		private void Start()
		{
			_objects = new Dictionary<string, GameObject>
			{
				{"Main Menu", mainMenu},
				{"Credits", credits}
			};
		}

		internal static void Display(string key, bool enable = true)
		{
			_objects[key].SetActive(enable);
		}
	}
}