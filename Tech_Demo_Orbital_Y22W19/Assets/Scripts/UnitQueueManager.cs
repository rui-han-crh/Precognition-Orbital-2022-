using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

/// <summary>
/// A specific structure that managed Unit turns
/// 1. A unit is OBSERVED to take the next turn 
/// 2. It moves a certain amount and incurs a COST
/// 3. It is shifted to the correct place in the structure
/// 
/// Unit will ONLY be removed from the structure if dead.
/// </summary>
public class UnitQueueManager : MonoBehaviour
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private GameObject characterHeadPrefab;
    [SerializeField]
    private RectTransform waitSlider;

    private bool isPlayingAnimation;
    public bool IsPlayingAnimation => isPlayingAnimation;


    public void InitialiseQueue(GameObject[] units, Dictionary<string, Unit> nameUnitMapping)
    {
        foreach (GameObject unit in units)
        {
            GameObject characterHead = Instantiate(characterHeadPrefab, content);
            characterHead.name = characterHeadPrefab.name + "_" + unit.name;
            characterHead.GetComponent<HeadAvatarBehaviour>().SetBoundGameObject(unit);
        }

        UpdateUnitQueue(nameUnitMapping);

    }

    public void UpdateUnitQueue(Dictionary<string, Unit> nameUnitMapping)
    {
        LinearAnimation linAnim = GetComponent<LinearAnimation>();

        List<LinearAnimation.LinearAnimationTarget> targets = new List<LinearAnimation.LinearAnimationTarget>();

        Unit[] orderedUnit = nameUnitMapping.Values.OrderBy(x => x.Time).ToArray();

        Dictionary<string, int> unitOrder = new Dictionary<string, int>();

        for (int i = 0; i< orderedUnit.Length; i++)
        {
            unitOrder.Add(orderedUnit[i].Name, i);
        }

        RectTransform characterHeadRectTransform = characterHeadPrefab.GetComponent<RectTransform>();
        Vector2 minOrigin = characterHeadRectTransform.anchorMin;
        Vector2 displacement = new Vector2(characterHeadRectTransform.anchorMax.x - characterHeadRectTransform.anchorMin.x, 0);

        foreach (Transform character in content)
        {
            HeadAvatarBehaviour characterHeadAvatar = character.GetComponent<HeadAvatarBehaviour>();

            if (!nameUnitMapping.ContainsKey(characterHeadAvatar.BoundGameObject.name))
            {
                Destroy(characterHeadAvatar.gameObject);
                continue;
            }

            characterHeadAvatar.UpdateHealthBar(nameUnitMapping[characterHeadAvatar.BoundGameObject.name]);

            int index = unitOrder[characterHeadAvatar.BoundGameObject.name];

            targets.Add(new LinearAnimation.LinearAnimationTarget(character.gameObject,
                                                                minOrigin + index * displacement,
                                                                minOrigin + (index + 1) * displacement + Vector2.up,
                                                                1));
            character.gameObject.SetActive(false);
        }

        SetWaitSliderLength(minOrigin.x, displacement.x * unitOrder.Count);

        StopAllCoroutines();
        isPlayingAnimation = true;
        linAnim.SetTargets(targets.ToArray());

        for (int i = 0; i < targets.Count; i++)
        {
            linAnim.ToggleUI(i);
        }
        new Task(CheckAllTargetsStopped(targets));
    }

    private IEnumerator CheckAllTargetsStopped(IEnumerable<LinearAnimation.LinearAnimationTarget> targets)
    {
        while(targets.Any(x => x.AnimationIsRunning))
        {
            yield return null;
        }
        isPlayingAnimation = false;
    }

    private void SetWaitSliderLength(float minX, float maxX)
    {
        waitSlider.anchorMin = new Vector2(minX, waitSlider.anchorMin.y);
        waitSlider.anchorMax = new Vector2(maxX, waitSlider.anchorMax.y);
    }
}
