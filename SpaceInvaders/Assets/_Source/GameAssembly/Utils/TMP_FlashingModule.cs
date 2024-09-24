using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Utils
{
	[RequireComponent(typeof(TMP_Text))]
	public class TMPFlashingModule : MonoBehaviour
	{
		[field: SerializeField] public string Text { get; private set; }

		[field: SerializeField] private bool useTargetText;
		[field: SerializeField] private bool flashingOnAwake;
		[field: SerializeField] private float awakeFlashingTime;
		
		public bool IsFlashing { get; private set; }
		
		private TMP_Text _target;

		private void OnValidate()
		{
			_target = GetComponent<TMP_Text>();
		}

		private void Awake()
		{
			if(useTargetText)
				Text = _target.text;
			
			if(flashingOnAwake)
				Flash(awakeFlashingTime);
		}

		public void Flash(float duration)
		{
			if (IsFlashing)
			{
				Debug.LogWarning("Flashing is using right now!");
				return;
			}

			IsFlashing = true;
			StartCoroutine(FlashCoroutine(duration));
		}

		private IEnumerator FlashCoroutine(float duration)
		{
			_target.text = "";
			
			var oneLetterTime = duration / Text.Length;

			foreach (var letter in Text.ToCharArray())
			{
				yield return new WaitForSeconds(oneLetterTime);
				_target.text += letter;
			}
			
			IsFlashing = false;
		}
	}
}