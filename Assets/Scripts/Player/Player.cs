using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public float AttackDmg;
    public float Armor;
    public float Health;
    public float CriticalRate;

    public Character(float attackDmg, float armor, float health, float criticalRate)
    {
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
