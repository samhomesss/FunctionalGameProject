using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum TYPE
    { // 아이템 종류.
        NONE = -1, IRON = 0, APPLE, PLANT, LUMBER, ROCK, PICKAXE,  // 없음, 철광석, 사과, 식물, 나무, 돌맹이, 곡괭이
        NUM,
    }; // 아이템이 몇 종류인가 나타낸다(=3).
};

public class ItemRoot : MonoBehaviour
{
    public GameObject RockPrefab = null; // Prefab 'ROCK'
    public GameObject plantPrefab = null; // Prefab 'Plant'
    public GameObject ironPrefab = null; // Prefab 'Iron'
    public GameObject appleTreePrefab = null; // Prefab 'Tree'

    Apple apple;
    protected List<Transform> respawn_points; // 출현 지점 List.

    public float step_timer = 0.0f;
    //public static float RESPAWN_TIME_APPLE = 5.0f; // 사과 출현 시간 상수.
    public static float RESPAWN_TIME_ROCK = 12.0f; // 철광석 출현 시간 상수.
    public static float RESPAWN_TIME_PLANT = 6.0f; // 식물 출현 시간 상수.

    //private float respawn_timer_apple = 0.0f; // 사과의 출현 시간.
    private float respawn_timer_rock = 0.0f; // 철광석의 출현 시간. 
    private float respawn_timer_plant = 0.0f; // 식물의 출현 시간.
       

    // 초기화 작업을 시행한다.
    void Start()
    {
        // 메모리 영역 확보.
        this.respawn_points = new List<Transform>();
        // "PlantRespawn" 태그가 붙은 모든 오브젝트를 배열에 저장.
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("PlantRespawn");
        apple = FindObjectOfType<Apple>().GetComponent<Apple>();
        RockPrefab = Resources.Load<GameObject>("Rock");
        plantPrefab = Resources.Load<GameObject>("Plant");
        appleTreePrefab = Resources.Load<GameObject>("Tree");
        //ironPrefab = Resources.Load<GameObject>("Iron");

        // 배열 respawns 내의 개개의 GameObject를 순서래도 처리한다.
        foreach (GameObject go in respawns)
        {
            // 렌더러 획득.
            MeshRenderer renderer = go.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            { // 렌더러가 존재하면.
                renderer.enabled = false; // 그 렌더러를 보이지 않게.
            }
            // 출현 포인트 List에 위치 정보를 추가.
            this.respawn_points.Add(go.transform);
        }
      
        // 철광석의 출현 포인트를 취득하고, 렌더러를 보이지 않게.
        GameObject rockrespawn = GameObject.Find("RockRespawn");
        rockrespawn.GetComponent<MeshRenderer>().enabled = false;

        this.respawnRock();
        this.respawnPlant();

        this.respawnPlant();
        this.respawnPlant();
    }

    // 각 아이템의 타이머 값이 출현 시간을 초과하면 해당 아이템을 출현.
    void Update()
    {
        //respawn_timer_apple += Time.deltaTime;
        respawn_timer_rock += Time.deltaTime;
        respawn_timer_plant += Time.deltaTime;
        //if (respawn_timer_apple > RESPAWN_TIME_APPLE)
        //{
        //    respawn_timer_apple = 0.0f;
        //    this.apple.respawnApple();
        //}
        if (respawn_timer_rock > RESPAWN_TIME_ROCK)
        {
            respawn_timer_rock = 0.0f;
            this.respawnRock(); // 철광석을 출현시킨다.
        }
        if (respawn_timer_plant > RESPAWN_TIME_PLANT)
        {
            respawn_timer_plant = 0.0f;
            this.respawnPlant(); // 식물을 출현시킨다.
        }
    }

    // 아이템의 종류를 Item.TYPE형으로 반환하는 메소드.
    public Item.TYPE getItemType(GameObject item_go)
    {
        Item.TYPE type = Item.TYPE.NONE;
        if (item_go != null)
        { // 인수로 받은 GameObject가 비어있지 않으면.
            switch (item_go.tag)
            { // 태그로 분기.
                case "Rock": type = Item.TYPE.ROCK; break;
                case "Apple": type = Item.TYPE.APPLE; break;
                case "Plant": type = Item.TYPE.PLANT; break;
                case "Iron": type = Item.TYPE.IRON; break;
                case "Lumber": type = Item.TYPE.LUMBER; break;
                case "PickAxe": type = Item.TYPE.PICKAXE; break;
            }
        }
        return (type);
    }

    // 철광석을 출현시킨다.
    public void respawnRock()
    {
        // 철광석 프리팹을 인스턴스화.
        GameObject go = GameObject.Instantiate(this.RockPrefab) as GameObject;
        // 철광석의 출현 포인트를 취득.
        Transform trans = GameObject.Find("RockRespawn").transform;
        Vector3 pos = trans.position;
        // 출현 위치를 조정.
        pos.y = 1.0f;
        pos.x += Random.Range(-1.0f, 1.0f);
        pos.z += Random.Range(-1.0f, 1.0f);
        // 철광석의 위치를 이동.
        go.transform.position = pos;
        go.name = RockPrefab.name;
        go.transform.SetParent(trans);
    }

    // 사과를 출현시킨다.
    //public void respawnApple()
    //{
    //    apple.respawnApple();
    //}

    // 식물을 출현시킨다.
    public void respawnPlant()
    {
        if (this.respawn_points.Count > 0)
        { // List가 비어있지 않으면.
          // 식물 프리팹을 인스턴스화.
            GameObject go = GameObject.Instantiate(this.plantPrefab) as GameObject;
            // 식물의 출현 포인트를 랜덤하게 취득.
            int n = Random.Range(0, this.respawn_points.Count);
            Transform trans = this.respawn_points[n];
            Vector3 pos = trans.position;
            // 출현 위치를 조정.
            pos.y = 1.0f;
            pos.x += Random.Range(-1.0f, 1.0f);
            pos.z += Random.Range(-1.0f, 1.0f);
            // 식물의 위치를 이동.
            go.transform.position = pos;
            go.name = plantPrefab.name;
            go.transform.SetParent(trans);
        }
    }

    public void CreatTree(GameObject apple)
    {
        GameObject go = GameObject.Instantiate(this.appleTreePrefab) as GameObject;

        go.transform.position = new Vector3(apple.transform.position.x, 1.1f, apple.transform.position.z);
        go.gameObject.name = appleTreePrefab.name;

    }

    // 들고 있는 아이템에 따른 ‘수리 진척 상태’를 반환
    public float getGainRepairment(GameObject item_go)
    {
        float gain = 0.0f;
        if (item_go == null)
        {
            gain = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.ROCK:
                    gain = GameStatus.GAIN_REPAIRMENT_ROCK; break;
                case Item.TYPE.PLANT:
                    gain = GameStatus.GAIN_REPAIRMENT_PLANT; break;
            }
        }
        return (gain);
    }
    // 들고 있는 아이템에 따른 ‘체력 감소 상태’를 반환
    public float getConsumeSatiety(GameObject item_go)
    {
        float consume = 0.0f;
        if (item_go == null)
        {
            consume = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.ROCK:
                    consume = GameStatus.CONSUME_SATIETY_ROCK; break;
                case Item.TYPE.APPLE:
                    consume = GameStatus.CONSUME_SATIETY_APPLE; break;
                case Item.TYPE.PLANT:
                    consume = GameStatus.CONSUME_SATIETY_PLANT; break;
            }
        }
        return (consume);
    }
    // 들고 있는 아이템에 따른 ‘체력 회복 상태’를 반환
    public float getRegainSatiety(GameObject item_go)
    {
        float regain = 0.0f;
        if (item_go == null)
        {
            regain = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.APPLE:
                    regain = GameStatus.REGAIN_SATIETY_APPLE; break;
                case Item.TYPE.PLANT:
                    regain = GameStatus.REGAIN_SATIETY_PLANT; break;
            }
        }
        return (regain);
    }

    public float getRegainTemperature(GameObject item_go)
    {
        float regain = 0.0f;
        if (item_go == null)
        {
            regain = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.APPLE:
                    regain = GameStatus.REGAIN_TEMPERATURE_APPLE; break;
                case Item.TYPE.PLANT:
                    regain = GameStatus.REGAIN_TEMPERATURE_PLANT; break;
            }
        }
        return (regain);
    }
}
