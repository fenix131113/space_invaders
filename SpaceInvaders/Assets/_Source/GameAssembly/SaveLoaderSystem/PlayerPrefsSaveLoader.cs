using PlayerSystem;
using UnityEngine;
using Zenject;

namespace SaveLoaderSystem
{
	public class PlayerPrefsSaveLoader : ISaveLoader, IInitializable
	{
		private readonly PlayerScore _playerPlayerScore;

		[Inject]
		public PlayerPrefsSaveLoader(PlayerScore playerScore)
		{
			_playerPlayerScore = playerScore;
		}

		public void Initialize()
		{
			TryLoad();
		}

		public void Save()
		{
			PlayerPrefs.SetInt("HighScore", _playerPlayerScore.HighScore);
		}

		public bool TryLoad()
		{
			if (TryGetHighScore(out var highScore))
				_playerPlayerScore.LoadData(highScore);

			return true;
		}

		private static bool TryGetHighScore(out int highScore)
		{
			if (PlayerPrefs.HasKey("HighScore"))
			{
				highScore = PlayerPrefs.GetInt("HighScore");
				return true;
			}

			highScore = 0;
			return false;
		}
	}
}