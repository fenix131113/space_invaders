using ShootingSystem;
using UnityEngine;

namespace PlayerSystem
{
	public class PlayerShooter : AShooter
	{
		[SerializeField] private Transform shootPoint;
		
		private GameObject _bullet;

		private void Awake()
		{
			Init();
		}

		private void Init()
		{
			_bullet = Instantiate(bulletPrefab);
			_bullet.SetActive(false);
		}

		public override void Shoot()
		{
			if(_bullet.activeSelf)
				return;

			_bullet.transform.position = shootPoint.position;
			_bullet.SetActive(true);
		}
	}
}