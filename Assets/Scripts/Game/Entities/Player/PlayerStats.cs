using System;

using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerStats : MonoBehaviour
	{
		public static event Action<string, object> OnStatChange;

		private static byte _maxLives;

		private byte _lives;
		private byte _cherries;

		private PlayerController _player;

		public byte Lives
		{
			get => _lives;
			set
			{
				_lives = value;
				OnStatChange?.Invoke("Life", value);
				PlayerPrefs.SetInt("Life", value);
			}
		}

		public byte Cherries
		{
			get => _cherries;
			set
			{
				_cherries = value;
				OnStatChange?.Invoke("Cherry", value);
			}
		}

		private void Awake() => _player = GetComponent<PlayerController>();

		private void Start()
		{
			if (!PlayerPrefs.HasKey("Life"))
			{
				_maxLives = _player.Data.MaxLives;
				ResetLives();
			}

			_lives = (byte)PlayerPrefs.GetInt("Life");
		}

		private void OnDestroy() => Cherries = 0;
		private void OnApplicationQuit() => PlayerPrefs.DeleteAll();

		public static void ResetLives() => PlayerPrefs.SetInt("Life", _maxLives);
	}
}