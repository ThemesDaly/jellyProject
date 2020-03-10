using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysPlayer : MonoBehaviour
{
    //Detected any block to the crash game
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Block")
        {
            GameManager.Instance.CrashGame();
        }
    }

    //Detected trigger to add score
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Score")
        {
            GameManager.Instance.AddScore();
        }
    }
}
