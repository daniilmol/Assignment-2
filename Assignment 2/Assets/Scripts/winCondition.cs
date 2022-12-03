using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class winCondition : MonoBehaviour
{
    public int goalNumber = 0;
    // Start is called before the first frame update
    public TextMeshProUGUI winCon;

    public void setWinCondition(int goalNumber) {
        winCon.text = goalNumber.ToString() + winCon.text;
        //winCon = GetComponent<TextMeshProUGUI>().text 
    }

    //private void Update()
    //{
    //    setWinCondition(goalNumber);
    //}
}
