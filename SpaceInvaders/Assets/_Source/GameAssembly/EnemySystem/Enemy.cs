using HealthSystem;
using PlayerSystem;
using UnityEngine;

namespace EnemySystem
{
	public class Enemy : MonoBehaviour, IHealth
	{
		[field: SerializeField] public Transform ShootPoint { get; private set; }
		[field: SerializeField] public GameObject EnemyGFX { get; private set; }

		[SerializeField] private int scoreByDeath;
		[SerializeField] private Collider2D currentCollider;
		[SerializeField] private ParticleSystem destroyingParticles;
		[SerializeField] private int maxHealth;
		
		public int Health { get; private set; }

		private PlayerScore _playerScore;

		public void Init(PlayerScore playerScore)
		{
			_playerScore = playerScore;
			
			Health = maxHealth;
		}

		private void Die()
		{
			destroyingParticles.Play();

			_playerScore.AddScore((uint)scoreByDeath);
			
			currentCollider.enabled = false;
			EnemyGFX.SetActive(false);
		}


		public void DecreaseHealth(int amount = 1)
		{
			Health = Mathf.Clamp(Health - amount, 0, maxHealth);

			if (Health == 0)
				Die();
		}
	}
}