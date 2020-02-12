using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BrushMovement : MonoBehaviour
{

    //this script handles brush movement for PC (testing) and Mobile (tilt or touchscreen input)

    Vector3 dir = Vector3.zero;

    public Material BlueP, YellowP, RedP, GreenP, GoldP;
    public Material Coin, CoinGrayscale, Carrot, defaultMaterial;

    public bool PaintP, newPixels, startMoving, endGame, pauseGame, ButtonMoveLeft, ButtonMoveRight, changeInput = true;

    public GameObject pixels, brushPaint, gatherPaint, paintAnimation;

    FileManager getFileManager;

    public Image deathImg, screenFade;

    void Start()
    {
        PaintP = false;
        newPixels = true;
        endGame = false;
        pauseGame = false;
        ButtonMoveLeft = false;
        ButtonMoveRight = false;

        //gets a script that handles loading and saving important information such as amount of coins
        getFileManager = GameObject.Find("GameEvents").GetComponent<FileManager>();
        //loads and saves the game at the start due to a software lag spike on its' first usage of the script
        getFileManager.LoadGame();
        getFileManager.SaveGame();

        //caps framerate and causes the mobile screen to never sleep (timeout)
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void FixedUpdate()
    {
        //###### Continuously Move Brush #####//
        if (!endGame && !pauseGame)
        {
            transform.Translate(Vector3.left * 4f * Time.deltaTime);

            GameObject newInst = Instantiate(brushPaint, transform.position, gameObject.transform.rotation);
            newInst.transform.parent = gatherPaint.transform;
            newInst.GetComponent<Renderer>().material.color = new Color(gameObject.GetComponent<Renderer>().material.color.r, gameObject.GetComponent<Renderer>().material.color.g, gameObject.GetComponent<Renderer>().material.color.b, gameObject.GetComponent<Renderer>().material.color.a);

            //main input is keyboard and tilt
            KeyboardMovement();

            //players can change input through the settings to change from Tilt to Touchscreen Input
            if (!changeInput)
            {
                TiltMovement();
            }
            else if (changeInput)
            {
                if (ButtonMoveLeft)
                {
                    dir = new Vector3(0,0,0);

                    transform.Translate(Vector3.left * 0.2f * Time.deltaTime);
                    transform.Rotate(Vector3.up, -60f * Time.deltaTime, Space.World);

                    //###### Player Rotation Cap #####//
                    if (transform.rotation.eulerAngles.y > 270 || transform.rotation.eulerAngles.y < 90)
                        transform.Rotate(Vector3.up, dir.x * 180f, Space.World);

                    if (transform.rotation.eulerAngles.y <= 270 && transform.rotation.eulerAngles.y >= 265)
                        transform.localEulerAngles = new Vector3(0, 271, 0);

                    if (transform.rotation.eulerAngles.y >= 90 && transform.rotation.eulerAngles.y <= 95)
                        transform.localEulerAngles = new Vector3(0, 89, 0);
                    //###### Player Rotation Cap #####//

                }
                else if (ButtonMoveRight)
                {
                    transform.Translate(Vector3.left * 0.2f * Time.deltaTime);
                    transform.Rotate(Vector3.up, 60f * Time.deltaTime, Space.World);

                    //###### Player Rotation Cap #####//
                    if (transform.rotation.eulerAngles.y > 270 || transform.rotation.eulerAngles.y < 90)
                        transform.Rotate(Vector3.up, dir.x * 180f, Space.World);

                    if (transform.rotation.eulerAngles.y <= 270 && transform.rotation.eulerAngles.y >= 265)
                        transform.localEulerAngles = new Vector3(0, 271, 0);

                    if (transform.rotation.eulerAngles.y >= 90 && transform.rotation.eulerAngles.y <= 95)
                        transform.localEulerAngles = new Vector3(0, 89, 0);
                    //###### Player Rotation Cap #####//

                }
            }

        }
        else if (endGame)
        {
            //stops the game after collision with object (processed in other script)
            deathImg.color = Color.Lerp(deathImg.color, new Color(deathImg.color.r, deathImg.color.g, deathImg.color.b, 255), 0.005f * Time.deltaTime);
            screenFade.color = Color.Lerp(screenFade.color, new Color(screenFade.color.r, screenFade.color.g, screenFade.color.b, 255), 0.005f * Time.deltaTime);
        }

    }

    //buttons on screen cause these methods to run, making touchscreen input possible

    public void MoveLeftButtonDown()
    {
        ButtonMoveLeft = true;
    }

    public void MoveLeftButtonUp()
    {
        ButtonMoveLeft = false;
    }

    public void MoveRightButtonDown()
    {
        ButtonMoveRight = true;
    }

    public void MoveRightButtonUp()
    {
        ButtonMoveRight = false;
    }

    void TiltMovement()
    {
        //###### Mobile Tilt Calculation #####//
        dir.x = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;
        //###### Mobile Tilt Calculation #####//


        //###### Player Rotation Cap #####//
        if (transform.rotation.eulerAngles.y > 270 || transform.rotation.eulerAngles.y < 90)
            transform.Rotate(Vector3.up, dir.x * 180f, Space.World);

        if (transform.rotation.eulerAngles.y <= 270 && transform.rotation.eulerAngles.y >= 265)
            transform.localEulerAngles = new Vector3(0, 271, 0);

        if (transform.rotation.eulerAngles.y >= 90 && transform.rotation.eulerAngles.y <= 95)
            transform.localEulerAngles = new Vector3(0, 89, 0);
        //###### Player Rotation Cap #####//
    }

    void KeyboardMovement()
    {
        //###### Computer Controls #####//
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 0.2f * Time.deltaTime);
            transform.Rotate(Vector3.up, -60f * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.left * 0.2f * Time.deltaTime);
            transform.Rotate(Vector3.up, 60f * Time.deltaTime, Space.World);
        }
        //###### Computer Controls #####//



        //###### Player Rotation Cap #####//
        if (transform.rotation.eulerAngles.y > 270 || transform.rotation.eulerAngles.y < 90)
            transform.Rotate(Vector3.up, dir.x * 180f, Space.World);

        if (transform.rotation.eulerAngles.y <= 270 && transform.rotation.eulerAngles.y >= 265)
            transform.localEulerAngles = new Vector3(0, 271, 0);

        if (transform.rotation.eulerAngles.y >= 90 && transform.rotation.eulerAngles.y <= 95)
            transform.localEulerAngles = new Vector3(0, 89, 0);
        //###### Player Rotation Cap #####//
    }
}
