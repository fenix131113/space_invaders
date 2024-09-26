using HealthSystem;
using UnityEngine;

namespace ShootingSystem.Enemy
{
	public class EnemyBullet : ABullet
	{
		[SerializeField] private float speed;
		
		private void Update()
		{
			transform.position -= transform.up * (speed * Time.deltaTime);

			if (transform.position.y < Camera.main!.ViewportToWorldPoint(new Vector2(0, 0)).y)
				gameObject.SetActive(false);
		}
		
		protected override void OnTargetEntered(Collider2D target)
		{
			if(target.TryGetComponent(out IHealth health))
				health.DecreaseHealth();
#if UNITY_EDITOR
			else
				Debug.LogWarning($"Can't find IHealth component. Check \"{target.name}\" object!");		
#endif
			
			gameObject.SetActive(false);
		}
	}
}