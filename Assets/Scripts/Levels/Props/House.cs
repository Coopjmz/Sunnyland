using UnityEngine;

namespace Levels
{
	class House: MonoBehaviour
	{
		//Events
		public void Enter()
		{
			Game.Reset();
			SceneLoader.Load(Scene.Next);
		}
	}
}