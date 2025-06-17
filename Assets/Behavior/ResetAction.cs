using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.SceneManagement;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Reset",
    story: "[Player] is attacked by [Self]",
    category: "Action",
    id: "110a7fcf09dd833fc8fc3c9f6b40da85")]
public partial class ResetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    private const float delay = 3f;   // giữ 3 giây
    private float _startTime;

    protected override Status OnStart()
    {
        if (Player == null || Player.Value == null) return Status.Failure;
        if (Self == null || Self.Value == null) return Status.Failure;

        //--------------------------------------------------
        // 1. Khóa điều khiển
        //--------------------------------------------------
        if (Player.Value.TryGetComponent(out CharacterController cc))
            cc.enabled = false;

        if (Player.Value.TryGetComponent(out PlayerControllerScene2 pc))
            pc.enabled = false;

        //--------------------------------------------------
        // 2. Xoay thân + camera về phía Self
        //--------------------------------------------------
        Vector3 flatDir = Self.Value.transform.position - Player.Value.transform.position;
        flatDir.y = 0f;
        if (flatDir.sqrMagnitude > 0.0001f)
            Player.Value.transform.rotation = Quaternion.LookRotation(flatDir, Vector3.up);

        if (pc != null && pc.Camera != null)
            pc.Camera.localRotation = Quaternion.identity;

        //--------------------------------------------------
        // 3. Bắt đầu đếm thời gian
        //--------------------------------------------------
        _startTime = Time.time;

        // Giữ node ở trạng thái Running trong 3 giây
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Chờ đủ 3 giây rồi mới reload
        if (Time.time - _startTime >= delay)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd() { }
}
