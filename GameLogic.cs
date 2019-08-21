using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole
{
    public bool isActive;
    public GameObject holeGO;
    public bool upsideDown;
    public bool hasPeg = true;
    public Vector2Int arrayPos;

    public void Start()
    {
        if (isActive)
            holeGO.GetComponent<HoleGO>().thisHole = this;

        holeGO.SetActive(isActive);
    }

    public void Update()
    {
    }
}

public class GameLogic : MonoBehaviour
{
    public Hole[,] board = new Hole[7, 7];
    public bool[,] activeHoles;
    public bool[,] activeHoles2;
    public float scale = 1f;
    public GameObject holeGO;


    public static Hole firstHole;
    public static Hole secondHole;

    private Hole midHole;

    public static GameLogic gameLogic;


    void Start()
    {
        gameLogic = this;
        //scale = Screen.
        activeHoles = new bool[,] {
            {false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false},
            {false, true,  true,  true,  true,  true,  false},
            {false, true,  true,  true,  true,  true,  false},
            {false, false, true,  true,  true,  false, false},
            {false, false, false, false, false, false, false},
            {false, false, false, false, false, false, false}
        };
        activeHoles2 = new bool[,] {
            {false, false, false, false, false, false, false},
            {true,  true,  true,  true,  true,  true,  true },
            {true,  true,  true,  true,  true,  true,  true },
            {true,  true,  true,  true,  true,  true,  true },
            {true,  true,  true,  true,  true,  true,  true },
            {false, true,  true,  true,  true,  true,  false},
            {false, false, false, false, false, false, false}
        };

        board = FillBoard(board, activeHoles);

    }

    Hole[,] FillBoard(Hole[,] newBoard, bool[,] active)
    {
        for (int x = 0; x < newBoard.GetLength(0); x++)
        {
            for (int y = 0; y < newBoard.GetLength(1); y++)
            {
                Hole newHole = new Hole();

                bool upsideDown = (((x + y) % 2) == 1);

                Vector3 pos = new Vector3(((x - 3.5f) / 2f) + 0.25f , ((y - 3.5f) * 0.86f) + 0.43f, 0) * scale;
                Quaternion rot = Quaternion.Euler(0, 0, ((x + y) % 2) * 180f);

                newHole.isActive = active[y, x];
                newHole.holeGO = Instantiate(holeGO, transform);
                newHole.holeGO.transform.localPosition = pos;
                newHole.holeGO.transform.localRotation = rot;
                newHole.upsideDown = upsideDown;
                newHole.arrayPos = new Vector2Int(x, y);
                if (x == 3 && y == 3)
                    newHole.hasPeg = false;

                newHole.Start();

                newBoard[x, y] = newHole;
            }
        }
        return newBoard;
    }

    public void CheckMove()
    {
        Vector2Int aPeg = firstHole.arrayPos;
        Vector2Int bPeg = secondHole.arrayPos;

        if ((Mathf.Abs(aPeg.x - bPeg.x) == 2 && aPeg.y - bPeg.y == 0)
            || (Mathf.Abs(aPeg.x - bPeg.x) == 1 && Mathf.Abs(aPeg.y - bPeg.y) == 1))
        {
            if(secondHole.hasPeg == false)
            {
                if (!firstHole.upsideDown && aPeg.y > bPeg.y)
                {
                    midHole = board[aPeg.x, aPeg.y - 1];

                }
                else if (firstHole.upsideDown && aPeg.y < bPeg.y)
                {
                    midHole = board[aPeg.x, aPeg.y + 1];

                }
                else if (aPeg.x < bPeg.x)
                {
                    midHole = board[aPeg.x + 1, aPeg.y];

                } else if (aPeg.x > bPeg.x)
                {
                    midHole = board[aPeg.x - 1, aPeg.y];

                }

            if (midHole.hasPeg)
                {
                    midHole.hasPeg = false;
                    secondHole.hasPeg = true;
                    return;
                }

            }
        }


        firstHole.hasPeg = true;
    }

    public void LoadBoard1()
    {
        DestroyHoles();
        FillBoard(board, activeHoles);
    }

    public void LoadBoard2()
    {
        DestroyHoles();
        FillBoard(board, activeHoles2);
    }

    public void DestroyHoles()
    {
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                if (board[x, y].holeGO.gameObject != null) {
                    Destroy(board[x, y].holeGO.gameObject);
                }
            }
        }
    }
}
