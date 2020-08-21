using UnityEngine;

namespace Levels
{
	class House: MonoBehaviour
	{
		//Events
		public void Enter()
		{
			Game.Restart();
			SceneLoader.Load(Scene.Next);
		}
	}
}