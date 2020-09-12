using System.Collections.Generic;

using UnityEngine;

namespace Sunnyland.MainMenu
{
	sealed class UI : Displayable
	{
		[Header("Menus")]
		[SerializeField] private GameObject _mainMenu = default;
		[SerializeField] private GameObject _credits = default;

		public static UI Instance { get; private set; }

		private void Start()
		{
			if (!Instance)
			{
				Instance = this;
				Objects = new Dictionary<string, GameObject>
				{
					{"Main Menu", _mainMenu},
					{"Credits", _credits}
				};
			}
			else Destroy(gameObject);
		}
	}
}