using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EnemySystem;
using EnemySystem.Data;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ShootingSystem.Enemy
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

			List<List<EnemySystem.Enemy>> selectedColumns;

			selectedColumns = _enemySpawner.EnemyColumns.Where(column => column.Any(enemy => enemy.EnemyGFX.activeSelf))
				.ToList();

			if (selectedColumns.Count == 0)
				return;

			_bullet.transform.position =
				selectedColumns[Random.Range(0, selectedColumns.Count)].Where(enemy => enemy.EnemyGFX.activeSelf)
					.ToList()[^1].ShootPoint.position;
			
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