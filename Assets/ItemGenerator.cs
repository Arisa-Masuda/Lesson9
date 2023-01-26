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

    // 時間計測用の変数
    private float delta = 0;
    // アイテムの生成間隔
    private float span = 1.0f;
    // アイテムの生成位置：X座標
    private float genPosX = -8;
    // アイテムの生成位置：Y座標
    private float genPosY = 8.5f;
    // アイテムの最大生成数
    private int maxItemNum = 14;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime; //(★)

        // span秒以上の時間が経過したかを調べる
        if (this.delta > this.span)
        {
            this.delta = 0;    // アイテムの生成間隔
            this.genPosX = -7; // アイテムの生成位置：X座標

            // 指定した数だけアイテムを生成する
            for (int i = 0; i <= this.maxItemNum; i++)
            {
                //どのアイテムを出すのかをランダムに設定
                int num = Random.Range(1, 10);
                //落とすアイテムのX軸を決める
                genPosX = genPosX + i;

                //SnowBallを生成する
                if (num <= 3)
                {
                    GameObject go = Instantiate(SnowBallPrefab);
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
            this.span = 1.5f + this.delta;
        }
    }
}
