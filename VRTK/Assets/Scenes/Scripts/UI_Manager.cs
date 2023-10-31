using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("Sistema de puntuación")]
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        score=0;
    }

    public void Getting_Points(){
        score++;
        Debug.Log("Score: " + score);
    }
}
