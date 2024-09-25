using System;
using HealthSystem;
using PlayerSystem;
using UnityEngine;
using Utils;

namespace EnemySystem
{
	public class Enemy : MonoBehaviour, IHealth
	{
		[field: SerializeField] public Transform ShootPoint { get; private set; }
		[field: SerializeField] public GameObject EnemyGFX { get; private set; }

		[SerializeField] private LayerMask endGameLayer;
		[SerializeField] private int scoreByDeath;
		[SerializeField] private Collider2D currentCollider;
		[SerializeField] private ParticleSystem destroyingParticles;
		[SerializeField] private int maxHealth;
		
		public event Action OnDeath;
		
		public int Health { get; private set; }

		private PlayerScore _playerScore;
		private PlayerHealth _playerHealth;

		public void Init(PlayerScore playerScore, PlayerHealth playerHealth)
		{
			_playerScore = playerScore;
			_playerHealth = playerHealth;
			
			Health = maxHealth;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (LayerService.CheckLayersEquality(other.gameObject.layer, endGameLayer))
				_playerHealth.DecreaseHealth(3);
		}

		private void Die()
		{
			OnDeath?.Invoke();
			destroyingParticles.Play();

			_playerScore.AddScore((uint)scoreByDeath);
			
			currentCollider.enabled = false;
			OnDeath = null;
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