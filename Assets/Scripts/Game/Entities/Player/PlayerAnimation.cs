using System.Collections;

using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerAnimation : MonoBehaviour
    {
		private enum State
		{
			Idle,
			Running,
			Crouching,
			Jumping,
			Falling,
			Climbing
		}

		private State _state;

		private PlayerController _player;

		private void Awake() => _player = GetComponent<PlayerController>();
		private void Start() => StartCoroutine(Flicker(1f));
		private void Update() => AnimationUpdate();

		private void AnimationUpdate()
		{
			_player.Animator.speed = 1f;

			if (_player.Movement.Climbing)
			{
				_state = State.Climbing;

				if (Mathf.Abs(_player.Rigidbody.velocity.y) < 2f)
					_player.Animator.speed = 0f;
			}
			else if (_player.Rigidbody.velocity.y < -Game.EPSILON)
				_state = State.Falling;
			else if (_player.Rigidbody.velocity.y > Game.EPSILON)
				_state = State.Jumping;
			else if (_player.Movement.Crouching)
				_state = State.Crouching;
			else if (Mathf.Abs(_player.Rigidbody.velocity.x) > Game.EPSILON)
				_state = State.Running;
			else _state = State.Idle;

			_player.Animator.SetInteger("State", (int)_state);
		}

		public IEnumerator Flicker(float timer)
		{
			do
			{
				_player.SpriteRenderer.enabled = !_player.SpriteRenderer.enabled;

				timer -= .1f;
				yield return new WaitForSeconds(.1f);
			}
			while (timer > 0f);

			_player.SpriteRenderer.enabled = true;
		}
	}
}