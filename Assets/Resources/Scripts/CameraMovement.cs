using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cái này dùng để zoom camera lại gần cái rubik khi mà ta start game
public class CameraMovement : MonoBehaviour
{
    public Vector3 defaultCameraPosition = new Vector3(1f, 2f, -20f);
    public Vector3 rubikFocusPosition = new Vector3(1f, 2f, -15f);
    public float cameraSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float unitsPerFrame = cameraSpeed * Time.deltaTime;
        if (MainGame.isGameStarted) {
            transform.position = Vector3.MoveTowards(transform.position, rubikFocusPosition, unitsPerFrame);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, defaultCameraPosition, unitsPerFrame);
        }
    }
}
