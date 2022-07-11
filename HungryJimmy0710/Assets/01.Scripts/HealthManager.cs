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
    private bool isRainning = false;
    private bool isDancing = false;
    private bool isRainPose = false;
    private bool isEnd = false;

    public GameObject rainPrefab; //비 효과



    // Start is called before the first frame update
    void Start()
    {
        animator = thePlayer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Dead();
            Dancing();
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
        if(rainPrefab.activeSelf == true) //비가 내릴 경우에
        {
            StartCoroutine(Dance()); //댄싱 코루틴 실행
        }
    }

    IEnumerator Dance()
    {
        isDancing = true; //파라미터 값이 isDancing이면....
        animator.SetBool("isDancing", isDancing); //비맞는 애니메이션 실행
        yield return new WaitForSeconds (1f); //1초 정도 대기 후
        isRainPose = true;
        animator.SetBool("isRainPose", isRainPose); //비맞는 포즈 애니메이션 실행
        yield return new WaitForSeconds (9f); //9초 정도 대기 후
        animator.SetBool("isEnd",isEnd);
        isEnd = true;
    }

}
