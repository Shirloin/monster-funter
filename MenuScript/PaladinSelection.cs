using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSelection : MonoBehaviour
{
    public SceneLoader sl;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void OnMouseDown()
    {
        PlayerPrefs.SetInt("selectedCharacter", 1);
        print(1);
        sl.LoadScene("GameScene");
    }
}
