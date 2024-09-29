using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsOnGround : MonoBehaviour
{
    [HideInInspector] public bool isHeldByController = false; // Dieser Wert wird true, wenn der Teller gehalten wird
    public bool isOnGround = false;
    
// Trigger oder Collision wird ausgelöst, wenn der Teller den Boden berührt
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Stelle sicher, dass der Boden das Tag "Ground" hat
        {
            isOnGround = true; // Teller berührt den Boden
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false; // Teller verlässt den Boden
        }
    }

    // Methode um zu überprüfen ob der Teller auf dem Boden steht und nicht gehalten wird
    public bool IsPlateOnGroundAndNotHeld()
    {
        return isOnGround && !isHeldByController;
    }
}
