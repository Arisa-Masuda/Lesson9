using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //���_�e�L�X�g
    private GameObject scoreText;
    //�ǉ����_
    private int addscore = 0;
    //���v���_�i�����l�͂O�j
    private int totalscore = 0;
    //�^�[�Q�b�g�̃^�O
    private string targettag = "";

    //�Q�[���I�[�o�[�e�L�X�g
    private GameObject gameOverText;
    //�Q�[���I�[�o�[�̔���
    private bool isGameOver = false;

    //HP
    private GameObject life1;
    private GameObject life2;
    private GameObject life3;

    // Start is called before the first frame update
    void Start()
    {
        //�V�[���r���[����I�u�W�F�N�g�̎��Ԃ���������
        this.scoreText = GameObject.Find("Score");
        this.gameOverText = GameObject.Find("GameOver");
        this.life1 = GameObject.Find("Life1");
        this.life2 = GameObject.Find("Life2");
        this.life3 = GameObject.Find("Life3");

    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���I�[�o�[�ɂȂ����ꍇ
        if (this.isGameOver == true)
        {
            //�N���b�N���ꂽ��V�[�������[�h����
            if (Input.GetMouseButtonDown(0))
            {
                //SampleScene��ǂݍ���
                SceneManager.LoadScene("SampleScene"); //SceneManager�N���X��LoadScene�֐����g���ƃV�[����ǂݍ��ނ��Ƃ��ł���
            }
        }
        else
        {
            //ScoreText�ɍ��v���_��\��
            this.scoreText.GetComponent<Text>().text = totalscore.ToString() +" pt";
        }
    }

    public void GameOver()
    {
        //�Q�[���I�[�o�[�ɂȂ����Ƃ��ɁA��ʏ�ɃQ�[���I�[�o�[��\������
        this.gameOverText.GetComponent<Text>().text = "Game Over";
        this.isGameOver = true;
    }

    //�y���M�����Փ˂������ɌĂ΂��֐�
    public void ScoreCount(string getTag)
    {
        //�ϐ��̏�����
        addscore = 0;    //�ǉ����_
        targettag = "";  //�^�O

        //�擾�����^�O����
        targettag = getTag;

        //�^�[�Q�b�g�̎�ނɂ���Ď擾�ł���_����ς���
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

        //���v���_�ɒǉ����_�����Z����
        totalscore += addscore;
    }

    //�y���M�����Փ˂������ɌĂ΂��֐�
    public void LifeManage(int Life)
    {
        //�c����3�̏ꍇLife3���폜����
        if(Life == 3)
        {
            Destroy(this.life3);
        }
        //�c����2�̏ꍇLife2���폜����
        else if (Life == 2) 
        {
            Destroy(this.life2);
        }
        //�c����1�̏ꍇLife1���폜����
        else if (Life == 1)
        {
            Destroy(this.life1);
        }
    }
}
