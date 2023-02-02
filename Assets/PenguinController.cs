using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //ペンギンを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;
    //ペンギンを反転させるコンポーネントを入れる
    SpriteRenderer renderer;

    //横方向の速度
    private float speed = 0.07f;
    //移動可能な幅
    private float field = 8.0f;
    
    // ダメージ判定フラグ
    private bool isDamage;
    // スライド判定フラグ
    private bool isSlide;

    //点滅回数
    private int flashTime = 10;
    //点滅スピード
    private float flashSpeed = 0.1f;

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

        //ペンギンがスライドしていなければ左右の矢印キーが押せる
        if (isSlide == false) 
        {
            //左矢印キーが押された場合
            if (Input.GetKey(KeyCode.LeftArrow) && -this.field < this.transform.position.x)
            {
                position.x -= speed;
                renderer.flipX = true; //反転する
            }
            //右印キーが押された場合
            else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.field)
            {
                position.x += speed;
                renderer.flipX = false; //反転しない
            }
        }
        
        //上印キーが押された場合
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //スライドアニメから戻す
            this.myAnimator.SetBool("penguin_slide", false);
            //スライドしている状態から戻す
            isSlide = false;
        }

        //ペンギンが移動する位置を決める
        transform.position = position;
    }

    //ペンギンが衝突したときに呼ばれる関数
    private void OnTriggerEnter2D(Collider2D other)
    {
        //ダメージを受けている場合は処理しない
        if (isDamage) 
        {
            return;
        }

        //雪の結晶に衝突したとき
        if (other.gameObject.tag == "LargeFlake" || other.gameObject.tag == "SmallFlake")
        {
            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();
            //破棄する
            Destroy(other.gameObject);
        }
        //雪玉か氷柱に当たった場合
        else if (other.gameObject.tag == "SnowBall" || other.gameObject.tag == "Spike")
        {
            //ペンギンを点滅させる（コルーチンを開始）
            StartCoroutine(OnDamage());
            //スライドアニメを再生
            this.myAnimator.SetBool("penguin_slide", true);
            //スライドしている状態にする
            isSlide = true;

            //アイテムを破棄する
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

    //ダメージを受けて点滅させるときに呼ばれる関数
    IEnumerator OnDamage()
    {
        //ダメージを受けている状態にする
        isDamage = true;

        //点滅させる
        for (int i = 0; i < flashTime; i++)
        {
            //flashSpeed分継続させる
            yield return new WaitForSeconds(flashSpeed);
            //spriteRendererをオフ
            renderer.enabled = false;

            //flashSpeed分継続させる
            yield return new WaitForSeconds(flashSpeed);
            //spriteRendererをオン
            renderer.enabled = true;
        }

        // 通常状態に戻す
        isDamage = false;
    }
}
