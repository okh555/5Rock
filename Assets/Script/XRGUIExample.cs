using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XRGUIExample : MonoBehaviour
{


    [SerializeField]
    private Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            startButton.GetComponent<TextMeshProUGUI>().text = "문이 열립니다.";
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
