using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkGameObject : MonoBehaviour
{
    public GameObject targetBlinkObject;
    public float blinkRepeatTime = 0.5f;
    // public float blinkInitialPause = 0.01f;
    private Vector3 initialScale;


    void Start()
    {
        initialScale = targetBlinkObject.transform.localScale;
    }
    public void CallStartBlinkGameObject()
    {
        Debug.Log("Blink!!");
        InvokeRepeating("StartBlinkGameObject", 0 , blinkRepeatTime);   
    }

    void StartBlinkGameObject()
    {
        if (targetBlinkObject.transform.localScale == new Vector3(initialScale.x, initialScale.y, initialScale.z))
        {
            Debug.Log("Blink ON");
            targetBlinkObject.transform.localScale = new Vector3(0, 0, 0);
        }
        else { targetBlinkObject.transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z); 
        Debug.Log("Blink OFF");
        }
    }
    public void StopBlinkGameObject()
    {
                        Debug.Log("Stip Blink");
        CancelInvoke();
        targetBlinkObject.transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z); 
    }
}
