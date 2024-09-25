using System;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
	public class PlayerInputHandler : ITickable
	{
		private PlayerMovement _playerMovement;

		public event Action OnPlayerShoot; 

		[Inject]
		private void Construct(PlayerMovement playerMovement)
		{
			_playerMovement = playerMovement;
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
				OnPlayerShoot?.Invoke();
		}
	}
}