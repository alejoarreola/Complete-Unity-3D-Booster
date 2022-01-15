using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayAmount = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    bool isTransitioning = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
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
        audioSource.PlayOneShot(crash); //plays crash sound on crash
        Invoke("ReloadLevel", delayAmount); 
    }

    void StartFinishSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(success); //plays success sound on finish
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