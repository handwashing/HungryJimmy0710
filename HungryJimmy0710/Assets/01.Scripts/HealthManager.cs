using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [SerializeField] private StatusController theStatus;
    [SerializeField] private PlayerController thePlayer;

    //애니메이션
    public Animator animator;
    private bool isDead = false;
    private bool isDancing = false;
    private bool isEnd = false;
    private bool isRainPose = false;
    private bool isMove = false;

    public GameObject rainPrefab; //비 효과

    void Start()
    {
        animator = thePlayer.GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            Dead();
            if(rainPrefab.activeInHierarchy) //살아있는 상태에서 비가 온다면...
            {
                Dancing();
            }
        }

    }


    public void Dead()
    {
        if (theStatus.currentHungry <= 0)
        {
            isDead = true;
            animator.SetTrigger("isDead");
            GameManager.isPause = true;
        }
    }

    public void Dancing() //댄싱 애니메이션
    {
        
        
            StartCoroutine(Dance()); //댄싱 코루틴 실행
        
    }

    IEnumerator Dance()
    {
        
        isDancing = true; //파라미터 값이 isDancing이면....
        animator.SetBool("isDancing", isDancing); //비맞는 애니메이션 실행
        yield return new WaitForSeconds (1f); //1초 정도 대기 후
        isRainPose = true; //파라미터 값 isRainPose
        animator.SetBool("isDancing", false); //댄싱 애니메이션 중지
        animator.SetBool("isRainPose",isRainPose); //비맞는 포즈 애니메이션 실행
        yield return new WaitForSeconds (9f); //9초 정도 대기 후
        isEnd = true; //비맞는 애니메이션도 중지
        animator.SetBool("isEnd",isEnd);

    }

}
