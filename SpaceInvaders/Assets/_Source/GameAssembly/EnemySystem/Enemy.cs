using ShootingSystem;
using UnityEngine;

namespace EnemySystem
{
	public class Enemy : MonoBehaviour, IHealth
	{
		[field: SerializeField] public Transform ShootPoint { get; private set; }
		[field: SerializeField] public GameObject EnemyGFX { get; private set; }

		[SerializeField] private Collider2D currentCollider;
		[SerializeField] private ParticleSystem destroyingParticles;
		[SerializeField] private int maxHealth;

		private int _health;

		public void Start()
		{
			_health = maxHealth;
		}

		private void Die()
		{
			destroyingParticles.Play();

			currentCollider.enabled = false;
			EnemyGFX.SetActive(false);
		}

		public void DecreaseHealth(int amount = 1)
		{
			_health = Mathf.Clamp(_health - amount, 0, maxHealth);

			if (_health == 0)
				Die();
		}
	}
}