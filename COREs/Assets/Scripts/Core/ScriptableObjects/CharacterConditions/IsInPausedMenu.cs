using UnityEngine;

[CreateAssetMenu(fileName = "IsInPausedMenu", menuName = "BehaviorCondition/IsInPausedMenu")]
public class IsInPausedMenu : CharacterBehaviorCondition
{
    [SerializeField]
    bool reverse = false;
    public override bool IsSatisfied(Character character)
    {
        return false;
    }
}