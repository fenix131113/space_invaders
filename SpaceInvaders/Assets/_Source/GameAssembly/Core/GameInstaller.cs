using PlayerSystem;
using PlayerSystem.Data;
using UnityEngine;
using Zenject;

namespace Core
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private PlayerSettings playerSettings;
		[SerializeField] private PlayerMovement playerMovement;

		public override void InstallBindings()
		{
			BindPlayer();
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