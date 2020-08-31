using System.Collections.Generic;

using UnityEngine;

namespace Sunnyland.MainMenu.UI
{
	sealed class UI : MonoBehaviour
	{
		private static Dictionary<string, GameObject> _objects;

		[Header("Menus")]
		[SerializeField] private GameObject _mainMenu = default;
		[SerializeField] private GameObject _credits = default;

		private void Start()
		{
			_objects = new Dictionary<string, GameObject>
			{
				{"Main Menu", _mainMenu},
				{"Credits", _credits}
			};
		}

		internal static void Display(string key, bool enable = true) => _objects[key].SetActive(enable);
	}
}