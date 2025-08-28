using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public int Level { get; private set; }
    public int Exp { get; private set; }
    public float AttackDmg { get; private set; }
    public float Armor { get; private set; }
    public float Health { get; private set; }
    public float CriticalRate { get; private set; }

    public Character(int level, int exp, float attackDmg, float armor, float health, float criticalRate)
    {
        Level = level;
        Exp = exp;
        AttackDmg = attackDmg;
        Armor = armor;
        Health = health;
        CriticalRate = criticalRate;
    }

    public void AddAttack(float attack)
    {
        AttackDmg += attack;
    }
    
    public void AddArmor(float armor)
    {
        Armor += armor;
    }
    
    public void AddHealth(float health)
    {
        Health += health;
    }
    
    public void AddCriticalRate(float criticalRate)
    {
        CriticalRate += criticalRate;
    }
    
}
