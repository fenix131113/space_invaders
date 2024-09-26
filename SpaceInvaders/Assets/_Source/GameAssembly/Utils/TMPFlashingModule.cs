using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utils
{
	[RequireComponent(typeof(TMP_Text))]
	public class TMPFlashingModule : MonoBehaviour
	{
		[field: SerializeField] public string Text { get; private set; }
		[field: SerializeField] public List<string> SpriteAssetTags { get; private set; } = new();

		[field: SerializeField] private bool useSpriteAssets;
		[field: SerializeField] private bool useTargetText;
		[field: SerializeField] private bool flashingOnAwake;
		[field: SerializeField] private float awakeFlashingTime;

		public bool IsFlashing { get; private set; }

		private TMP_Text _target;

		private void Awake()
		{
			_target = GetComponent<TMP_Text>();
		}

		private void Start()
		{
			if (useTargetText)
				Text = _target.text;

			if (flashingOnAwake)
				Flash(awakeFlashingTime);
		}

		public void Flash(float duration)
		{
			if (IsFlashing)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Flashing is using right now!");
#endif
				return;
			}

			IsFlashing = true;
			StartCoroutine(FlashCoroutine(duration));
		}

		public void RefreshText()
		{
			StartCoroutine(FlashCoroutine(0));
		}
		
		private IEnumerator FlashCoroutine(float duration)
		{
			_target.text = "";

			var oneLetterTime = duration / (Text.Length + SpriteAssetTags.Count);

			foreach (var letter in Text.ToCharArray())
			{
				yield return new WaitForSeconds(oneLetterTime);

				_target.text += letter;
			}

			if (useSpriteAssets)
				foreach (var spriteAssetTag in SpriteAssetTags)
				{
					yield return new WaitForSeconds(oneLetterTime);

					_target.text += spriteAssetTag;
				}

			IsFlashing = false;
		}
	}
}