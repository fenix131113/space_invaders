using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
	public class GameRestart : MonoBehaviour
	{
		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.R))
				RestartGame();
		}

		private void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}