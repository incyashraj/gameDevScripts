using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audiosource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
        {
            if (isTransitioning || collisionDisable) { return; }

            switch (other.gameObject.tag)
            {
                case "Spawn":
                    Debug.Log("This is a Launching Pad");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                case "Fuel":
                    Debug.Log("FULED UP!!!");
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }

    

    // Update is called once per frame
    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audiosource.Stop();
        audiosource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<playerMovement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audiosource.Stop();
        audiosource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<playerMovement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
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
