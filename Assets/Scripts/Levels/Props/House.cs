using UnityEngine;

namespace Levels
{
	sealed class House : MonoBehaviour
	{
		public void Enter()
		{
			Game.Restart();
			SceneLoader.Load(Scene.Next);
		}
	}
}