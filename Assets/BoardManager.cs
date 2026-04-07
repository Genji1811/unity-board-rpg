using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform[] tiles;

    void Start()
    {
        Debug.Log("Total tiles: " + tiles.Length);
    }
}
