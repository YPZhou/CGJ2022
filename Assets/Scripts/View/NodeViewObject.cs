using UnityEngine;
using static Constants;

public class NodeViewObject : BaseViewObject<NodeData>
{

	[SerializeField]
    Sprite NormalNodeSprite;

	[SerializeField]
    Sprite StartNodeSprite;

	[SerializeField]
    Sprite GoalNodeSprite;

	protected override void Start()
	{
        if (CoreObject != null)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (CoreObject.NodeType == NodeType.Normal)
                {
                    spriteRenderer.sprite = NormalNodeSprite;
                }
                else if (CoreObject.NodeType == NodeType.Start)
				{
                    spriteRenderer.sprite = StartNodeSprite;
				}
                else if (CoreObject.NodeType == NodeType.Goal)
				{
                    spriteRenderer.sprite = GoalNodeSprite;
				}
            }

            transform.position = CoreObject.Position;
        }
    }
}