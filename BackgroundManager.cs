using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{

    public GameObject brush;
    public Image backgroundImage;
    public Image[] AutumnLeafs;

    Color currentColour;

    float timer;
    int spawns = 0;

    void Start()
    {
        currentColour = brush.GetComponent<Renderer>().material.color;

        //specific code for specific backgrounds (backgrounds are cosmetics in the game)
        if(backgroundImage.sprite.name == "BG#Rainbow")
            backgroundImage.color = brush.GetComponent<Renderer>().material.color;

    }

    void FixedUpdate()
    {

        timer += Time.deltaTime;

        //specific code for specific background (lerps to new background colour each time the paint brush changes paint)
        if (brush.GetComponent<Renderer>().material.color != currentColour && backgroundImage.sprite.name == "BG#Rainbow")
        {
            backgroundImage.color = Color.Lerp(backgroundImage.color, brush.GetComponent<Renderer>().material.color, 2.5f * Time.deltaTime);

            if (backgroundImage.color == brush.GetComponent<Renderer>().material.color)
                currentColour = brush.GetComponent<Renderer>().material.color;
        }

        //specific code for specific background (instantiates leaves which move and relocate using a different script)
        if (backgroundImage.sprite.name == "BG#Autumn")
        {
            if (timer > 1 && spawns < 21)
            {
                Image instObject = Instantiate(AutumnLeafs[Random.Range(0, AutumnLeafs.Length)], new Vector3(Random.Range((gameObject.GetComponent<RectTransform>().rect.width * -1) / 2, gameObject.GetComponent<RectTransform>().rect.width) / 2, Random.Range((gameObject.GetComponent<RectTransform>().rect.height * -1) / 2, gameObject.GetComponent<RectTransform>().rect.height) / 2, 0), Quaternion.AngleAxis(Random.Range(0, 360), Vector3.back));
                instObject.transform.parent = gameObject.transform;
                instObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                instObject.transform.localPosition = new Vector3(Random.Range((gameObject.GetComponent<RectTransform>().rect.width * -1) / 3, gameObject.GetComponent<RectTransform>().rect.width) / 3, Random.Range((gameObject.GetComponent<RectTransform>().rect.height * -1) / 3, gameObject.GetComponent<RectTransform>().rect.height) / 3, 0);
                instObject.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

                spawns++;
                timer = 0;
            }
        }
    }
}
