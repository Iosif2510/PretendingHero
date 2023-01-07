using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PlayerSkillInfos skillInfos;

    // Temp
    public float hp, maxHp;
    public float exp, maxExp;
    public int level;

    public int skillPoint;
    
    private float[] _skillLevels;
    private float[] _skillCooldown;
    public float[] SkillLevels => _skillLevels;
    public float[] SkillCooldown => _skillCooldown;
    // 
    
    
    private Vector2 _velocity;
    private Vector2 _direction;
    private Vector2 _interactionDir;
    
    private bool _isAttacking;
    private bool _dash;
    private bool _guard;
    private bool _transparent;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public void AddSkillLevel(int index)
    {
        if (skillPoint == 0) return;
        _skillLevels[index]++;
        skillPoint--;
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _skillLevels = new float[skillInfos.skillCooldown.Length];
        _skillCooldown = new float[skillInfos.skillCooldown.Length];

        _transparent = false;

        hp = maxHp;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        hp = Mathf.Clamp(hp, 0, maxHp);
        exp = Mathf.Clamp(exp, 0, maxExp);

        if (exp >= maxExp)
        {
            exp = 0;
            level++;
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
        if (Input.GetKeyDown(KeyCode.Q) && !_isAttacking && _direction.magnitude > 0 && _skillCooldown[0] <= 0
            && _skillLevels[0] > 0)
        {
            StartCoroutine("Dash");
            SetCooldown(0);
        }
        
        // Guard : skill index == 2
        if (Input.GetKeyDown(KeyCode.W) && !_isAttacking && _skillCooldown[2] <= 0
            && _skillLevels[2] > 0)
        {
            StartCoroutine("Guard");
            SetCooldown(2);
        }
        
        // Concentration : skill index == 4
        if (Input.GetKeyDown(KeyCode.E) && !_isAttacking && _skillCooldown[4] <= 0
            && _skillLevels[4] > 0)
        {
            StartCoroutine("Concentration");
            SetCooldown(4);
        }
        
        // Transparent : skill index == 5
        if (Input.GetKeyDown(KeyCode.R) && !_isAttacking
                                        && _skillLevels[5] > 0)
        {
            if (!_transparent && _skillCooldown[5] <= 0)
            {
                StartCoroutine("Transparent");
                SetCooldown(5);
            }
            else if (_transparent)
            {
                _transparent = false;
                _spriteRenderer.color += new Color(0, 0, 0, 0.5f);
                _skillCooldown[5] /= 5;
            }
        }

        // Skill Cooldown...
        for (int i = 0; i < _skillCooldown.Length; i++)
        {
            if (_skillCooldown[i] > 0) _skillCooldown[i] -= Time.deltaTime;
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

    private void SetCooldown(int index)
    {
        _skillCooldown[index] = GetCooldown(index);
    }

    public float GetCooldown(int index)
    {
        return skillInfos.skillCooldown[index]
               - skillInfos.cooldownDecrement[index] * _skillLevels[index];
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
            }
        }
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
    private IEnumerator Concentration()
    {
        _isAttacking = true;
        for (int i = 0; i<5; i++)
        {
            hp += 10 + _skillLevels[4] * skillInfos.abilityIncrement[4];
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
}
