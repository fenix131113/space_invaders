﻿using ShootingSystem;
using UnityEngine;

namespace PlayerSystem
{
	public class PlayerBullet : ABullet
	{
		[SerializeField] private float speed;

		private void Update()
		{
			transform.position += transform.up * (speed * Time.deltaTime);

			if (transform.position.y > Camera.main!.ViewportToWorldPoint(new Vector2(0, 1)).y)
				gameObject.SetActive(false);
		}

		protected override void OnTargetEntered(Collider2D target)
		{
			if (target.TryGetComponent(out IHealth health))
				health.DecreaseHealth();
			else
				Debug.LogWarning($"Can't find IHealth component. Check {target.name} object!");

			gameObject.SetActive(false);
		}
	}
}