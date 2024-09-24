using UnityEngine;

namespace ShootingSystem
{
	public abstract class AShooter : MonoBehaviour
	{
		[SerializeField] protected GameObject bulletPrefab;
		
		public abstract void Shoot();
	}
}