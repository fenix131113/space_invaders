using System;
using System.Collections.Generic;
using EnemySystem.Data;
using PlayerSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EnemySystem
{
	public class EnemySpawnService
	{
		private readonly EnemySpawnSettings _spawnerSettings;
		private readonly PlayerScore _playerScore;
		private readonly PlayerHealth _playerHealth;
		
		public List<List<Enemy>> EnemyColumns { get;} = new();
		
		private Vector3 _startPosition;
		private readonly Transform _spawnParent;
		private readonly Action _onEnemyDeadAction;
		
		public EnemySpawnService(EnemySpawnSettings spawnerSettings, PlayerScore playerScore, PlayerHealth playerHealth, Action onEnemyDead, Transform spawnParent)
		{
			_spawnerSettings = spawnerSettings;
			_playerScore = playerScore;
			_playerHealth = playerHealth;
			_onEnemyDeadAction = onEnemyDead;
			_spawnParent = spawnParent;

#if UNITY_EDITOR
			if(!spawnParent)
				Debug.LogError("The spawn parent can't be null!");
#endif
		}
		
		public void Spawn()
		{
			CalculateStartPosition();
			Vector2 spawnPosition = _startPosition;
			for (var i = 1; i < _spawnerSettings.Raws + 1; i++)
			{
				for (var j = 1; j < _spawnerSettings.Columns + 1; j++)
				{
					if (EnemyColumns.Count < j)
						EnemyColumns.Add(new List<Enemy>());

					EnemyColumns[j - 1].Add(Object.Instantiate(_spawnerSettings.EnemyPrefab, spawnPosition,
						Quaternion.identity,
						_spawnParent));

					EnemyColumns[j - 1][^1].Init(_playerScore, _playerHealth);
					EnemyColumns[j - 1][^1].OnDeath += _onEnemyDeadAction;

					spawnPosition.x += _spawnerSettings.EnemyPrefab.transform.localScale.x + _spawnerSettings.XSpacing;
				}

				spawnPosition.x = _startPosition.x;
				spawnPosition.y -= _spawnerSettings.EnemyPrefab.transform.localScale.y + _spawnerSettings.YSpacing;
			}

			EnemyColumns.Reverse();
		}
		
		private void CalculateStartPosition()
		{
			Vector2 centerPosition = _spawnParent.position;

			var topLeftPosition =
				new Vector2(
					(int)((float)-_spawnerSettings.Columns / 2) * (_spawnerSettings.EnemyPrefab.transform.localScale.x +
					                                               _spawnerSettings.XSpacing),
					(int)((float)_spawnerSettings.Raws / 2) * (_spawnerSettings.EnemyPrefab.transform.localScale.y +
					                                           _spawnerSettings.YSpacing));

			_startPosition = centerPosition + topLeftPosition;
		}
	}
}