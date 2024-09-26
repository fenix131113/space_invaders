using System;
using System.Collections.Generic;
using EnemySystem.Data;
using PlayerSystem;
using UnityEngine;
using Zenject;

namespace EnemySystem
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private Transform spawnParent;
		
		public int EnemyLeft { get; private set; }
		public List<List<Enemy>> EnemyColumns => _currentSpawner.EnemyColumns;

		private EnemySpawnSettings _spawnerSettings;
		private PlayerScore _playerScore;
		private PlayerHealth _playerHealth;
		private EnemySpawnService _currentSpawner;
		
		public event Action OnEnemyCountChanged;

		[Inject]
		public void Construct(EnemySpawnSettings spawnerSettings, PlayerScore playerScore, PlayerHealth playerHealth)
		{
			_spawnerSettings = spawnerSettings;
			_playerScore = playerScore;
			_playerHealth = playerHealth;
		}

		private void Awake()
		{
			Spawn();
		}

		private void Spawn()
		{
			_currentSpawner = new EnemySpawnService(_spawnerSettings, _playerScore, _playerHealth, DecreaseEnemies, spawnParent);
			
			_currentSpawner.Spawn();

			EnemyLeft = _spawnerSettings.Raws * _spawnerSettings.Columns;
		}
		
		private void DecreaseEnemies()
		{
			EnemyLeft--;

			OnEnemyCountChanged?.Invoke();
		}
	}
}