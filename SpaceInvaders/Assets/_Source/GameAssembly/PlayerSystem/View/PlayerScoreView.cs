using TMPro;
using UnityEngine;
using Zenject;

namespace PlayerSystem.View
{
	public class PlayerScoreView : MonoBehaviour
	{
		[SerializeField] private TMP_Text scoreText;
		[SerializeField] private TMP_Text highScoreText;

		private PlayerScore _playerScore;
		
		[Inject]
		private void Construct(PlayerScore playerScore)
		{
			_playerScore = playerScore;
		}

		private void RefreshVisual()
		{
			scoreText.text = $"Score\n{_playerScore.Score}";
			highScoreText.text = $"High Score\n{_playerScore.HighScore}";
		}
		
		private void Awake()
		{
			RefreshVisual();
			
			Bind();
		}

		private void Bind()
		{
			_playerScore.OnScoreChanged += RefreshVisual;
		}

		private void Expose()
		{
			_playerScore.OnScoreChanged -= RefreshVisual;
		}

		private void OnDestroy() => Expose();

		private void OnApplicationQuit() => Expose();
	}
}