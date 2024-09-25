using System.Collections;
using EnemySystem.Data;
using PlayerSystem;
using UnityEngine;
using Zenject;

namespace EnemySystem
{
	public class EnemyContainerMovement : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float downMoveInterval;
		[SerializeField] private float downMoveLenght;

		private EnemySpawnSettings _enemySpawnSettings;
		private PlayerHealth _playerHealth;
		private bool _isRight = true;
		private float _rightStop;
		private float _leftStop;

		[Inject]
		private void Construct(EnemySpawnSettings enemySpawnSettings, PlayerHealth playerHealth)
		{
			_enemySpawnSettings = enemySpawnSettings;
			_playerHealth = playerHealth;
		}

		private void Awake()
		{
			CalculateStopCoords();
			StartCoroutine(MoveDownCoroutine());
		}

		private void Update()
		{
			MoveHorizontal();
		}

		private void CalculateStopCoords()
		{
			_rightStop = Camera.main!.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x -
			             (float)_enemySpawnSettings.Columns / 2 *
			             (_enemySpawnSettings.EnemyPrefab.transform.localScale.x + _enemySpawnSettings.XSpacing);

			_leftStop = Camera.main!.ScreenToWorldPoint(new Vector2(0, 0)).x + (float)_enemySpawnSettings.Columns / 2 *
				(_enemySpawnSettings.EnemyPrefab.transform.localScale.x + _enemySpawnSettings.XSpacing);
		}

		private void MoveHorizontal()
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

		private IEnumerator MoveDownCoroutine()
		{
			while (!_playerHealth.IsDead)
			{
				yield return new WaitForSeconds(downMoveInterval);
				transform.position -= transform.up * downMoveLenght;
			}
		}
	}
}