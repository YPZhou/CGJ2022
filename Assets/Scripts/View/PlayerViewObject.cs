using UnityEngine;

public class PlayerViewObject : BaseViewObject<PlayerData>
{
    [SerializeField]
    private Vector2 nodeOffset;

	protected override void Update()
	{
        if (CoreObject?.CurrentNode.Object is NodeData nodeData)
        {
            if (nodeData.HasAnySelectedRoute)
			{
                transform.position = nodeData.Position + nodeOffset + new Vector2(0f, Mathf.Sin(Time.realtimeSinceStartup * 10f) * 0.3f);
                //transform.localScale = new Vector3(Mathf.Sin(Time.realtimeSinceStartup * 5f) * 0.02f + 1f, Mathf.Sin(Time.realtimeSinceStartup * 5f) * 0.02f + 1f, 1f);
            }
            else
			{
                transform.position = nodeData.Position + nodeOffset;
                //transform.localScale = Vector3.one;
            }
            transform.localScale = new Vector3(Mathf.Sin(Time.realtimeSinceStartup * 5f) * 0.02f + 1f, Mathf.Sin(Time.realtimeSinceStartup * 5f) * 0.02f + 1f, 1f);
        }
    }
}