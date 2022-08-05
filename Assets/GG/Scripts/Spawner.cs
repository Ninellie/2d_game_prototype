//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static System.Random;

//public class Spawner : MonoBehaviour
//{
//    [Header("Set in Inspector")]
//    public GameObject[] prefabEnemies; // ������ �������� Enemy
//    public float enemySpawnPerSecond = 0.5f; // ��������� �������� � �������
//    public float enemyDefaultPadding = 1.5f; // ������ ��� ����������������

//    private BoundsCheck bndCheck;


//    [Header("Set in Inspector")]
//    public float radius = 1f;
//    public bool keepOnScreen = true;

//    [Header("Set Dynamically")]
//    public bool isOnScreen = true;
//    public float camWidth;
//    public float camHeight;
//    [HideInInspector]
//    public bool offRight, offLeft, offUp, offDown;


//    void Awake()
//    {
//        camHeight = Camera.main.orthographicSize;
//        camWidth = camHeight * Camera.main.aspect;
//    }

//    public void SpawnEnemy()
//    {
//        // ������� ��������� ������ Enemy ��� ��������
//        int ndx = Random.Range(0, prefabEnemies.Length);
//        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

//        // ���������� ��������� ������� ��� ������� � ��������� ������� x
//        float enemyPadding = enemyDefaultPadding;
//        if (go.GetComponent<BoundsCheck>() != null)
//        {
//            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
//        }

//        // ���������� ��������� ���������� ���������� ���������� �������
//        Vector3 pos = Vector3.zero;
//        float xMin = -bndCheck.camWidth + enemyPadding;
//        float xMax = bndCheck.camWidth - enemyPadding;
//        pos.x = Random.Range(xMin, xMax);
//        pos.y = bndCheck.camHeight + enemyPadding;
//        go.transform.position = pos;

//        // ����� ������� SpawnEnemy()
//        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
