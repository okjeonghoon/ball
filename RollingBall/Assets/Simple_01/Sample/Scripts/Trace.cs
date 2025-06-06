using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour
{
    [Header("추적 설정")]
    public Transform player;            // 추적할 플레이어
    public float moveSpeed = 5f;        // 이동 속도
    public float detectionRange = 10f;  // 감지 범위
    public float chargeSpeed = 10f;     // 돌진 속도

    [Header("돌진 설정")]
    public float chargeCooldown = 3f;   // 돌진 쿨타임
    public float chargeDistance = 15f;  // 돌진 거리
    public float chargeDuration = 1f;   // 돌진 지속 시간
    public float stopDuration = 0.5f;   // 멈춤 지속 시간


    private bool isStopped = false;
    private bool isCharging = false;
    private float lastChargeTime;
    private float chargeStartTime;
    private float stopStartTime;
    private Vector3 chargeDirection;
    private Rigidbody rb;

    private void Awake()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Rigidbody 설정
        rb.freezeRotation = true;       // 회전 방지
        rb.useGravity = true;           // 중력 사용

        lastChargeTime = -chargeCooldown; // 게임 시작시 바로 돌진할 수 있도록 설정
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("플레이어가 설정되지 않았습니다!");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isStopped)
        {
            if (Time.time - stopStartTime >= stopDuration)
            {
                isStopped = false;
            }
            return;
        }

        // 플레이어가 감지 범위 안에 있을 때
        if (distanceToPlayer <= detectionRange)
        {
            // 플레이어 방향 바라보기
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            // 돌진 가능 상태 체크
            if (!isCharging && Time.time >= lastChargeTime + chargeCooldown)
            {
                StartCharge();
            }
        }

        // 돌진 로직
        if (isCharging)
        {
            rb.velocity = chargeDirection * chargeSpeed;

            // 돌진 시간이 지났을 떄
            if (Time.time - chargeStartTime >= chargeDuration)
            {
                StopCharge();
            }
        }

    }

    private void StartCharge()
    {
        isCharging = true;
        chargeStartTime = Time.time;
        lastChargeTime = Time.time;
        chargeDirection = (player.position - transform.position).normalized;
    }

    private void StopCharge()
    {
        isCharging = false;
        isStopped = true;
        stopStartTime = Time.time;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌 시 돌진 멈추기
        if (isCharging)
        {
            StopCharge();
        }
    }
}
