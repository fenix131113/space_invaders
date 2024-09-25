using PlayerSystem.Data;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        private PlayerSettings _playerSettings;
        private PlayerHealth _playerHealth;

        [Inject]
        private void Construct(PlayerSettings playerSettings, PlayerHealth playerHealth)
        {
            _playerSettings = playerSettings;
            _playerHealth = playerHealth;
        }
        
        public void MovePlayer(Vector2 movement)
        {
            if(_playerHealth.IsDead)
                return;
            
            playerTransform.position += (Vector3)movement * (_playerSettings.Speed * Time.deltaTime);

            var minX = Camera.main!.ScreenToWorldPoint(new Vector2(0, 0)).x;
            var maxX = Camera.main!.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            var offset = playerTransform.localScale.x / 2;
            
            playerTransform.position = new Vector2(Mathf.Clamp(playerTransform.position.x, minX + offset, maxX - offset),
                playerTransform.position.y);
        }
    }
}
