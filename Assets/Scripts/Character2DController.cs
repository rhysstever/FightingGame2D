using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    public PlayerNum playerNum;
    public float movementSpeed = 1;
    public float jumpForce = 1;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxis(
            CharacterManager.instance.GetPlayerSpecificInput(
                InputType.Horizontal, playerNum));
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;

        if(Input.GetAxis(
            CharacterManager.instance.GetPlayerSpecificInput(InputType.Vertical, playerNum)) > 0.0f
            && Mathf.Abs(rb.velocity.y) < 0.001f)
		{
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}
    }
}
