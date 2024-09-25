using UnityEngine;
using Utils;
using Zenject;

namespace PlayerSystem.View
{
	public class PlayerHealthView : MonoBehaviour
	{
		[SerializeField] private TMPFlashingModule module;

		private PlayerHealth _playerHealth;

		[Inject]
		private void Construct(PlayerHealth playerHealth)
		{
			_playerHealth = playerHealth;
		}

		private void Awake()
		{
			Bind();
		}

		private void RefreshVisual()
		{
			if (_playerHealth.Health >= module.SpriteAssetTags.Count) return;

			while (module.SpriteAssetTags.Count > _playerHealth.Health)
				module.SpriteAssetTags.Remove(module.SpriteAssetTags[^1]);
			
			module.RefreshText();
		}

		private void Bind()
		{
			_playerHealth.OnHealthChanged += RefreshVisual;
		}

		private void Expose()
		{
			_playerHealth.OnHealthChanged -= RefreshVisual;
		}

		private void OnDestroy() => Expose();

		private void OnApplicationQuit() => Expose();
	}
}