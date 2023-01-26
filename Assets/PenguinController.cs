using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;
    //�y���M�����ړ�������R���|�[�l���g������
    Rigidbody2D rigid2D;
    //�������̑��x
    private float speed = 0.05f;  //�l��ς��Ă݂�i���j0.1
    //�ړ��\�ȕ�
    private float field = 8.0f;
    //�y���M���𔽓]������R���|�[�l���g������
    SpriteRenderer renderer;

    //�c���Life��
    private int Life = 3;

    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();
        //Rigidbody2D�̃R���|�[�l���g���擾����
        this.rigid2D = GetComponent<Rigidbody2D>();
        //SpriteRenderer�̃R���|�[�l���g���擾����
        this.renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //�y���M���̈ʒu���擾����
        Vector2 position = this.transform.position;

        //�����L�[�������ꂽ�ꍇ
        if (Input.GetKey(KeyCode.LeftArrow) && - this.field < this.transform.position.x)
        {
            position.x -= speed;
            renderer.flipX = true;�@//���]����
        }
        //�E��L�[�������ꂽ�ꍇ
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.field)
        {
            position.x += speed;
            renderer.flipX = false;�@//���]���Ȃ�
        }
        //���L�[�������ꂽ�ꍇ
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //�X���C�h�A�j������߂�
            this.myAnimator.SetBool("penguin_slide", false);
        }

        //�y���M�����ړ�����ʒu�����߂�
        transform.position = position;
    }

    //�y���M�����Փ˂����Ƃ��ɌĂ΂��֐�
    private void OnCollisionEnter2D(Collision2D other)
    {
        //��̌����ɏՓ˂����Ƃ�
        if (other.gameObject.tag == "LargeFlake" || other.gameObject.tag == "SmallFlake")
        {
            //�p�[�e�B�N�����Đ�
            GetComponent<ParticleSystem>().Play();
            //�j������
            Destroy(other.gameObject);
        }
        //SnowBall�ɓ��������ꍇ
        if (other.gameObject.tag == "SnowBall")
        {
            //�X���C�h�A�j�����Đ�
            this.myAnimator.SetBool("penguin_slide", true);
            //�j������
            Destroy(other.gameObject);
            //HP�����炷
            GameObject.Find("Canvas").GetComponent<UIController>().LifeManage(this.Life);

            //�c���HP���P�̏ꍇ��GameOver�ɂ���
            if (this.Life == 1)
            {
                //UIController��GameOver�֐����Ăяo���ĉ�ʏ�ɁuGameOver�v�ƕ\������
                GameObject.Find("Canvas").GetComponent<UIController>().GameOver();
            }
            else
            {
                //�c���HP���P���炷
                this.Life -= 1;
            }
        }
        //UIController��GameOver�֐����Ăяo���ē��_�����Z����
        GameObject.Find("Canvas").GetComponent<UIController>().ScoreCount(other.gameObject.tag);
    }
}
