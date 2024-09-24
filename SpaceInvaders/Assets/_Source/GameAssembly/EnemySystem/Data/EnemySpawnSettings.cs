using UnityEngine;

namespace EnemySystem.Data
{
	[CreateAssetMenu(fileName = "New Enemy Spawner Settings", menuName = "ScriptableObjects/New Enemy Spawner Settings")]
	public class EnemySpawnSettings : ScriptableObject
	{
		[field: SerializeField] public int Columns { get; private set; }
		[field: SerializeField] public int Raws { get; private set; }
		[field: SerializeField] public float XSpacing { get; private set; }
		[field: SerializeField] public float YSpacing { get; private set; }
		[field: SerializeField] public Enemy EnemyPrefab { get; private set; }
		[field: SerializeField] public GameObject EnemyBulletPrefab { get; private set; }
	}
}