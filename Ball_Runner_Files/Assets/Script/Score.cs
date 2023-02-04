using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int Rotate_Speed;

    void Update()
    {
        transform.Rotate( 0, 0,Rotate_Speed * Time.deltaTime);
    }
}
