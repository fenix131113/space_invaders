using System;
using HealthSystem;
using UnityEngine;

namespace PlayerSystem
{
	public class PlayerHealth : MonoBehaviour, IHealth
	{
		public int Health { get; private set; } = 3;

		public event Action OnHealthChanged;

		public void DecreaseHealth(int amount = 1)
		{
			Health = Mathf.Clamp(Health - amount, 0, 3);

			OnHealthChanged?.Invoke();
			
			if (Health == 0)
				Die();
		}

		private void Die()
		{
			Debug.Log("Dead");
		}
	}
}