using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    [SerializeField] private float returnSpeed; // 返回速度
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;
    private int facingDir;
    private bool canRotate = true;
    private bool isReturning = false;

    [SerializeField] private float detectionRadius = 5f; // 检测敌人的半径
    [SerializeField] private LayerMask enemyLayer; // 敌人所在的层
    [SerializeField] private float initialSpeed = 5f; // 初始速度

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if(canRotate)
            transform.right = rb.velocity;
        if(isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, player.transform.position) < 0.1f)
            {
                player.CatchTheSword();
            }
        }
    }
    public void SetupSword(float _gravityScale, int _facingDir, Player _player)
    {
        player = _player;
        anim.SetBool("rotation", true);
        if (rb != null)
        {
            rb.gravityScale = _gravityScale;
            // 根据 facingDir 设置初始方向和速度
            rb.velocity = new Vector2(_facingDir * initialSpeed, 0);
        }
        else
        {
            Debug.LogError("Rigidbody2D component is null!");
        }

        facingDir = _facingDir;
        // 启动自动寻敌功能
        StartCoroutine(AutoSeekEnemy());
    }
    public void ReturnSword()
    {
        anim.SetBool("rotation", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }
    private IEnumerator AutoSeekEnemy()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
            List<Collider2D> validEnemies = new List<Collider2D>();

            foreach (var collider in colliders)
            {
                Vector2 directionToEnemy = collider.transform.position - transform.position;
                if ((facingDir == 1 && directionToEnemy.x > 0) || (facingDir == -1 && directionToEnemy.x < 0))
                {
                    validEnemies.Add(collider);
                }
            }

            if (validEnemies.Count > 0)
            {
                Transform nearestEnemy = GetNearestEnemy(validEnemies.ToArray());
                if (nearestEnemy != null)
                {
                    Vector2 direction = (nearestEnemy.position - transform.position).normalized;
                    rb.velocity = direction * initialSpeed; // 调整剑的速度方向
                }
            }
            yield return new WaitForSeconds(0.1f); // 每0.1秒检测一次
        }
    }

    private Transform GetNearestEnemy(Collider2D[] enemies)
    {
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isReturning)
        {
            return;
        }
        anim.SetBool("rotation", false);
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
