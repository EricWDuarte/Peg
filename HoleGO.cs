using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGO : MonoBehaviour
{
    public Hole thisHole;
    public GameObject peg;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = Vector3.zero;
        pos.y -= 0.11f;
        peg = Instantiate(peg, transform.position, Quaternion.identity, transform);
        peg.transform.localPosition = pos;
    }

    // Update is called once per frame
    void Update()
    {
        peg.SetActive(thisHole.hasPeg);
    }

    private void OnMouseDown()
    {
        if (MousePeg.holdingPeg)
        {
            GameLogic.secondHole = thisHole;
            GameLogic.gameLogic.CheckMove();
            MousePeg.holdingPeg = false;
            Game_Manager.manager.PlaySound();

        }
        else
        {
            if (thisHole.hasPeg)
            {
                GameLogic.firstHole = thisHole;
                MousePeg.holdingPeg = true;
                thisHole.hasPeg = false;
                Game_Manager.manager.PlaySound();

            }
        }
    }
}
