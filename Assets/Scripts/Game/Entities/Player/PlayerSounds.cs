using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerSounds : MonoBehaviour
    {
		[SerializeField] private AudioSource _footstepsSFX = default;
		[SerializeField] private AudioSource _jumpSFX = default;
		[SerializeField] private AudioSource _deathSFX = default;

		internal void PlayJumpSound() => _jumpSFX.Play();
		internal void PlayDeathSound() => _deathSFX.Play();

		private void PlayFootstepsSound() => _footstepsSFX.Play();
    }
}