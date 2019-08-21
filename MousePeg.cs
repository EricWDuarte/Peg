using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePeg : MonoBehaviour
{
    public static bool holdingPeg = false;

    public GameObject pegSprite;
    private Vector2 pegOffset;


    // Start is called before the first frame update
    void Start()
    {
        holdingPeg = false;
        pegOffset = pegSprite.transform.localScale / 2;

    }

    // Update is called once per frame
    void Update()
    {

        pegSprite.SetActive(holdingPeg);

        pegSprite.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 temp = pegSprite.transform.position;
        temp.z = 0;
        pegSprite.transform.position = temp;
    }
}
