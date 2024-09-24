using System;
using EnemySystem.Data;
using UnityEngine;
using Zenject;

namespace EnemySystem
{
	public class EnemyContainerMovement : MonoBehaviour
	{
		[SerializeField] private float speed;

		private EnemySpawnSettings _enemySpawnSettings;
		private bool _isRight = true;
		private float _rightStop;
		private float _leftStop;

		[Inject]
		private void Construct(EnemySpawnSettings enemySpawnSettings)
		{
			_enemySpawnSettings = enemySpawnSettings;
		}

		private void Awake()
		{
			CalculateStopCoords();
		}

		private void Update()
		{
			Move();
		}

		private void CalculateStopCoords()
		{
			_rightStop = Camera.main!.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - (float)_enemySpawnSettings.Columns / 2 *
			            (_enemySpawnSettings.EnemyPrefab.transform.localScale.x + _enemySpawnSettings.XSpacing);
			
			_leftStop = Camera.main!.ScreenToWorldPoint(new Vector2(0, 0)).x + (float)_enemySpawnSettings.Columns / 2 *
				(_enemySpawnSettings.EnemyPrefab.transform.localScale.x + _enemySpawnSettings.XSpacing);
		}
		
		private void Move()
		{
			if (_isRight)
				transform.position += transform.right * (speed * Time.deltaTime);
			else
				transform.position -= transform.right * (speed * Time.deltaTime);

			if (transform.position.x > _rightStop && _isRight)
				_isRight = false;
			else if (transform.position.x < _leftStop && !_isRight)
				_isRight = true;
		}
	}
}