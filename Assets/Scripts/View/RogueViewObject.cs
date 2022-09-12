using UnityEngine;
using static Constants;

public class RogueViewObject : BaseViewObject<RogueData>
{
    /// <summary>
    /// 与节点位置的相对偏移量。
    /// </summary>
    [SerializeField]
    private Vector2 nodeOffset;

    protected override void Start()
    {
        if (CoreObject?.Node.Object is NodeData nodeData)
        {
            transform.position = nodeData.Position + nodeOffset;
            transform.localScale = new Vector3(0.35f, 0.35f, 1f);
        }

        animationOffset = Random.Range(0f, 100f);
    }

	protected override void Update()
	{
        var baseRotation = 0f;
        switch (CoreObject.Ability)
		{
            case RogueAbility.MoveDown:
                baseRotation = 180;
                break;
            case RogueAbility.MoveLeft:
                baseRotation = 90f;
                break;
            case RogueAbility.MoveRight:
                baseRotation = -90f;
                break;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.realtimeSinceStartup * 10 + animationOffset) * 10 + baseRotation);
	}

    float animationOffset;
}