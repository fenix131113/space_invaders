using UnityEngine;

namespace EnemySystem
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField] private ParticleSystem destroyingParticles;
		[SerializeField] private GameObject ammoPrefab;

		public void Die()
		{
			destroyingParticles.Play();
			
			gameObject.SetActive(false);
		}

		public void Shoot()
		{
			
		}
	}
}