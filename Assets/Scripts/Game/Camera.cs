using UnityEngine;
using Cinemachine;

using Sunnyland.Game.Entities.Player;

namespace Sunnyland.Game
{
	sealed class Camera : MonoBehaviour
	{
		private PlayerController _player;
		private CinemachineBrain _cinemachine;

		private void Start()
		{
			_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
			_cinemachine = GetComponent<CinemachineBrain>();
		}

		private void LateUpdate()
		{
			if (!_player.IsAlive)
			{
				enabled = false;
				_cinemachine.enabled = false;
			}
		}
	}
}