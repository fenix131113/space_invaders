using UnityEngine;
using Utils;

namespace ShootingSystem
{
	public abstract class ABullet : MonoBehaviour
	{
		[SerializeField] protected LayerMask interactLayers;

		protected abstract void OnTargetEntered(Collider2D target);

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (LayerService.CheckLayersEquality(other.gameObject.layer, interactLayers))
				OnTargetEntered(other);
		}
	}
}