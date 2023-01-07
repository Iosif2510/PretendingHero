using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour, Creature
{
    [SerializeField] private Transform sight;
    
    public NPCData data;
    
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidbody;

    private GameObject _target;

    public float hp, maxHp;
    
    public float speed;
    public float rate;
    
    public bool isAttack;

    private Vector2 _dir;

    public Vector2 dir => _dir;
    
    private float _attackTime;
    private float _invincibilityTime;
    private float _knockbackTime;

    private void Start()
    {
        hp = maxHp;
        
        _dir = Vector2.down;
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        Invoke("Think", 0f);
    }

    private void Update()
    {
        _knockbackTime -= Time.deltaTime;
        _invincibilityTime -= Time.deltaTime;
        _attackTime -= Time.deltaTime;

        sight.rotation = Quaternion.Lerp(sight.rotation, Quaternion.Euler(0, 0,
                Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg + 90
            ), rate * Time.deltaTime);
        
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            Think();
            isAttack = false;
        }
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody.velocity = _dir * speed;
            var _velocity = _rigidbody.velocity;


            if (_sprite.flipX && _velocity.x > 0) _sprite.flipX = false;
            else if (!_sprite.flipX && _velocity.x < 0) _sprite.flipX = true;

            if (_velocity.magnitude == 0) _animator.SetBool("walk", false);
            else
            {
                _animator.SetBool("walk", true);
                if (_velocity.x != 0) _animator.SetFloat("dir", 0.5f);
                else if (_velocity.y > 0) _animator.SetFloat("dir", 1);
                else _animator.SetFloat("dir", 0);
            }
        }
    }
    
    public void Think()
    {
        if (!isAttack && _knockbackTime <= 0)
        {
            Vector3 dir = Quaternion.AngleAxis(Random.Range(-90f, 90f), Vector3.forward) * _dir;
            if (_target != null)
            {
                //멈춤, 왼쪽, 오른쪽, 위, 아래 = 0, 1, 2, 3, 4
                dir = (_target.transform.position - transform.position).normalized;
            }

            _dir = dir;
        }

        Invoke("Think", 2);
    }

    public virtual void Attack(GameObject mon)
    {
        if (!isAttack)
        {
            if (_target != null)
            {
                //멈춤, 왼쪽, 오른쪽, 위, 아래 = 0, 1, 2, 3, 4
                _dir = (_target.transform.position - transform.position).normalized;
            }
            _target = mon;
            _animator.SetTrigger("attack");
            isAttack = true;
            _rigidbody.velocity = Vector2.zero;
        }
    }
    
    public void Hit(float damage, Vector2 knockback, float backTime)
    {
        if (_invincibilityTime > 0) return;
        hp -= (int) damage;
        _rigidbody.velocity = knockback;
        _knockbackTime = backTime;
        _invincibilityTime = 1f;
        StartCoroutine(HitSparkle());

        if (hp <= 0) Die();
    }
    public void Die()
    {
        isAttack = false;
        Destroy(gameObject);
    }
    
    IEnumerator HitSparkle()
    {
        Color color = _sprite.color;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.08f);
            if (i%2 == 0) _sprite.color -= new Color(0, 0, 0, 1);
            else _sprite.color += new Color(0, 0, 0, 1);
        }

        _sprite.color = color;
    }
}
