using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 20f;
    private int jumps = 1;
    private float jump_force = 500;

    public int player_points = 0;

    public Dictionary<string, bool> quests;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        quests = new Dictionary<string, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical")*speed;
        float horizontal = Input.GetAxis("Horizontal")*0.5f*speed;
        if (vertical < 0) {
            vertical *= 0.5f;//move backwards at half speed
        }
        Vector3 movement = transform.forward * vertical + transform.right * horizontal;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (rb.velocity.x != 0 || rb.velocity.z != 0)
        {
            GetComponentInChildren<Animator>().SetBool("Running", true);
        }
        else {
            GetComponentInChildren<Animator>().SetBool("Running", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumps >0) {
            
            rb.AddForce(transform.up * jump_force);
            jumps--;
        }


        GameObject.Find("pointsText").GetComponent<Text>().text = player_points.ToString();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.other.tag == "Ground")
        {
            jumps = 1;
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        if (quests.ContainsKey(other.tag)) {
            quests[other.tag] = true;
            Destroy(other.gameObject);
        }
    }

    public void increase_points(int num_points) {
        player_points+=num_points;

    }


    
}

