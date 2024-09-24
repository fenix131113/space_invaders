using ShootingSystem;
using UnityEngine;

namespace EnemySystem
{
	public class Enemy : MonoBehaviour, IHealth
	{
		//[field: SerializeField] public En
		
		[SerializeField] private ParticleSystem destroyingParticles;
		[SerializeField] private int maxHealth;
		
		private int _health;
		private GameObject _bullet;

		public void Init(GameObject bullet)
		{
			if(_bullet)
				return;
			
			_bullet = bullet;
		}
		
		private void OnValidate()
		{
			_health = maxHealth;
		}

		private void Die()
		{
			destroyingParticles.Play();
			
			gameObject.SetActive(false);
		}

		public void DecreaseHealth(int amount = 1)
		{
			_health = Mathf.Clamp(_health - amount, 0, maxHealth);
			
			if(_health == 0)
				Die();
		}
	}
}