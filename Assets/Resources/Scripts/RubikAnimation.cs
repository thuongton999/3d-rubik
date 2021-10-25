using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// đây là class (component) để làm cái rubik quay quay cho vui
public class RubikAnimation : MonoBehaviour
{
    public float spinningSpeed = 20f;
    // public Vector3 colorFlashing = new Vector3(14, 14, 14);
    // public float flashSpeed = 14f;
    Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!MainGame.isGameStarted) {
            Spinning(spinningSpeed * Time.deltaTime);
        } else {
            LockDefaultRotation(spinningSpeed * 6 * Time.deltaTime);
        }
    }
    void Spinning(float anglesPerFrame) 
    {
        transform.Rotate(anglesPerFrame, anglesPerFrame, 0);
    }
    void LockDefaultRotation(float anglesPerFrame)
    {  
        if (transform.rotation == defaultRotation)
            MainGame.lockedRotation = true;
        if (!MainGame.lockedRotation)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, defaultRotation, anglesPerFrame);
    }
}
