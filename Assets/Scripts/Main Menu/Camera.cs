using UnityEngine;

namespace Sunnyland.MainMenu
{
	sealed class Camera : MonoBehaviour
	{
		[SerializeField] private float _end = 187f;
		[SerializeField] private AudioSource _music = default;

		private float _start, _unitsPerSecond;

		private void Start()
		{
			_start = transform.position.x;
			_unitsPerSecond = (_end - _start) / _music.clip.length;
		}

		private void LateUpdate()
		{
			if (transform.position.x < _end)
				transform.Translate(Vector3.right * Time.deltaTime * _unitsPerSecond);
			else transform.position = new Vector3(_start, transform.position.y, -10f);
		}
	}
}