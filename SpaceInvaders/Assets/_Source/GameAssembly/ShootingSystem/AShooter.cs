using UnityEngine;

namespace ShootingSystem
{
	public abstract class AShooter : MonoBehaviour
	{
		[SerializeField] protected ABullet bulletPrefab;
		
		public abstract void Shoot();
	}
}