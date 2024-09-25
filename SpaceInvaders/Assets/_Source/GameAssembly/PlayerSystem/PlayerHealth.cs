using ShootingSystem;
using UnityEngine;

namespace PlayerSystem
{
	public class PlayerHealth : MonoBehaviour, IHealth
	{
		private int _health = 3;
		
		public void DecreaseHealth(int amount = 1)
		{
			_health = Mathf.Clamp(_health - amount, 0, 3);
			
			if(_health == 0)
				Die();
		}

		private void Die()
		{
			Debug.Log("Dead");
		}
	}
}