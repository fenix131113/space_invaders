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
		private readonly PlayerInput _inputAction;

		public event Action OnPlayerShoot;

		[Inject]
		private PlayerInputHandler(PlayerMovement playerMovement, GameRestart gameRestart, PlayerInput inputAction)
		{
			_playerMovement = playerMovement;
			_gameRestart = gameRestart;
			_inputAction = inputAction;
		}

		~PlayerInputHandler() => Expose();

		public void Tick()
		{
			MoveInputHandler();
		}

		public void Initialize()
		{
			Bind();
		}
		
		private void Bind()
		{
			_inputAction.actions.FindAction("Restart").started += RestartInputHandler;
			_inputAction.actions.FindAction("Shoot").started += ShootInputHandler;
		}

		private void Expose()
		{
			_inputAction.actions.FindAction("Restart").started -= RestartInputHandler;
			_inputAction.actions.FindAction("Shoot").started -= ShootInputHandler;
		}

		private void RestartInputHandler(InputAction.CallbackContext ctx)
		{
			_gameRestart.RestartGame();
		}

		private void MoveInputHandler()
		{
			var horizontalMove = _inputAction.actions.FindAction("Horizontal").ReadValue<float>();
			_playerMovement.MovePlayer(new Vector2(horizontalMove, 0));
		}

		private void ShootInputHandler(InputAction.CallbackContext ctx)
		{
			OnPlayerShoot?.Invoke();
		}
	}
}