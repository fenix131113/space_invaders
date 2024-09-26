using System;
using UnityEngine;

namespace PlayerSystem
{
	public class PlayerScore
	{
		public int Score { get; set; }
		public int HighScore { get; set; }

		public event Action OnScoreChanged;
		
		public void AddScore(uint score)
		{
			Score += (int)score;

			if (Score > HighScore)
			{
				HighScore = Score;
			}

			OnScoreChanged?.Invoke();
		}

		public void LoadData(int highScore)
		{
			HighScore = highScore;
			
			OnScoreChanged?.Invoke();
		}
	}
}