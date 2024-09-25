using PlayerSystem;
using UnityEngine;
using Zenject;

namespace ShootingSystem.Player
{
	public class PlayerShooter : AShooter
	{
		[SerializeField] private Transform shootPoint;
		
		private PlayerBullet _bullet;
		private PlayerInputHandler _inputHandler;

		[Inject]
		private void Construct(PlayerInputHandler playerInputHandler)
		{
			_inputHandler = playerInputHandler;
			
			Init();
		}

		private void Awake() => Bind();

		private void Bind()
		{
			_inputHandler.OnPlayerShoot += Shoot;
		}

		private void Expose()
		{
			_inputHandler.OnPlayerShoot -= Shoot;
		}
		
		private void Init()
		{
			_bullet = Instantiate(bulletPrefab as PlayerBullet);
			_bullet.gameObject.SetActive(false);
		}

		public override void Shoot()
		{
			if(_bullet.gameObject.activeSelf)
				return;

			_bullet.transform.position = shootPoint.position;
			_bullet.ActivateBullet();
		}

		private void OnDestroy() => Expose();

		private void OnApplicationQuit() => Expose();
	}
}