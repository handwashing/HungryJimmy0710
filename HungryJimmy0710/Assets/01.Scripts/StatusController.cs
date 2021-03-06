using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // 체력
    [SerializeField]
    private int hp;
    private int currentHp;

    // 배고픔
    [SerializeField]
    private int hungry;
    public int currentHungry;

    // 배고픔이 줄어드는 속도
    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    // 목마름
    [SerializeField]
    private float thirsty;
    public float currentThirsty;

    // 목마름이 줄어드는 속도
    [SerializeField]
    private int thirstyDecreaseTime; //지정해둔 속도
    private int currentThirstyDecreaseTime; //계속 변하는 양(시간)

    // 필요한 이미지
    [SerializeField]
    private Image[] images_Gauge;

    private bool isDead = false;
    private HealthManager theHealth;

    public GameObject rainPrefab; // 비 내리는 파티클 이펙트 오브젝트 

    public bool isRain = false; // 비가 오는지 확인

    private const int HUNGRY = 0, THIRSTY = 1, HP = 2;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        currentHungry = hungry;
        currentThirsty = thirsty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Hungry();
            Thirsty();
            GaugeUpdate();
        }
    }


    private void Hungry()       // 배고픔 구현
    {
        if (currentHungry > 0)      // 현재 배고픔이 0보다 클 경우에만 깎음
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
                currentHungryDecreaseTime++;
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else        // 0보다 작아졌을때
            theHealth.Dead();
            // Debug.Log("배고픔 수치가 0이 되었습니다");
    }

    private void Thirsty()      // 목마름 구현
    {
        if (currentThirsty > 0)
        {
            //
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
                currentThirstyDecreaseTime++; //1씩 계속 증가
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
            if(!isRain)
            {
                Raining();
                isRain = false;
            }
        }
        else
            theHealth.Dead();
            // Debug.Log("목마름 수치가 0이 되었습니다");
    }

    private void Raining() //비 내리기
    {
        if (rainPrefab.activeInHierarchy) //하이어라키 창에 비 프리팹이 활성화 되었다면
        {
            currentThirsty += 3f * Time.deltaTime; //총 30의 수분 주기(비가 10초동안 옴)
        }
    }

    private void GaugeUpdate()      // 상태 수치 변화 시각화
    {
        images_Gauge[HP].fillAmount = (float)currentHp / hp;
        images_Gauge[HUNGRY].fillAmount = (float)currentHungry / hungry;
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
    }

    // HP 회복 (아이템 사용시)
    public void IncreaseHP(int _count)
    {
        if (currentHp + _count < hp)        // currentHp와 회복될 수치를 더했을때 hp가 넘는가?
        {
            currentHp += _count;
        }
        else
            currentHp = hp;
    }

    // HP 감소
    public void DecreaseHP(int _count)
    {
        currentHp -= _count;

        if (currentHp <= 0)     // currentHp가 0 이하가 되면 죽음
            Debug.Log("캐릭터의 hp가 0이 되었습니다!!");
    }

    public void IncreaseThirsty(int _count)
    {
        if (currentThirsty + _count < thirsty )
        {
            currentThirsty += _count;
            // if (rainPrefab.activeInHierarchy)
            // {
            //     currentThirsty += 30;
            //     //_count = 30;
            //     //theStatus.IncreaseThirsty(30);
            // }
        }
        else
            currentThirsty = thirsty;
    }

    // Hungry 증가
    public void IncreaseHungry(int _count)
    {
        if (currentHungry + _count < hungry )
        {
            currentHungry += _count;
        }
        else
            currentHungry = hungry;
    }

    // Hungry 감소
    public void DecreaseHungry(int _count)
    {
        currentHungry -= _count;
    }


}
