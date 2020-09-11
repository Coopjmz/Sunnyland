using UnityEngine;

namespace Sunnyland.Game.Entities.Enemies.Patrol
{
	abstract class PatrolEnemy : Enemy
	{
		protected sbyte Direction { get; private set; }

		protected float LeftBoundX { private get; set; }
		protected float RightBoundX { private get; set; }

		protected void Start() => Direction = (sbyte)transform.localScale.x;

		protected void PatrolAI()
		{
			if (Direction == 1 && transform.position.x <= LeftBoundX)
			{
				Direction = -1;
				transform.localScale = new Vector3(Direction, 1f);
			}
			else if (Direction == -1 && transform.position.x >= RightBoundX)
			{
				Direction = 1;
				transform.localScale = new Vector3(Direction, 1f);
			}
		}
	}
}