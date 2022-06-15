using CombatSystem.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class HealthBarManager : MonoBehaviour
    {
        private static HealthBarManager instance;
        public static HealthBarManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<HealthBarManager>();
                    Debug.Assert(instance != null, "There is no HealthBarManager in the scene, consider adding one");
                }
                return instance;
            }
        }

        [SerializeField]
        private GameObject healthBarPrefab;

        [SerializeField]
        private RectTransform healthBarCollection;

        private List<BarFillBehaviour> healthBars = new List<BarFillBehaviour>() { null };

        public void InitialiseHealthBars(IEnumerable<Unit> units)
        {
            foreach (Unit unit in units) 
            {
                GameObject healthBar = Instantiate(healthBarPrefab, healthBarCollection);
                BarFillBehaviour barFillBehaviour = healthBar.GetComponent<BarFillBehaviour>();
                barFillBehaviour.SetParent(UnitManager.Instance.GetGameObjectOfUnit(unit).transform);
                healthBars.Add(barFillBehaviour);
            }
        }

        public BarFillBehaviour GetHealthBar(Unit unit)
        {
            return healthBars[unit.Identity];
        }
    }
}