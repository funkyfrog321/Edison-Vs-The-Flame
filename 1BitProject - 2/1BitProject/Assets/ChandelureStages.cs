using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelureStages : MonoBehaviour
{
    SpriteRenderer renderer;
    public Sprite Stage2;
    public Sprite Stage3;
    public Sprite Stage4;
    public Sprite Stage5;
    public Sprite StageFinal;
    int StageCounter = 0;
    List<Sprite> SpriteList = new List<Sprite>();

    public List<GameObject> glowObjects;
    

    // Start is called before the first frame update
    void Start()
    {
        SpriteList.Add(Stage2);
        SpriteList.Add(Stage3);
        SpriteList.Add(Stage4);
        SpriteList.Add(Stage5);
        SpriteList.Add(StageFinal);
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void progressStage()
    {
        renderer.sprite = SpriteList[StageCounter];
        Destroy(glowObjects[StageCounter]);

        StageCounter = StageCounter + 1;

    }







}
