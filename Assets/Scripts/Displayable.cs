using System.Collections.Generic;

using UnityEngine;

namespace Sunnyland
{
	abstract class Displayable : MonoBehaviour
	{
		protected Dictionary<string, GameObject> Objects { private get; set; }

		public void Display(string key, bool enable = true) => Objects[key].SetActive(enable);
		public bool IsActive(string key) => Objects[key] && Objects[key].activeSelf;
	}
}