using UnityEngine;

namespace Sunnyland.Game.Tutorials
{
	sealed class TutorialManager : MonoBehaviour
	{
		public static TutorialManager Instance { get; private set; }

		public bool IsTutorialDisplayed { get; private set; }

		private void Start()
		{
			if (!Instance)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else Destroy(gameObject);
		}

		public void DisplayTutorial(bool enabled)
		{
			IsTutorialDisplayed = enabled;
			UI.Instance.Display("Tutorial", enabled);
		}

		public void ResetTutorials() => Destroy(Instance.gameObject);
	}
}