using EnemySystem;
using PlayerSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace Core.View
{
	public class WinLooseView : MonoBehaviour
	{
		[SerializeField] private TMP_Text infoLabel;
		[SerializeField] private GameObject screenBlocker;
		
		private const string WinMessage = "You've won\nPress R to restart";
		private const string LooseMessage = "You've lost\nPress R to restart";

		private PlayerHealth _playerHealth;
		private EnemySpawner _enemySpawner;
		
		[Inject]
		private void Construct(PlayerHealth playerHealth, EnemySpawner enemySpawner)
		{
			_playerHealth = playerHealth;
			_enemySpawner = enemySpawner;
		}

		private void Awake()
		{
			Bind();
		}

		private void Loose()
		{
			infoLabel.text = LooseMessage;
			screenBlocker.SetActive(true);
		}

		private void Win()
		{
			if(_enemySpawner.EnemyLeft > 0)
				return;
			
			infoLabel.text = WinMessage;
			screenBlocker.SetActive(true);	
		}
		
		private void Bind()
		{
			_playerHealth.OnDead += Loose;
			_enemySpawner.OnEnemyCountChanged += Win;
		}

		private void Expose()
		{
			_playerHealth.OnDead -= Loose;
			_enemySpawner.OnEnemyCountChanged -= Win;
		}
		
		private void OnDestroy() => Expose();

		private void OnApplicationQuit() => Expose();
	}
}
