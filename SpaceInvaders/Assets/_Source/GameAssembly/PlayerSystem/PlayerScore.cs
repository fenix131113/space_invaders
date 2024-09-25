using System;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
	public class PlayerScore : IInitializable

	{
		public int Score { get; set; }
		public int HighScore { get; set; }

		public event Action OnScoreChanged;

		public void Initialize()
		{
			LoadHighScore();
		}
		
		public void AddScore(uint score)
		{
			Score += (int)score;

			if (Score > HighScore)
			{
				HighScore = Score;
				SaveHighScore();
			}

			OnScoreChanged?.Invoke();
		}

		private void SaveHighScore()
		{
			PlayerPrefs.SetInt("HighScore", HighScore);
		}

		private void LoadHighScore()
		{
			if (PlayerPrefs.HasKey("HighScore"))
				HighScore = PlayerPrefs.GetInt("HighScore");
		}
	}
}