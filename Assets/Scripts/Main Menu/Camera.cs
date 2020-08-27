using UnityEngine;

namespace MainMenu
{
	sealed class Camera : MonoBehaviour
	{
		[SerializeField] private float end = 187f;
		[SerializeField] private AudioSource music = default;

		private float _start, _unitsPerSecond;

		private void Start()
		{
			_start = transform.position.x;
			_unitsPerSecond = (end - _start) / music.clip.length;
		}

		private void Update()
		{
			if(transform.position.x < end)
				transform.Translate(Vector3.right * Time.deltaTime * _unitsPerSecond);
			else transform.position = new Vector3(_start, transform.position.y, -10f);
		}
	}
}