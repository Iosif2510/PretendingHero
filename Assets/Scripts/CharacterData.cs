using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    public string _name;
    public int _level;
    public int _power;
    public int _defense;
    public int _health;
    public int _speed;
    public float _attackDelay;
    public int _powerUp;
    public int _defenseUp;
    public int _healthUp;
}
