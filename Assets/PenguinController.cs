using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //ペンギンを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;
    //横方向の速度
    private float speed = 0.05f;  //値を変えてみる（★）0.1
    //移動可能な幅
    private float field = 8.0f;
    //ペンギンを反転させるコンポーネントを入れる
    SpriteRenderer renderer;

    //残りのLife数
    private int Life = 3;

    // Start is called before the first frame update
    void Start()
    {
        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();
        //Rigidbody2Dのコンポーネントを取得する
        this.rigid2D = GetComponent<Rigidbody2D>();
        //SpriteRendererのコンポーネントを取得する
        this.renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //ペンギンの位置を取得する
        Vector2 position = this.transform.position;

        //左矢印キーが押された場合
        if (Input.GetKey(KeyCode.LeftArrow) && - this.field < this.transform.position.x)
        {
            position.x -= speed;
            renderer.flipX = true;　//反転する
        }
        //右印キーが押された場合
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.field)
        {
            position.x += speed;
            renderer.flipX = false;　//反転しない
        }
        //上印キーが押された場合
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //スライドアニメから戻す
            this.myAnimator.SetBool("penguin_slide", false);
        }

        //ペンギンが移動する位置を決める
        transform.position = position;
    }

    //ペンギンが衝突したときに呼ばれる関数
    private void OnCollisionEnter2D(Collision2D other)
    {
        //雪の決勝に衝突したとき
        if (other.gameObject.tag == "LargeFlake" || other.gameObject.tag == "SmallFlake")
        {
            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();
            //破棄する
            Destroy(other.gameObject);
        }
        //SnowBallに当たった場合
        if (other.gameObject.tag == "SnowBall")
        {
            //スライドアニメを再生
            this.myAnimator.SetBool("penguin_slide", true);
            //破棄する
            Destroy(other.gameObject);
            //HPを減らす
            GameObject.Find("Canvas").GetComponent<UIController>().LifeManage(this.Life);

            //残りのHPが１の場合はGameOverにする
            if (this.Life == 1)
            {
                //UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する
                GameObject.Find("Canvas").GetComponent<UIController>().GameOver();
            }
            else
            {
                //残りのHPを１減らす
                this.Life -= 1;
            }
        }
        //UIControllerのGameOver関数を呼び出して得点を加算する
        GameObject.Find("Canvas").GetComponent<UIController>().ScoreCount(other.gameObject.tag);
    }
}
