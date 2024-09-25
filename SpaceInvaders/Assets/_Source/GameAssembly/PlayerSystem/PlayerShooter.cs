using ShootingSystem;
using UnityEngine;

namespace PlayerSystem
{
	public class PlayerShooter : AShooter
	{
		[SerializeField] private Transform shootPoint;
		
		private PlayerBullet _bullet;

		private void Awake()
		{
			Init();
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
	}
}