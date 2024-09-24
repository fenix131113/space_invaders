using ShootingSystem;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
	public class PlayerInputHandler : ITickable
	{
		private PlayerMovement _playerMovement;
		private AShooter _playerShooter;

		[Inject]
		private void Construct(PlayerMovement playerMovement, AShooter playerShooter)
		{
			_playerMovement = playerMovement;
			_playerShooter = playerShooter;
		}

		public void Tick()
		{
			MoveInputHandler();
			ShootInputHandler();
		}

		private void MoveInputHandler()
		{
			var horizontalMove = Input.GetAxisRaw("Horizontal");
			_playerMovement.MovePlayer(new Vector2(horizontalMove, 0));
		}

		private void ShootInputHandler()
		{
			if(Input.GetKeyDown(KeyCode.Space))
				_playerShooter.Shoot();
		}
	}
}