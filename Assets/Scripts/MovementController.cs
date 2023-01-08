using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour, Creature
{
    [SerializeField] private float speed;
    private PlayerSkillInfos skillInfos {get => PlayerDataManager.Instance.skillInfos;}

    // Temp
    public float hp {get => PlayerDataManager.Instance.hp; set => PlayerDataManager.Instance.hp = value;}
    public float maxHp {get => PlayerDataManager.Instance.maxHp; set => PlayerDataManager.Instance.maxHp = value;}
    public float exp {get => PlayerDataManager.Instance.exp; set => PlayerDataManager.Instance.exp = value;}
    public float maxExp {get => PlayerDataManager.Instance.maxExp; set => PlayerDataManager.Instance.maxExp = value;}
    public int level {get => PlayerDataManager.Instance.level; set => PlayerDataManager.Instance.level = value;}
    public float suspicion {get => PlayerDataManager.Instance.suspicion; set => PlayerDataManager.Instance.suspicion = value;}
    public float maxSuspicion {get => PlayerDataManager.Instance.maxSuspicion; set => PlayerDataManager.Instance.maxSuspicion = value;}

    public int power {get => PlayerDataManager.Instance.power; set => PlayerDataManager.Instance.power = value;}
    public int defense {get => PlayerDataManager.Instance.defense; set => PlayerDataManager.Instance.defense = value;}

    public int skillPoint {get => PlayerDataManager.Instance.skillPoint; set => PlayerDataManager.Instance.skillPoint = value;}
    
    public float[] SkillLevels {get => PlayerDataManager.Instance.skillLevels; set => PlayerDataManager.Instance.skillLevels = value;}
    public float[] SkillCooldown {get => PlayerDataManager.Instance.skillCooldown; set => PlayerDataManager.Instance.skillCooldown = value;}
    // 

    
    public bool Transparent_ => _transparent;
    
    
    private Vector2 _velocity;
    private Vector2 _direction;
    private Vector2 _interactionDir;
    
    private bool _isAttacking;
    private bool _dash;
    private bool _guard;
    private bool _removeTrap;
    private bool _transparent;

    private float _invincibilityTime;
    private float _knockbackTime;
    

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public void AddSkillLevel(int index)
    {
        if (skillPoint == 0) return;
        SkillLevels[index]++;
        skillPoint--;
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _transparent = false;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        _knockbackTime -= Time.deltaTime;
        _invincibilityTime -= Time.deltaTime;
        
        hp = Mathf.Clamp(hp, 0, maxHp);
        exp = Mathf.Clamp(exp, 0, maxExp);

        if (hp <= 0)
        {
            GameManager.Instance.GameOver(Define.Death.NoHealth);
        }

        if (suspicion >= maxSuspicion)
        {
            GameManager.Instance.GameOver(Define.Death.Suspicion);
        }

        if (exp >= maxExp)
        {
            exp = 0;
            LevelUp();
            skillPoint += 2;
        }
        
        // Normal Attack
        if (Input.GetKeyDown(KeyCode.A) && !_isAttacking)
        {
            _animator.SetTrigger("attack");
            _isAttacking = true;
        }
        
        // Interaction
        if (Input.GetKeyDown(KeyCode.D) && !_isAttacking)
        {
            Interaction();
        }
        
        // Dash : skill index == 0
        if (Input.GetKeyDown(KeyCode.Q) && !_isAttacking && _direction.magnitude > 0 && SkillCooldown[0] <= 0
            && SkillLevels[0] > 0)
        {
            StartCoroutine("Dash");
            SetCooldown(0);
        }
        
        // Guard : skill index == 2
        if (Input.GetKeyDown(KeyCode.W) && !_isAttacking && SkillCooldown[2] <= 0
            && SkillLevels[2] > 0)
        {
            StartCoroutine("Guard");
            SetCooldown(2);
        }
        
        // Concentration : skill index == 4
        if (Input.GetKeyDown(KeyCode.E) && !_isAttacking && SkillCooldown[4] <= 0
            && SkillLevels[4] > 0)
        {
            StartCoroutine("Concentration");
            SetCooldown(4);
        }
        
        // Transparent : skill index == 5
        if (Input.GetKeyDown(KeyCode.R) && !_isAttacking
                                        && SkillLevels[5] > 0)
        {
            if (!_transparent && SkillCooldown[5] <= 0)
            {
                StartCoroutine("Transparent");
                SetCooldown(5);
            }
            else if (_transparent)
            {
                _transparent = false;
                _spriteRenderer.color += new Color(0, 0, 0, 0.5f);
                SkillCooldown[5] /= 5;
            }
        }

        // Skill Cooldown...
        for (int i = 0; i < SkillCooldown.Length; i++)
        {
            if (SkillCooldown[i] > 0) SkillCooldown[i] -= Time.deltaTime;
        }
        
        if (_isAttacking)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                _isAttacking = false;
            }
            else
            {
                if (!_dash) _rigidbody.velocity = Vector2.zero;
            }
        }

    }

    private void FixedUpdate()
    {
        _velocity = Vector2.zero;

        if (hp <= 0)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        
        if (!_isAttacking)
        {
            // Move
            if (Input.GetKey(KeyCode.RightArrow)) _velocity += Vector2.right;
            if (Input.GetKey(KeyCode.LeftArrow)) _velocity -= Vector2.right;
            if (Input.GetKey(KeyCode.UpArrow)) _velocity += Vector2.up;
            if (Input.GetKey(KeyCode.DownArrow)) _velocity -= Vector2.up;

            _velocity = _direction = _velocity.normalized;
            _rigidbody.velocity = _velocity * speed;

            if (_spriteRenderer.flipX && _velocity.x > 0) _spriteRenderer.flipX = false;
            else if (!_spriteRenderer.flipX && _velocity.x < 0) _spriteRenderer.flipX = true;

            // Animation
            if (_velocity.magnitude == 0) _animator.SetBool("walk", false);
            else
            {
                _animator.SetBool("walk", true);
                if (_velocity.x != 0) _animator.SetFloat("direction", 0.5f);
                else if (_velocity.y > 0) _animator.SetFloat("direction", 1);
                else _animator.SetFloat("direction", 0);
            }
        }
    }

    private void LevelUp()
    {
        level++;
        maxExp += 100;
        maxHp += 20;
        power += 2;
        defense += 2;
        PlayerDataManager.Instance.npcData1._level++;
        PlayerDataManager.Instance.npcData2._level++;
    }

    private void SetCooldown(int index)
    {
        SkillCooldown[index] = GetCooldown(index);
    }

    public float GetCooldown(int index)
    {
        return skillInfos.skillCooldown[index]
               - skillInfos.cooldownDecrement[index] * SkillLevels[index];
    }

    private void Interaction()
    {
        if (_animator.GetFloat("direction") == 1) _interactionDir = Vector2.up; // up
        else if (_animator.GetFloat("direction") == 0) _interactionDir = Vector2.down; // down
        else if (_spriteRenderer.flipX) _interactionDir = Vector2.left; // left
        else _interactionDir = Vector2.right; // right
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, _interactionDir, 1);

        if (hits.Length != 0)
        {
            foreach (var hit in hits)
            {
                if (hit.collider.tag == "Object")
                {
                    Interactable interatable = hit.collider.GetComponent<Interactable>();
                    if (interatable != null) interatable.Interact();
                }

                if (hit.collider.tag == "Monster" && !_isAttacking)
                {
                    var mon = hit.collider.GetComponent<Monster>();
                    if (mon.trapped && SkillCooldown[3] <= 0 && SkillLevels[3] > 0)
                    {
                        StartCoroutine(RemoveTrap(mon));
                    } else if (mon.currentHealth <= mon.data._health * 0.2f )
                    {
                        SetCooldown(1);
                        CollectMonster(mon.data);
                        Destroy(mon.gameObject);
                    }
                }
            }
        }
    }

    private void CollectMonster(MonsterData monster)
    {
        exp += monster._expGiven;
        MonsterCollectionManager.Instance.CollectMonster(monster);
    }

    // AfterImage Effect
    private void SpawnTrail()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer tpr = trailPart.AddComponent<SpriteRenderer>();
        tpr.sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        tpr.sortingLayerName = "Player";
        tpr.flipX = _spriteRenderer.flipX;
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
    
    private IEnumerator Dash()
    {
        _isAttacking = true;
        _dash = true;
        _rigidbody.velocity = _direction * speed * 3;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.03f);
            SpawnTrail();
        }
        _isAttacking = false;
        _dash = false;
    }
    private IEnumerator Guard()
    {
        _isAttacking = true;
        _guard = true;

        yield return new WaitForSeconds(0.5f);
        
        _isAttacking = false;
        _guard = false;
    }

    private IEnumerator RemoveTrap(Monster monster)
    {
        _isAttacking = true;
        _removeTrap = true;

        yield return new WaitForSeconds(1 - skillInfos.abilityIncrement[3] * level);

        if (_removeTrap)
        {
            CollectMonster(monster.data);
            Destroy(monster.gameObject);
            SetCooldown(3);
            _removeTrap = false;
            _isAttacking = false;
            PlayerDataManager.Instance.suspicion += 10 * PlayerDataManager.Instance.eyeNum;
        }
    }
    private IEnumerator Concentration()
    {
        _isAttacking = true;
        for (int i = 0; i<5; i++)
        {
            hp += 10 + SkillLevels[4] * skillInfos.abilityIncrement[4];
            yield return new WaitForSeconds(1f);
        } 
        _isAttacking = false;
    }
    
    private IEnumerator Transparent()
    {
        _transparent = true;
        _spriteRenderer.color -= new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(20 + skillInfos.abilityIncrement[5]);
        if (_transparent)
        {
            _transparent = false;
            _spriteRenderer.color += new Color(0, 0, 0, 0.5f);
        }
    }
    
    // HIT
    public void Hit(float damage, Vector2 knockback, float backTime)
    {
        if (_invincibilityTime > 0) return;
        hp -= (int) damage;
        _rigidbody.velocity = knockback;
        _knockbackTime = backTime;
        _invincibilityTime = 1f;
        StartCoroutine(HitSparkle());
        
        if (_removeTrap)
        {
            SetCooldown(3);
            _removeTrap = false;
            _isAttacking = false;
        }

        if (hp <= 0) Die();
    }
    public void Die()
    {
        _isAttacking = false;
    }

    IEnumerator HitSparkle()
    {
        Color color = _spriteRenderer.color;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.08f);
            if (i%2 == 0) _spriteRenderer.color -= new Color(0, 0, 0, 1);
            else _spriteRenderer.color += new Color(0, 0, 0, 1);
        }

        _spriteRenderer.color = color;
    }
}
