using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;
    //�y���M�����ړ�������R���|�[�l���g������
    Rigidbody2D rigid2D;
    //�y���M���𔽓]������R���|�[�l���g������
    SpriteRenderer renderer;

    //�������̑��x
    private float speed = 0.07f;
    //�ړ��\�ȕ�
    private float field = 8.0f;
    
    // �_���[�W����t���O
    private bool isDamage;
    // �X���C�h����t���O
    private bool isSlide;

    //�_�ŉ�
    private int flashTime = 10;
    //�_�ŃX�s�[�h
    private float flashSpeed = 0.1f;

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

        //�y���M�����X���C�h���Ă��Ȃ���΍��E�̖��L�[��������
        if (isSlide == false) 
        {
            //�����L�[�������ꂽ�ꍇ
            if (Input.GetKey(KeyCode.LeftArrow) && -this.field < this.transform.position.x)
            {
                position.x -= speed;
                renderer.flipX = true; //���]����
            }
            //�E��L�[�������ꂽ�ꍇ
            else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.field)
            {
                position.x += speed;
                renderer.flipX = false; //���]���Ȃ�
            }
        }
        
        //���L�[�������ꂽ�ꍇ
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //�X���C�h�A�j������߂�
            this.myAnimator.SetBool("penguin_slide", false);
            //�X���C�h���Ă����Ԃ���߂�
            isSlide = false;
        }

        //�y���M�����ړ�����ʒu�����߂�
        transform.position = position;
    }

    //�y���M�����Փ˂����Ƃ��ɌĂ΂��֐�
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�_���[�W���󂯂Ă���ꍇ�͏������Ȃ�
        if (isDamage) 
        {
            return;
        }

        //��̌����ɏՓ˂����Ƃ�
        if (other.gameObject.tag == "LargeFlake" || other.gameObject.tag == "SmallFlake")
        {
            //�p�[�e�B�N�����Đ�
            GetComponent<ParticleSystem>().Play();
            //�j������
            Destroy(other.gameObject);
        }
        //��ʂ��X���ɓ��������ꍇ
        else if (other.gameObject.tag == "SnowBall" || other.gameObject.tag == "Spike")
        {
            //�y���M����_�ł�����i�R���[�`�����J�n�j
            StartCoroutine(OnDamage());
            //�X���C�h�A�j�����Đ�
            this.myAnimator.SetBool("penguin_slide", true);
            //�X���C�h���Ă����Ԃɂ���
            isSlide = true;

            //�A�C�e����j������
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

    //�_���[�W���󂯂ē_�ł�����Ƃ��ɌĂ΂��֐�
    IEnumerator OnDamage()
    {
        //�_���[�W���󂯂Ă����Ԃɂ���
        isDamage = true;

        //�_�ł�����
        for (int i = 0; i < flashTime; i++)
        {
            //flashSpeed���p��������
            yield return new WaitForSeconds(flashSpeed);
            //spriteRenderer���I�t
            renderer.enabled = false;

            //flashSpeed���p��������
            yield return new WaitForSeconds(flashSpeed);
            //spriteRenderer���I��
            renderer.enabled = true;
        }

        // �ʏ��Ԃɖ߂�
        isDamage = false;
    }
}
