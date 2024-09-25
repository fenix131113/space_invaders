using System;
using HealthSystem;
using UnityEngine;

namespace PlayerSystem
{
	public class PlayerHealth : MonoBehaviour, IHealth
	{
		public int Health { get; private set; } = 3;
		public bool IsDead { get; private set; }

		public event Action OnHealthChanged;
		public event Action OnDead;

		public void DecreaseHealth(int amount = 1)
		{
			if(Health == 0)
				return;
			
			Health = Mathf.Clamp(Health - amount, 0, 3);

			OnHealthChanged?.Invoke();

			if (Health == 0)
				Die();
		}

		private void Die()
		{
			if (IsDead)
				return;
			
			OnDead?.Invoke();
			IsDead = true;
		}
	}
}