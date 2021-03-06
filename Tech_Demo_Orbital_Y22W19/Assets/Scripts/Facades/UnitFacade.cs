using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem.Entities;

namespace Facades
{
    public class UnitFacade : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth = 1,
            currentHealth = 1,
            range,
            attack,
            defence,
            risk,
            maxActionPoints,
            maxSkillPoints,
            currentSkillPoints;

        [SerializeField]
        private string basicSkillName;

        [SerializeField]
        private string ultimateSkillName;

        [SerializeField]
        private Unit.RiskCalculationMethod riskCalculationMethod;

        [SerializeField]
        private Unit.UnitFaction faction;

        [SerializeField]
        private Sprite avatar;

        [SerializeField]
        private int startingTime = 0;

        public Sprite CharacterAvatar => avatar;

        public Unit CreateUnit()
        {
            UnitProperties properties = new UnitProperties(
                maxHealth,
                currentHealth,
                defence,
                attack,
                range,
                maxActionPoints,
                maxSkillPoints, 
                currentSkillPoints);

            Unit unit = Unit.CreateNewUnit(
                gameObject.name, 
                properties, 
                0, 
                faction, 
                risk, 
                riskCalculationMethod, 
                basicSkillName, 
                ultimateSkillName);

            unit = unit.ChangeTime(startingTime);

            return unit;
        }
    }
}
