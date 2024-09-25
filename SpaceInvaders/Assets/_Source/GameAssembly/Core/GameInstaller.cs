using EnemySystem;
using EnemySystem.Data;
using PlayerSystem;
using PlayerSystem.Data;
using ShootingSystem;
using UnityEngine;
using Zenject;
using PlayerInputHandler = PlayerSystem.PlayerInputHandler;

namespace Core
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private PlayerMovement playerMovement;
		[SerializeField] private AShooter playerShooter;
		[SerializeField] private EnemySpawner enemySpawner;
		[SerializeField] private PlayerHealth playerHealth;

		[SerializeField] private PlayerSettings playerSettings;
		[SerializeField] private EnemySpawnSettings enemySpawnSettings;
		
		public override void InstallBindings()
		{
			BindPlayer();
			BindEnemies();
		}

		private void BindEnemies()
		{
			Container.Bind<EnemySpawnSettings>()
				.FromInstance(enemySpawnSettings)
				.AsSingle()
				.NonLazy();

			Container.Bind<EnemySpawner>()
				.FromInstance(enemySpawner)
				.AsSingle()
				.NonLazy();
		}
		
		private void BindPlayer()
		{
			Container.BindInterfacesAndSelfTo<PlayerInputHandler>()
				.AsSingle()
				.NonLazy();

			Container.Bind<PlayerSettings>()
				.FromInstance(playerSettings)
				.AsSingle()
				.NonLazy();

			Container.Bind<PlayerMovement>()
				.FromInstance(playerMovement)
				.AsSingle()
				.NonLazy();

			Container.Bind<AShooter>()
				.FromInstance(playerShooter)
				.AsSingle()
				.NonLazy();

			Container.BindInterfacesAndSelfTo<PlayerScore>()
				.AsSingle()
				.NonLazy();

			Container.Bind<PlayerHealth>()
				.FromInstance(playerHealth)
				.AsSingle()
				.NonLazy();
		}
	}
}