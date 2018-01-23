using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 MovementVector;
    Animator anim;
    Rigidbody rb;

    float CamRayLength = 100f;
    int FloorMask;

    void Awake()
    {
        FloorMask = LayerMask.GetMask("Floor");
        anim = transform.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float H = Input.GetAxisRaw("Horizontal");
        float V = Input.GetAxisRaw("Vertical");
    }

    void Move(float h, float v)
    {
        MovementVector.Set(h, 0, v);
        MovementVector = MovementVector.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + MovementVector);
    }

    void Turn()
    {
        Ray TurnRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit TurnTo;
        if (Physics.Raycast(TurnRay,out TurnTo, CamRayLength, FloorMask))
        {
            Vector3 P2M = TurnTo.point - transform.position;
            P2M.y = 0;

            rb.MoveRotation(Quaternion.LookRotation(P2M));
        }
    }
}
