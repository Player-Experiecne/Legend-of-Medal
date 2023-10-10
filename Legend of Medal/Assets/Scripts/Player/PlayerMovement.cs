using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Using GetAxisRaw for direct input
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical).normalized;

        // Rotate the movement direction by 45 degrees around the y-axis
        movement = Quaternion.Euler(0, 45, 0) * movement;

        movement = movement * speed * Time.deltaTime;

        transform.Translate(movement);
    }
}
