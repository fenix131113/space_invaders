using UnityEngine;
using Zenject;

namespace PlayerSystem
{
	public class PlayerInputHandler : ITickable
	{
		private PlayerMovement _playerMovement;

		[Inject]
		private void Construct(PlayerMovement playerMovement)
		{
			_playerMovement = playerMovement;
		}

		public void Tick()
		{
			MoveInputHandler();
		}

		private void MoveInputHandler()
		{
			var horizontalMove = Input.GetAxisRaw("Horizontal");
			_playerMovement.MovePlayer(new Vector2(horizontalMove, 0));
		}
	}
}