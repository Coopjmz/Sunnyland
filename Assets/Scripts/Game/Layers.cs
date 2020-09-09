using UnityEngine;

namespace Sunnyland.Game
{
    sealed class Layers : MonoBehaviour
    {
        [SerializeField] private LayerMask _ground = default;
        [SerializeField] private LayerMask _topLadder = default;
        [SerializeField] private LayerMask _bottomLadder = default;
        [SerializeField] private LayerMask _enemy = default;

        internal static LayerMask Ground { get; private set; }
        internal static LayerMask TopLadder { get; private set; }
        internal static LayerMask BottomLadder { get; private set; }
        internal static LayerMask Enemy { get; private set; }

        private void Start()
		{
            Ground = _ground;
            TopLadder = _topLadder;
            BottomLadder = _bottomLadder;
            Enemy = _enemy;
        }
	}
}