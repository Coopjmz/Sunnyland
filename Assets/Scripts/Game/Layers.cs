using UnityEngine;

namespace Sunnyland.Game
{
	sealed class Layers : MonoBehaviour
	{
		[SerializeField] private LayerMask _ground = default;
		[SerializeField] private LayerMask _topLadder = default;
		[SerializeField] private LayerMask _bottomLadder = default;
		[SerializeField] private LayerMask _enemy = default;

		public static LayerMask Ground { get; private set; }
		public static LayerMask TopLadder { get; private set; }
		public static LayerMask BottomLadder { get; private set; }
		public static LayerMask Enemy { get; private set; }

		private void Start()
		{
			Ground = _ground;
			TopLadder = _topLadder;
			BottomLadder = _bottomLadder;
			Enemy = _enemy;
		}
	}
}