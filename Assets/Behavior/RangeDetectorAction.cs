using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Range Detector", story: "Update [Range_Detector] and assign [Player]", category: "Action", id: "72ed6af12a90e428d8feed946fe7400d")]
public partial class RangeDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<RangeDetector> Range_Detector;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnUpdate()
    {
        Player.Value = Range_Detector.Value.UpdateDetector();
        return Player.Value == null ? Status.Failure : Status.Success;
    }


}

