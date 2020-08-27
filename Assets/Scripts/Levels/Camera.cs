using UnityEngine;
using Cinemachine;

namespace Levels
{
	sealed class Camera : MonoBehaviour
	{
		private Player _player;
		private CinemachineBrain _cinemachine;

		private void Start()
		{
			_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			_cinemachine = GetComponent<CinemachineBrain>();
		}

		private void Update()
		{
			if(!_player.IsAlive)
			{
				enabled = false;
				_cinemachine.enabled = false;
			}
		}
	}
}