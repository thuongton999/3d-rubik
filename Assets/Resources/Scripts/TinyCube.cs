using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cái class này là cái khối nhỏ (tiny cube) trong rubik nà :3
public class TinyCube : MonoBehaviour
{
    public float layerDepth;
    public RubikController rubikController;

    private Dictionary<Color, string> colorsName = new Dictionary<Color, string>();

    GameObject layerPrefab;
    float cubeSurfacePosition;
    float cubeSurfaceRotation;

    void Awake()
    {
        layerPrefab = Resources.Load("Prefabs/TinyCubeLayer") as GameObject;
        layerDepth = 0.0001f;
        cubeSurfacePosition = 0.5f + layerDepth;
        cubeSurfaceRotation = 90f;

        this.colorsName[Color.red] = "Red";
        this.colorsName[Color.blue] = "Blue";
        this.colorsName[Color.green] = "Green";
        this.colorsName[Color.white] = "White";
        this.colorsName[new Color(1, 0.34f, 0, 1)] = "Orange";
        this.colorsName[Color.yellow] = "Yellow";
    }

    void Start()
    {
        rubikController = GameObject.Find("Rubik").GetComponent<RubikController>();
    }
    void OnMouseDown()
    {
        if (MainGame.isGameStarted)
        {
            MainGame.cubeSelected = transform.localPosition;
        }
    }
    void OnMouseOver()
    {
        if (MainGame.isGameStarted)
        {
            rubikController.HighlightRowsAndColumns(_index: transform.localPosition, _highlight: true);
        }
    }
    void OnMouseExit()
    {
        if (MainGame.isGameStarted)
        {
            rubikController.HighlightRowsAndColumns(_index: transform.localPosition, _highlight: false);
        }
    }

    public void AddSideLayer(Vector3 _direction, Vector3 _rotation, Color _color)
    {
        GameObject layer = Instantiate(
            original: layerPrefab, 
            position: Vector3.zero, 
            rotation: Quaternion.Euler(_rotation * cubeSurfaceRotation),
            parent: transform
        ) as GameObject;
        layer.transform.localPosition = _direction * cubeSurfacePosition;
        layer.GetComponent<Renderer>().material.color = _color;
        layer.name = this.colorsName[_color];
    }
}