using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb; //breaking convention in naming it "rb" for rigidbody
    
    [SerializeField] float rocketThrust = 1000f;
    [SerializeField] float rotateSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.AddRelativeForce(rocketThrust * Time.deltaTime * Vector3.up);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotateSpeed);
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotateSpeed);
        }
    }

    //having "private" or nothing is the same. Thus, this is a private method
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezes rotation so we can manually rotate
        transform.Rotate(rotationThisFrame * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false; //unfreeze rotation so physics can take over
    }
}
