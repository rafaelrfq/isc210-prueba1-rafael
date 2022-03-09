using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreValueText;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        scoreValueText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        scoreValueText.text = GameManager.scoreJugador.ToString();
    }
}
