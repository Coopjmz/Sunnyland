using System;

using UnityEngine;

namespace Sunnyland.Game.Entities.Player
{
	[RequireComponent(typeof(PlayerController))]
	sealed class PlayerStats : MonoBehaviour
	{
		internal static event Action<string, object> OnStatChange;

		internal const byte MAX_LIVES = 3;

		private byte _lives;
		private byte _cherries;

		internal byte Lives
		{
			get => _lives;
			set
			{
				_lives = value;
				OnStatChange?.Invoke("Life", value);
				PlayerPrefs.SetInt("Life", value);
			}
		}

		internal byte Cherries
		{
			get => _cherries;
			set
			{
				_cherries = value;
				OnStatChange?.Invoke("Cherry", value);
			}
		}

		private void Start()
		{
			if (!PlayerPrefs.HasKey("Life")) ResetLives();

			_lives = (byte)PlayerPrefs.GetInt("Life");
		}

		private void OnDestroy() => Cherries = 0;
		private void OnApplicationQuit() => PlayerPrefs.DeleteAll();

		internal static void ResetLives() => PlayerPrefs.SetInt("Life", MAX_LIVES);
	}
}