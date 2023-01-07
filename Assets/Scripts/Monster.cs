using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour, Creature, Interactable
{
    public MonsterData data;
    
    public int frequency; //몬스터 속도
    public int nextMove; //몬스터 방향

    public bool trapped;
    public bool rescued;

    public Vector2 boxOffset;
    public Vector2 attackBoundary;

    Rigidbody2D monster;

    public int currentHealth;

    private float _knockbackTime;
    private float _invincibilityTime;
    private bool _isAttack;
    private float _attackTime;

    private SpriteRenderer _sprite;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + boxOffset, attackBoundary);
    }

    private void Awake()
    {
        rescued = false;
        _sprite = GetComponent<SpriteRenderer>();
        monster = GetComponent<Rigidbody2D>();
        currentHealth = data._health;

        Invoke("Think", 1);
    }

    private void Update()
    {
        _attackTime -= Time.deltaTime;
    }

    //충돌 콜라이더
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isAttack && ( collision.collider.CompareTag("Player") ||  collision.collider.CompareTag("NPC"))) 
            collision.collider.GetComponent<Creature>().Hit(data._power, 
                (Vector2) (collision.collider.transform.position - transform.position).normalized,
                0.5f);

        if(collision.collider.CompareTag("Left"))
        {
	        nextMove = 2;
        }

        else if(collision.collider.CompareTag("Right"))
        {
	        nextMove = 1;
        }

        else if(collision.collider.CompareTag("Up"))
        {
	        nextMove = 3;
        }

        else if(collision.collider.CompareTag("Down"))
        {
	        nextMove = 4;
        }

        else if(collision.collider.CompareTag("Obstacle"))
        {
            Vector3 yourloc = collision.gameObject.transform.position;
            Vector3 mypos = monster.transform.position;

            Vector2 dirVec = new Vector2(yourloc.x - mypos.x, yourloc.y - mypos.y);
            dirVec = dirVec.normalized * (-2);
            monster.velocity = new Vector2(dirVec.x, dirVec.y);

            Debug.Log("속도값" + monster.velocity);
        } else if (collision.collider.CompareTag("Trap"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(Trapped());
            trapped = true;
        }
    }


    void FixedUpdate()
    {
        if (trapped) monster.velocity = Vector2.zero;
        
        currentHealth = Mathf.Clamp(currentHealth, 0, data._health);
        if (currentHealth > 0)
        {
            _knockbackTime -= Time.fixedDeltaTime;
            _invincibilityTime -= Time.fixedDeltaTime;
            //기본 Move
            if (_knockbackTime <= 0 && !_isAttack && !trapped && !rescued)
            {
                if (_attackTime <= 0)
                {
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(
                        (Vector2)transform.position + boxOffset,
                        attackBoundary, 0);

                    foreach (var i in collider2Ds)
                    {
                        if (i.tag == "Player" && !i.GetComponent<MovementController>().Transparent_)
                        {
                            monster.velocity = Vector2.zero;
                            StartCoroutine(Attack());
                            _attackTime = data._attackDelay;
                            return;
                        }
                        if (i.tag == "NPC")
                        {
                            monster.velocity = Vector2.zero;
                            StartCoroutine(Attack());
                            _attackTime = data._attackDelay;
                            return;
                        }
                    }
                }

                switch (nextMove)
                {
                    case 0:
                        monster.velocity = new Vector2(monster.velocity.x, monster.velocity.y);
                        break;

                    case 1:
                        monster.velocity = new Vector2(-1, monster.velocity.y);
                        break;

                    case 2:
                        monster.velocity = new Vector2(1, monster.velocity.y);
                        break;

                    case 3:
                        monster.velocity = new Vector2(monster.velocity.x, -1);
                        break;

                    case 4:
                        monster.velocity = new Vector2(monster.velocity.x, 1);
                        break;
                }

                monster.velocity = monster.velocity.normalized * data._speed;
            }
        }
        else
        {
            monster.velocity = Vector2.zero;
        }
    }
    void Think()
    {
        //멈춤, 왼쪽, 오른쪽, 위, 아래 = 0, 1, 2, 3, 4
        nextMove = Random.Range(0, 5);

        Invoke("Think", 1);
    }

    public void Hit(float damage, Vector2 knockback, float backTime)
    {
        if (_invincibilityTime > 0) return;
        currentHealth -= (int) damage - data._defense;
        monster.velocity = knockback;
        _knockbackTime = backTime;
        _invincibilityTime = 0.5f;
        StartCoroutine(HitSparkle());

        if (currentHealth <= 0) Die();
    }
    public void Die()
    {
        Destroy(gameObject, 1f);
    }

    IEnumerator HitSparkle()
    {
        Color color = _sprite.color;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.04f);
            if (i%2 == 0) _sprite.color -= new Color(0, 0, 0, 1);
            else _sprite.color += new Color(0, 0, 0, 1);
        }

        _sprite.color = color;
    }

    IEnumerator Attack()
    {
        _isAttack = true;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(
            (Vector2)transform.position + boxOffset,
            attackBoundary, 0);

        foreach (var i in collider2Ds)
        {
            if (i.tag == "Player" || i.tag == "NPC")
            {
                Transform pos = i.transform;
                monster.velocity = (pos.position - transform.position).normalized * data._speed * 1.3f;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            SpawnTrail();
            yield return new WaitForSeconds(0.1f);
        }
        _isAttack = false;
        monster.velocity = Vector2.zero;
    }
    
    private void SpawnTrail()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer tpr = trailPart.AddComponent<SpriteRenderer>();
        tpr.sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        tpr.sortingLayerName = "Player";
        tpr.flipX = _sprite.flipX;
        trailPart.transform.position = transform.position;
        Destroy(trailPart, 0.5f);
        
        StartCoroutine("FadeTrailPart", tpr);
    }
    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime*3; 
            trailPartRenderer.color = color;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Trapped()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 5) transform.position += new Vector3(Time.deltaTime, 0,0 );
            else transform.position -= new Vector3(Time.deltaTime, 0,0 );
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.4f);
        if (trapped) StartCoroutine(Trapped());
    }

    public void Interact()
    {
        
    }
}