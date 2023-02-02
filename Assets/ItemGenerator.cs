using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //SnowBallを取得
    public GameObject SnowBallPrefab;
    //SmallSnowFlakeを取得
    public GameObject SmallSnowFlakePrefab;
    //LargeSnowFlakeを取得
    public GameObject LargeSnowFlakePrefab;
    //SpikePrefabを取得
    public GameObject SpikePrefab;

    // 時間計測用の変数
    private float delta = 0;
    // アイテムの生成間隔
    private float span = 1.3f;
    // アイテムの生成までの時間
    private float genTime = 0f;
    // アイテムの生成位置：X座標
    private float genPosX = 0f;
    // アイテムの生成位置：Y座標
    private float genPosY = 8.0f;
    // アイテムの最大生成数
    private int maxItemNum = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;

        // span秒以上の時間が経過したかを調べる
        if (this.delta > this.genTime)
        {
            this.delta = 0;    // アイテムの生成間隔
            this.genPosX = -9.0f; // アイテムの生成位置：X座標

            // 指定した数だけアイテムを生成する
            for (int i = 0; i <= this.maxItemNum; i++)
            {
                //どのアイテムを出すのかをランダムに設定
                int num = Random.Range(0, 10);
                //落とすアイテムのX軸を決める
                genPosX += span;

                //SnowBallを生成する
                if (num <= 1)
                {
                    GameObject go = Instantiate(SnowBallPrefab);
                    go.transform.position = new Vector2(genPosX, genPosY);
                }
                //Spikeを生成する
                if (num == 2)
                {
                    GameObject go = Instantiate(SpikePrefab);
                    go.transform.position = new Vector2(genPosX, genPosY);
                }
                //SmallSnowFlakeを生成する
                else if (4 <= num && num <= 6)
                {
                    GameObject go = Instantiate(SmallSnowFlakePrefab);
                    go.transform.position = new Vector2(genPosX, genPosY);
                }
                //LargeSnowFlakeを生成する
                else if (8 <= num)
                {
                    GameObject go = Instantiate(LargeSnowFlakePrefab);
                    go.transform.position = new Vector2(genPosX, genPosY);
                }
            }
            // 次のキューブまでの生成時間を決める
            this.genTime = this.delta + 1.5f;
        }
    }
}
