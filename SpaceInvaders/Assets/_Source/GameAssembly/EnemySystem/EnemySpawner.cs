using System.Collections.Generic;
using EnemySystem.Data;
using UnityEngine;
using Zenject;

namespace EnemySystem
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private Transform spawnParent;

		public List<List<Enemy>> EnemyRaws { get; private set; } = new();

		private EnemySpawnSettings _spawnerSettings;
		private Vector3 _startPosition;

		[Inject]
		public void Construct(EnemySpawnSettings spawnerSettings)
		{
			_spawnerSettings = spawnerSettings;
		}

		public void Awake()
		{
			Spawn();
		}
		

		private void Spawn()
		{
			CalculateStartPosition();
			Vector2 spawnPosition = _startPosition;
			for (var i = 1; i < _spawnerSettings.Raws + 1; i++)
			{
				if (EnemyRaws.Count < i)
					EnemyRaws.Add(new List<Enemy>());
				
				for (var j = 1; j < _spawnerSettings.Columns + 1; j++)
				{
					EnemyRaws[i - 1].Add(Instantiate(_spawnerSettings.EnemyPrefab, spawnPosition, Quaternion.identity,
						spawnParent));
					spawnPosition.x += _spawnerSettings.EnemyPrefab.transform.localScale.x + _spawnerSettings.XSpacing;
				}

				spawnPosition.x = _startPosition.x;
				spawnPosition.y -= _spawnerSettings.EnemyPrefab.transform.localScale.y + _spawnerSettings.YSpacing;
			}
			
			EnemyRaws.Reverse();
		}

		private void CalculateStartPosition()
		{
			Vector2 centerPosition = spawnParent.position;

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