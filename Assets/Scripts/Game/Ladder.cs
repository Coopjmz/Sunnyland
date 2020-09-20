using UnityEngine;

using Sunnyland.Game.Map;

namespace Sunnyland.Game
{
	enum LadderPart
	{
		Top,
		Bottom
	}

	[RequireComponent(typeof(BoxCollider2D))]
	sealed class Ladder : MonoBehaviour
	{
		[SerializeField] private BoxCollider2D _top = default;
		[SerializeField] private BoxCollider2D _bottom = default;

		public void EnablePlatform() => _top.enabled = true;
		public void DisablePlatform() => _top.enabled = false;

		public bool IsTouchingLadderPart(LadderPart ladderPart)
		{
			switch (ladderPart)
			{
				case LadderPart.Top:
					return _top.IsTouchingLayers(Layers.Player);
				case LadderPart.Bottom:
					return _bottom.IsTouchingLayers(Layers.Player);
				default: return default;
			}
		}
	}
}