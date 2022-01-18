using UnityEngine;

public class Movement : MonoBehaviour
{
    // general structure guidelines
    // PARAMETERS - for tuning, typically set in the editor

    // CACHE - e.g. references for readability or speed

    // STATE - private instance (member) variables

    [SerializeField] float rocketThrust = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] AudioClip engineThrust;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb; //breaking convention in naming it "rb" for rigidbody
    AudioSource audioSource; //cache reference to Audio Source

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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
        rb.AddRelativeForce(rocketThrust * Time.deltaTime * Vector3.up);

        if (!audioSource.isPlaying) //play audio if it's NOT already playing
        {
            audioSource.PlayOneShot(engineThrust);
        }

        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotateSpeed);

        if (!rightThrustParticles.isPlaying) //plays effect on right side since it is "pushing" a left rotation
        {
            rightThrustParticles.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotateSpeed);

        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    //having "private" or nothing is the same. Thus, this is a private method
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezes rotation so we can manually rotate
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false; //unfreeze rotation so physics can take over
    }
}
