using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's a friendly!");
;               break;

            case "Finish":
                LoadLevel();
                break;

            case "Fuel":
                Debug.Log("Got some fuel!");
                break;

            default:
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (currentSceneIndex < 2)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        else
        {
            SceneManager.LoadScene(0);
        }
    }
}