using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : IntEventInvoker
{
    [SerializeField]
    TextMeshPro Score;
    
    [SerializeField]
    TextMeshPro HighScore;

    int score = 0;

    private void Start()
    {
        
    }
}
