using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkGameObject : MonoBehaviour
{
    public GameObject targetBlinkObject;
    public float blinkRepeatTime = 0.2f;
    // public float blinkInitialPause = 0.01f;
    private Vector3 initialScale;
    private Vector3 initialPosition;


    void Start()
    {
        initialScale = targetBlinkObject.transform.localScale;
        initialPosition = targetBlinkObject.transform.position;
    }
    public void CallStartBlinkGameObject()
    {
        Debug.Log("Blink!!");
        InvokeRepeating("StartBlinkGameObject", 0 , blinkRepeatTime);  
        // targetBlinkObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); 
    }

    void StartBlinkGameObject()
    {
        if (targetBlinkObject.transform.localScale == new Vector3(initialScale.x, initialScale.y, initialScale.z))
        {
            Debug.Log("Blink ON");
            targetBlinkObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); 
            targetBlinkObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z); 
        }
        else 
        { 
            targetBlinkObject.transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z); 
            Debug.Log("Blink OFF");
        }
    }
    public void StopBlinkGameObject()
    {
        Debug.Log("Stop Blink");
        CancelInvoke();
        targetBlinkObject.transform.localScale = new Vector3(initialScale.x, 1.5f, 1.5f); 
        targetBlinkObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z); 
    }
}
