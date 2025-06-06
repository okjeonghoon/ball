using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour
{
    [Header("���� ����")]
    public Transform player;            // ������ �÷��̾�
    public float moveSpeed = 5f;        // �̵� �ӵ�
    public float detectionRange = 10f;  // ���� ����
    public float chargeSpeed = 10f;     // ���� �ӵ�

    [Header("���� ����")]
    public float chargeCooldown = 3f;   // ���� ��Ÿ��
    public float chargeDistance = 15f;  // ���� �Ÿ�
    public float chargeDuration = 1f;   // ���� ���� �ð�
    public float stopDuration = 0.5f;   // ���� ���� �ð�


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

        // Rigidbody ����
        rb.freezeRotation = true;       // ȸ�� ����
        rb.useGravity = true;           // �߷� ���

        lastChargeTime = -chargeCooldown; // ���� ���۽� �ٷ� ������ �� �ֵ��� ����
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("�÷��̾ �������� �ʾҽ��ϴ�!");
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

        // �÷��̾ ���� ���� �ȿ� ���� ��
        if (distanceToPlayer <= detectionRange)
        {
            // �÷��̾� ���� �ٶ󺸱�
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            // ���� ���� ���� üũ
            if (!isCharging && Time.time >= lastChargeTime + chargeCooldown)
            {
                StartCharge();
            }
        }

        // ���� ����
        if (isCharging)
        {
            rb.velocity = chargeDirection * chargeSpeed;

            // ���� �ð��� ������ ��
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
        // �浹 �� ���� ���߱�
        if (isCharging)
        {
            StopCharge();
        }
    }
}
