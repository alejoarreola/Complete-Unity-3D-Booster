using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayAmount = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        DebugKeyActions();
    }

    void DebugKeyActions() //adds debug functions to keys
    {
       if (Input.GetKeyDown(KeyCode.L)) //loads next level
        {
            LoadLevel();
        }

        else if (Input.GetKeyDown(KeyCode.R)) //restarts current level
        {
            ReloadLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision on and off
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
;               break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); //stops all audio
        audioSource.PlayOneShot(success); //plays success sound on finish
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadLevel", delayAmount);
    }

    void StartCrashSequence()
    {
        //invoke is not the best solution. Research "Coroutine" for better alternative
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash); //plays crash sound on crash
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayAmount);
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