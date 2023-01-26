using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //得点テキスト
    private GameObject scoreText;
    //追加得点
    private int addscore = 0;
    //合計得点（初期値は０）
    private int totalscore = 0;
    //ターゲットのタグ
    private string targettag = "";

    //ゲームオーバーテキスト
    private GameObject gameOverText;
    //ゲームオーバーの判定
    private bool isGameOver = false;

    //HP
    private GameObject life1;
    private GameObject life2;
    private GameObject life3;

    // Start is called before the first frame update
    void Start()
    {
        //シーンビューからオブジェクトの実態を検索する
        this.scoreText = GameObject.Find("Score");
        this.gameOverText = GameObject.Find("GameOver");
        this.life1 = GameObject.Find("Life1");
        this.life2 = GameObject.Find("Life2");
        this.life3 = GameObject.Find("Life3");

    }

    // Update is called once per frame
    void Update()
    {
        //ゲームオーバーになった場合
        if (this.isGameOver == true)
        {
            //クリックされたらシーンをロードする
            if (Input.GetMouseButtonDown(0))
            {
                //SampleSceneを読み込む
                SceneManager.LoadScene("SampleScene"); //SceneManagerクラスのLoadScene関数を使うとシーンを読み込むことができる
            }
        }
        else
        {
            //ScoreTextに合計得点を表示
            this.scoreText.GetComponent<Text>().text = totalscore.ToString() +" pt";
        }
    }

    public void GameOver()
    {
        //ゲームオーバーになったときに、画面上にゲームオーバーを表示する
        this.gameOverText.GetComponent<Text>().text = "Game Over";
        this.isGameOver = true;
    }

    //ペンギンが衝突した時に呼ばれる関数
    public void ScoreCount(string getTag)
    {
        //変数の初期化
        addscore = 0;    //追加得点
        targettag = "";  //タグ

        //取得したタグを代入
        targettag = getTag;

        //ターゲットの種類によって取得できる点数を変える
        if (targettag == "SmallFlake")
        {
            addscore = 5;
        }
        else if (targettag == "LargeFlake")
        {
            addscore = 10;
        }
        else if (targettag == "SnowBall")
        {
            addscore = -10;
        }

        //合計得点に追加得点を加算する
        totalscore += addscore;
    }

    //ペンギンが衝突した時に呼ばれる関数
    public void LifeManage(int Life)
    {
        //残数が3の場合Life3を削除する
        if(Life == 3)
        {
            Destroy(this.life3);
        }
        //残数が2の場合Life2を削除する
        else if (Life == 2) 
        {
            Destroy(this.life2);
        }
        //残数が1の場合Life1を削除する
        else if (Life == 1)
        {
            Destroy(this.life1);
        }
    }
}
