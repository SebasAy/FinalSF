using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket_Controller : MonoBehaviour
{
    [Header("Sistema de puntuación")]
    public string valid_tag = "Fruits";
    public UI_Manager ui_manager;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == valid_tag)
        //{
        //    ui_manager.Getting_Points();
        //}
        ui_manager.Getting_Points();
    }
}
