using HealthSystem;
using UnityEngine;

namespace ShootingSystem.Player
{
	public class PlayerBullet : ABullet
	{
		[SerializeField] private float speed;

		private bool _canHit = true;

		private void Update()
		{
			transform.position += transform.up * (speed * Time.deltaTime);

			if (transform.position.y > Camera.main!.ViewportToWorldPoint(new Vector2(0, 1)).y)
				gameObject.SetActive(false);
		}

		public void ActivateBullet()
		{
			gameObject.SetActive(true);
			_canHit = true;
		}

		protected override void OnTargetEntered(Collider2D target)
		{
			if (!_canHit)
				return;
			
			_canHit = false;
			
			if (target.TryGetComponent(out IHealth health))
				health.DecreaseHealth();
			else
				Debug.LogWarning($"Can't find IHealth component. Check {target.name} object!");

			gameObject.SetActive(false);
		}
	}
}