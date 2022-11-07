using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audiosource;
 
    [SerializeField] float rocketThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
       {
            StartThrusting();    
       }
       else 
       {
            StopThrusting();
       }
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
       {
            RotateLeft();
       }
       else if (Input.GetKey(KeyCode.D))
       {
            RotateRight();
       }
       else
       {
            StopRotating();
       }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
            
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        audiosource.Stop();
        mainEngineParticles.Stop();
    }


    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    public void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
