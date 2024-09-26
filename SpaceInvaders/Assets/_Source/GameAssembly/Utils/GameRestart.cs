using UnityEngine.SceneManagement;

namespace Utils
{
	public class GameRestart
	{
		public void RestartGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}