using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 direction;
    float speed = 5f;

    CharacterController CC;

    GameObject NPCText;

    bool inNPCRange = false;

    // Start is called before the first frame update
    void Start()
    {
        // This grabs the Character Controller off our game object.
        CC = GetComponent<CharacterController>();

        // This will search the hierarchy for a game object with the name NPCText
        // We are boldly assuming this object has been created and named correctly.
        NPCText = GameObject.Find("NPCText");
        // We don't want to see our text box on startup. This will ensure it is hidden immediately.
        NPCText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Our direction vector is assigned according to raw input values, and normalized.
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 
            Input.GetAxisRaw("Vertical")).normalized;

        // Simple move just takes a direction and speed.
        // If using CC.Move(), be sure to also multiply by Time.deltaTime to normalize the framerate.
        CC.SimpleMove(direction * speed);

        // If we are in range of an NPC and we press space bar, interact with them.
        if (inNPCRange && Input.GetButtonDown("Jump"))
        {
            Interact();
        }
    }

    // Called when a user presses space next to an NPC (or inside an NPC trigger volume to be exact).
    void Interact()
    {
        // Show our NPC text.
        NPCText.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If we are inside a collider with the NPC tag then flag that we are in range of an NPC.
        // Note: Tag must be created and assigned in the inspector in the editor.
        if (other.tag == "NPC")
        {
            inNPCRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Whenever we leave a trigger (any trigger really), the NPC text can go away:
        inNPCRange = false;
        NPCText.SetActive(false);
    }
}
