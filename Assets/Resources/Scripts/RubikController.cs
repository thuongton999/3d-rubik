using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// phần controller này chưa xong, chưa giải thích xD, ahyhy
public class RubikController : MonoBehaviour
{
    GameObject[] tinyCubes;
    GameObject cubesSelected;
    Quaternion rotateDirection;

    bool isRotating = false;
    bool firstRotate = true;
    bool isDragging = false;

    Vector3 dragOffset;
    float dragSensitivity = 0.5f;

    public Color rowSelectHighlight;
    public Color columnSelectHighlight;
    public Color depthSelectHighlight;
    const float RotateAngle = 90f;
    const float RotateSpeed = 180f;

    const string RotateUp = "w";
    const string RotateDown = "s";
    const string RotateLeft = "a";
    const string RotateRight = "d";
    const string RotateLeftAroundZ = "e";
    const string RotateRightAroundZ = "r";

    const int Left = 1;
    const int Right = -1;

    // Start is called before the first frame update
    void Start()
    {
        cubesSelected = GameObject.Find("CubesSelected");
        tinyCubes = GameObject.FindGameObjectsWithTag("RubikTinyCube");

        rowSelectHighlight = Color.red;
        columnSelectHighlight = Color.blue;
        depthSelectHighlight = Color.green;
    }

    public void HighlightRowsAndColumns(Vector3 _index, bool _highlight)
    {
        foreach (GameObject tinyCube in tinyCubes)
        {
            Vector3 childIndex = tinyCube.transform.localPosition;
            Outline childCubeOutline = tinyCube.GetComponent<Outline>();
            if (MainGame.IsAlmostEqual(childIndex.x, _index.x))
            {
                childCubeOutline.enabled = _highlight;
                childCubeOutline.OutlineColor = rowSelectHighlight;
            } 
            if (MainGame.IsAlmostEqual(childIndex.y, _index.y))
            {
                childCubeOutline.enabled = _highlight;
                childCubeOutline.OutlineColor = columnSelectHighlight;
            } 
            if (MainGame.IsAlmostEqual(childIndex.z, _index.z))
            {
                childCubeOutline.enabled = _highlight;
                childCubeOutline.OutlineColor = depthSelectHighlight;
            }
        }
    }

    Quaternion SetRotation(Vector3 _direction, bool _setRow = false, bool _setColumn = false, bool _setDepth = false)
    {
        bool onSelecting = MainGame.cubeSelected != Vector3.zero;
        if (onSelecting)
        {
            Vector3 selectedCube = MainGame.cubeSelected;
            foreach (GameObject tinyCube in tinyCubes)
            {
                Vector3 tinyCubeIndex = tinyCube.transform.localPosition;
                if (MainGame.IsAlmostEqual(tinyCubeIndex.y, selectedCube.y) && _setRow)
                {
                    tinyCube.transform.parent = cubesSelected.transform;
                } 
                else if (MainGame.IsAlmostEqual(tinyCubeIndex.x, selectedCube.x) && _setColumn) 
                {
                    tinyCube.transform.parent = cubesSelected.transform;
                } 
                else if (MainGame.IsAlmostEqual(tinyCubeIndex.z, selectedCube.z) && _setDepth)
                {
                    tinyCube.transform.parent = cubesSelected.transform;
                }
            }
        }
        return Quaternion.AngleAxis(RotateAngle, _direction) * cubesSelected.transform.localRotation;
    }

    void AutoDrag()
    {
        if (isDragging && MainGame.lockedRotation)
        {
            float xDistance = (dragOffset.x - Input.mousePosition.x);
            float yDistance = (Input.mousePosition.y - dragOffset.y);
            transform.Rotate(new Vector3(yDistance, xDistance, 0) * dragSensitivity * Time.deltaTime, Space.World);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // mouse input handlers
        if (MainGame.isGameStarted)
        {
            // left mouse
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!isDragging)
                {
                    dragOffset = Input.mousePosition;
                }
                isDragging = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isDragging = false;
            }
            AutoDrag();
        }

        // keyboard input handlers
        string keyboardInput = Input.inputString;
        if (MainGame.isGameStarted && !isRotating || firstRotate)
        {
            switch (keyboardInput)
            {
                case RotateRight: 
                {
                    firstRotate = false;
                    rotateDirection = SetRotation(Vector3.down, _setRow: true);
                    break;
                }
                case RotateLeft:
                {
                    firstRotate = false;
                    rotateDirection = SetRotation(Vector3.up, _setRow: true);
                    break;
                }
                case RotateUp:
                {
                    firstRotate = false;
                    rotateDirection = SetRotation(Vector3.right, _setColumn: true);
                    break;
                }
                case RotateDown:
                {
                    firstRotate = false;
                    rotateDirection = SetRotation(Vector3.left, _setColumn: true);
                    break;
                }
                case RotateLeftAroundZ:
                {
                    firstRotate = false;
                    rotateDirection = SetRotation(Vector3.forward, _setDepth: true);
                    break;
                }
                case RotateRightAroundZ:
                {
                    firstRotate = false;
                    rotateDirection = SetRotation(Vector3.back, _setDepth: true);
                    break;
                }
            }
        }
        isRotating = !(cubesSelected.transform.localRotation == rotateDirection);
        if (!isRotating)
        {
            foreach (GameObject tinyCube in tinyCubes)
            {
                tinyCube.transform.parent = transform;
            }
        }
        cubesSelected.transform.localRotation = Quaternion.RotateTowards(
            cubesSelected.transform.localRotation, 
            rotateDirection, 
            RotateSpeed * Time.deltaTime
        );
    }
}
