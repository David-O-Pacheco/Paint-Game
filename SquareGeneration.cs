using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGeneration : MonoBehaviour
{

    //this script generates a certain amount of pixels onto the top face of an object


    //defining pixel size
    public Vector3 pixelSize = new Vector3(0.01f, 1, 0.01f);

    //store all the positions inside a vector array
    public Vector3[] pixelPositions;

    //first and current position used to move and instantiate pixels in an orderly way
    public Vector3 firstPosition;
    public Vector3 currentPosition;

    int o;

    void Start()
    {
        //defining first position on flat surface of object
        firstPosition = new Vector3((gameObject.transform.position.x + (gameObject.transform.localScale.x / 2)) - ((test.transform.localScale.x / 2) * 10), 1.501f, (gameObject.transform.position.z - (gameObject.transform.localScale.z / 2)) + ((test.transform.localScale.z / 2) * 10));
        currentPosition = firstPosition;
        o = 0;

        //limit the amount of pixels generated to the sum of max pixels needed on flat surface
        pixelPositions = new Vector3[4500];
    }

    void Update()
    {

        //loop through 2D positions (X, Y) and create grid by instantiating pixels within the boundaries of the object

        if (o < pixelPositions.Length)
        {
            if (currentPosition.z < transform.localScale.z / 2)
            {
                if (currentPosition.x < transform.localScale.x / 2)
                {
                    pixelPositions[o] = currentPosition;

                    GameObject instObj = Instantiate(test, pixelPositions[o], Quaternion.identity);
                    instObj.transform.parent = gameObject.transform;
                    instObj.gameObject.GetComponent<MeshRenderer>().enabled = false;

                    currentPosition.z = pixelPositions[o].z + (test.transform.localScale.z * 10);
                    if (currentPosition.x != (transform.localScale.x / 2) + (test.transform.localScale.z * 10))
                        o++;
                }
            }
            else
            {
                currentPosition.x = pixelPositions[o - 50].x - (test.transform.localScale.x * 10);
                currentPosition.z = firstPosition.z;
            }
        }
    }
}
