using EnemySystem;
using EnemySystem.Data;
using PlayerSystem;
using PlayerSystem.Data;
using UnityEngine;
using Zenject;
using PlayerInputHandler = PlayerSystem.PlayerInputHandler;

namespace Core
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private PlayerMovement playerMovement;

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
		}
	}
}