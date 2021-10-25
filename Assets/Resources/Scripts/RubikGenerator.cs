using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cái này khá quan trọng, ban đầu ta chỉ có một empty object để đánh dấu vị trí của rubik thôi
// sau đó khi runtime ta mới dùng code để thêm các khối (cube) nhỏ bên trong rubik.
public class RubikGenerator : MonoBehaviour
{
    Vector3 distanceToSurface;
    GameObject tinyCubePrefab;

    void Awake()
    {
        distanceToSurface = new Vector3(MainGame.rubikWidth / 2, MainGame.rubikHeight / 2, MainGame.rubikDepth / 2);
        tinyCubePrefab = Resources.Load("Prefabs/TinyCube") as GameObject;
        GenerateRubikCubes();
    }

    void GenerateRubikCubes() 
    {
        for (int x = 0; x < MainGame.rubikWidth; x++)
        {
            for (int y = 0; y < MainGame.rubikHeight; y++)
            {
                for (int z = 0; z < MainGame.rubikDepth; z++)
                {
                    GameObject tinyCube = Instantiate(
                        original: tinyCubePrefab,
                        parent: transform,
                        instantiateInWorldSpace: false
                    ) as GameObject;

                    tinyCube.name = "TinyCube";
                    // relative to center of rubik
                    tinyCube.transform.localPosition = new Vector3(x, y, z) - distanceToSurface;
                    tinyCube.GetComponent<Outline>().enabled = false;
                    
                    TinyCube tinyCubeComponent = tinyCube.GetComponent<TinyCube>();
                    
                    if (x == 0) {
                        tinyCubeComponent.AddSideLayer(Vector3.left, Vector3.up, Color.red);
                    } else if (x == MainGame.rubikWidth-1) {
                        Color orange = new Color(1, 0.34f, 0, 1);
                        tinyCubeComponent.AddSideLayer(Vector3.right, Vector3.down, orange);
                    }
                    if (y == 0) {
                        tinyCubeComponent.AddSideLayer(Vector3.down, Vector3.left, Color.blue);
                    } else if (y == MainGame.rubikHeight-1) {
                        tinyCubeComponent.AddSideLayer(Vector3.up, Vector3.right, Color.green);
                    }
                    if (z == 0) {
                        tinyCubeComponent.AddSideLayer(Vector3.back, Vector3.zero, Color.white);
                    } else if (z == MainGame.rubikDepth-1) {
                        tinyCubeComponent.AddSideLayer(Vector3.forward, new Vector3(0, 2, 0), Color.yellow);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
