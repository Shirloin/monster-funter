using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    private SceneLoader sceneLoader;
    public static ChangeScene instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance==null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        sceneLoader = GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameScene(){
        sceneLoader.LoadScene("GameScene");
    }

    public void MenuScene()
    {
        sceneLoader.LoadScene("MenuScene");
    }

    public void MazeScene(){
        sceneLoader.LoadScene("MazeScene");
    }

    
}
