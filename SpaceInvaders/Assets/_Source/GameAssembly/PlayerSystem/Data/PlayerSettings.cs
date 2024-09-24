using UnityEngine;

namespace PlayerSystem.Data
{
	[CreateAssetMenu(fileName = "New PlayerSettings", menuName = "ScriptableObjects/New PlayerSettings")]
	public class PlayerSettings : ScriptableObject
	{
		[field: SerializeField] public float Speed { get; private set; }
		[field: SerializeField] public int MaxHealth { get; private set; }
	}
}