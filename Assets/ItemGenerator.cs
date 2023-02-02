using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //SnowBall���擾
    public GameObject SnowBallPrefab;
    //SmallSnowFlake���擾
    public GameObject SmallSnowFlakePrefab;
    //LargeSnowFlake���擾
    public GameObject LargeSnowFlakePrefab;
    //SpikePrefab���擾
    public GameObject SpikePrefab;

    // ���Ԍv���p�̕ϐ�
    private float delta = 0;
    // �A�C�e���̐����Ԋu
    private float span = 1.3f;
    // �A�C�e���̐����܂ł̎���
    private float genTime = 0f;
    // �A�C�e���̐����ʒu�FX���W
    private float genPosX = 0f;
    // �A�C�e���̐����ʒu�FY���W
    private float genPosY = 8.0f;
    // �A�C�e���̍ő吶����
    private int maxItemNum = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;

        // span�b�ȏ�̎��Ԃ��o�߂������𒲂ׂ�
        if (this.delta > this.genTime)
        {
            this.delta = 0;    // �A�C�e���̐����Ԋu
            this.genPosX = -9.0f; // �A�C�e���̐����ʒu�FX���W

            // �w�肵���������A�C�e���𐶐�����
            for (int i = 0; i <= this.maxItemNum; i++)
            {
                //�ǂ̃A�C�e�����o���̂��������_���ɐݒ�
                int num = Random.Range(0, 10);
                //���Ƃ��A�C�e����X�������߂�
                genPosX += span;

                //SnowBall�𐶐�����
                if (num <= 1)
                {
                    GameObject go = Instantiate(SnowBallPrefab);
                    go.transform.position = new Vector2(genPosX, genPosY);
                }
                //Spike�𐶐�����
                if (num == 2)
                {
                    GameObject go = Instantiate(SpikePrefab);
                    go.transform.position = new Vector2(genPosX, genPosY);
                }
                //SmallSnowFlake�𐶐�����
                else if (4 <= num && num <= 6)
                {
                    GameObject go = Instantiate(SmallSnowFlakePrefab);
                    go.transform.position = new Vector2(genPosX, genPosY);
                }
                //LargeSnowFlake�𐶐�����
                else if (8 <= num)
                {
                    GameObject go = Instantiate(LargeSnowFlakePrefab);
                    go.transform.position = new Vector2(genPosX, genPosY);
                }
            }
            // ���̃L���[�u�܂ł̐������Ԃ����߂�
            this.genTime = this.delta + 1.5f;
        }
    }
}
