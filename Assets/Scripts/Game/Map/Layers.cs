using UnityEngine;

namespace Sunnyland.Game.Map
{
	sealed class Layers : MonoBehaviour
	{
		[SerializeField] private LayerMask _player = default;
		[SerializeField] private LayerMask _enemy = default;

		public static LayerMask Player { get; private set; }
		public static LayerMask Enemy { get; private set; }

		private void Start()
		{
			Player = _player;
			Enemy = _enemy;
		}
	}
}