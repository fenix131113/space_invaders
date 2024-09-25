using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EnemySystem.Data;
using ShootingSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace EnemySystem
{
	public class EnemyShooter : AShooter
	{
		[SerializeField] private float minTimeBetweenShots;
		[SerializeField] private float maxTimeBetweenShots;

		private EnemySpawnSettings _spawnerSettings;
		private EnemySpawner _enemySpawner;
		private GameObject _bullet;

		[Inject]
		public void Construct(EnemySpawnSettings spawnerSettings, EnemySpawner enemySpawner)
		{
			_spawnerSettings = spawnerSettings;
			_enemySpawner = enemySpawner;
		}

		private void Awake()
		{
			InitializeBullet();
			StartCoroutine(ShootCoroutine());
		}

		private void InitializeBullet()
		{
			_bullet = Instantiate(_spawnerSettings.EnemyBulletPrefab);
			_bullet.SetActive(false);
		}

		public override void Shoot()
		{
			if (_bullet.activeSelf)
				return;

			List<Enemy> selectedRaw = new();

			foreach (var raw in _enemySpawner.EnemyRaws)
			{
				if (raw.All(enemy => !enemy.EnemyGFX.activeSelf))
					continue;

				selectedRaw = raw;
				selectedRaw = selectedRaw.Where(enemy => enemy.EnemyGFX.activeSelf).ToList();
				break;
			}

			if(selectedRaw.Count == 0)
				return;
			
			_bullet.transform.position = selectedRaw[Random.Range(0, selectedRaw.Count)].ShootPoint.position;
			_bullet.SetActive(true);
		}

		[SuppressMessage("ReSharper", "FunctionRecursiveOnAllPaths")]
		private IEnumerator ShootCoroutine()
		{
			yield return new WaitForSeconds(Random.Range(minTimeBetweenShots, maxTimeBetweenShots));

			Shoot();

			StartCoroutine(ShootCoroutine());
		}
	}
}