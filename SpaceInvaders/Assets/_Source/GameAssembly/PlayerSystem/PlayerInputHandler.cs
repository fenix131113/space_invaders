using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Zenject;

namespace PlayerSystem
{
	public class PlayerInputHandler : ITickable, IInitializable
	{
		private readonly PlayerMovement _playerMovement;
		private readonly GameRestart _gameRestart;
		private Controls _inputControls;

		public event Action OnPlayerShoot;

		[Inject]
		private PlayerInputHandler(PlayerMovement playerMovement, GameRestart gameRestart)
		{
			_playerMovement = playerMovement;
			_gameRestart = gameRestart;
		}

		~PlayerInputHandler() => Expose();

		public void Tick()
		{
			MoveInputHandler();
		}

		public void Initialize()
		{
			_inputControls = new Controls();
			
			Bind();
		}
		
		private void Bind()
		{
			_inputControls.Default.Restart.started += RestartInputHandler;
			_inputControls.Default.Shoot.started += ShootInputHandler;
			
			_inputControls.Enable();
		}

		private void Expose()
		{
			_inputControls.Default.Restart.started -= RestartInputHandler;
			_inputControls.Default.Shoot.started -= ShootInputHandler;
			
			_inputControls.Disable();
		}

		private void RestartInputHandler(InputAction.CallbackContext ctx)
		{
			_gameRestart.RestartGame();
		}

		private void MoveInputHandler()
		{
			var horizontalMove = _inputControls.Default.Horizontal.ReadValue<float>();
			_playerMovement.MovePlayer(new Vector2(horizontalMove, 0));
		}

		private void ShootInputHandler(InputAction.CallbackContext ctx)
		{
			OnPlayerShoot?.Invoke();
		}
	}
}