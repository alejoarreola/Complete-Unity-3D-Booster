using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayAmount = 2f;
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's a friendly!");
;               break;

            case "Finish":
                StartFinishSequence();
                break;

            default:
                StartCrashSequence(); 
                break;
        }
    }

    void StartCrashSequence()
    {
        //invoke is not the best solution. Research "Coroutine" for better alternative
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false; //todo: change to play a "fail" sfx
        Invoke("ReloadLevel", delayAmount); 
    }

    void StartFinishSequence()
    {
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false; //todo: change to play a "Victory" sfx
        Invoke("LoadLevel", delayAmount);
    }
    void LoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


}