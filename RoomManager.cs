using UnityEngine;

public class RoomManager : MonoBehaviour {
    private bool isInSpawnRoom, isInBossRoom, isInEnemyRoom, isInItemRoom, isInNormalRoom;

    public static RoomManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "BossRoom"){
            isInBossRoom = true;
        }
        if(other.gameObject.name == "EnemyRoom"){
            isInEnemyRoom = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "BossRoom"){
            isInBossRoom = true;
        }
        if(other.gameObject.name == "EnemyRoom"){
            isInEnemyRoom = true;
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.name=="BossRoom" ){
            Reset();
        }
        if(other.gameObject.name == "EnemyRoom"){
            Reset();
        }
    }

    private void Reset(){
        isInSpawnRoom = false;
        isInBossRoom = false;
        isInEnemyRoom = false;
        isInItemRoom = false;
        isInNormalRoom = false;
    }

    public bool IsInSpawnRoom{
        get{return isInSpawnRoom;}
    }

    public bool IsInBossRoom{
        get{return isInBossRoom;}
    }

    public bool IsInEnemyRoom{
        get{return isInEnemyRoom;}
    }
    public bool IsInItemRoom{
        get{return isInItemRoom;}
    }
    public bool IsInNormalRoom{
        get{return isInNormalRoom;}
    }
}