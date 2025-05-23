using System;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    [SerializeField] bool debugMode;
    [SerializeField] Vector3[] playerPositionByLevel;
    [SerializeField] private int curLevel;

 
    Transform p;
    public int KeyCount;

    private void Awake()
    {
        if (instance != null)
        {
            instance.SetUp();
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            instance.SetUp();
            DontDestroyOnLoad(instance);
        }
    }
    private void SetUp()
    {
        p = GameObject.Find("Ball").transform;
        if (debugMode) return;
        p.position = playerPositionByLevel[curLevel];

        Vector3 scale = p.localScale;
        scale.x = curLevel % 2 == 0 ? 1 : -1;
        p.localScale = scale;
    }


    public void P_NextLevel()
    {
        curLevel++;

        Vector3 scale = p.localScale;
        scale.x = curLevel % 2 == 0 ? 1 : -1;
        p.localScale = scale;
    }
    public void P_SetLevel(int value)
    { curLevel = value; }

}
