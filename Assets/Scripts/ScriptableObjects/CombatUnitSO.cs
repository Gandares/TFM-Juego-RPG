using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Scriptable Objects/Combat/Unit")]
public class CombatUnitSO : ScriptableObject
{
    public string unitName;
    public int level;

    public float expBase;
    public float Exp;
    public float MaxExp;

    public float baseHP;
    public float maxHP;
    public float currentHP;

    public float baseMana;
    public float maxMana;
    public float currentMana;

    public float attackPowerBase;
    public float attackPower;
    public float healingPowerBase;
    public float healingPower;
    public int moneyPerDefeat;
    public float expPerDefeat;

    private float damagePercent = 0.8f;
    public bool levelUp = false;

    public GameObject unitPrefab;



    public void AttackUnit(CombatUnitSO other, float extraDamage, bool usedMagic, float manaCost)
    {
        if (!usedMagic)
        {
            float minim = (this.attackPower * damagePercent) + extraDamage;
            other.TakeDamage(Random.Range(minim,this.attackPower+extraDamage));
        }
        else
        {
            other.TakeDamage(extraDamage);
            LessMana(manaCost);
        }
    }

    public void TakeDamage(float damage)
    {
        this.currentHP -= damage;

        if (this.currentHP < 0f)
            this.currentHP = 0f;
    }

    public void LessMana(float manaCost)
    {
        this.currentMana -= manaCost;
    }

    public void Heal(float heal)
    {
        this.currentHP += heal;

        if (this.currentHP > this.maxHP)
            this.currentHP = this.maxHP;
    }

    public void ResetHP()
    {
        this.currentHP = this.maxHP;
    }

    public void ResetMana()
    {
        this.currentMana = this.maxMana;
    }

    public void GainXP(float experience)
    {
        this.Exp += experience;
        if (this.Exp >= this.MaxExp)
        {
            levelUp = true;
            float expRestante = this.Exp - this.MaxExp;
            LevelUP();
            this.Exp += expRestante;
        }
    }

    public void LevelUP()
    {
        this.level++;
        this.maxHP = this.baseHP + (this.baseHP * 0.2f * this.level);
        this.maxMana = this.baseMana + (this.baseMana * 0.2f * this.level);
        this.attackPower = this.attackPowerBase + (this.attackPowerBase * 0.2f * this.level);
        this.healingPower = this.healingPowerBase + (this.healingPowerBase * 0.2f * this.level);
        this.MaxExp = this.expBase + (this.expBase * 0.2f * this.level);
        this.Exp = 0;
    }

    private void OnDisable()
    {
        this.maxHP = this.baseHP;
    }
}
