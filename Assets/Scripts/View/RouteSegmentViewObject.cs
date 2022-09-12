using System.Linq;
using UnityEngine;

public class RouteSegmentViewObject : BaseViewObject<RouteSegmentData>
{
	protected override void Start()
	{
		if (CoreObject.IsHorizontal)
		{
			transform.rotation = Quaternion.Euler(0f, 0f, 90f);
		}

		var segmentMidPoint = Vector2.zero;
		foreach (var position in CoreObject.SegmentPositions)
		{
			segmentMidPoint += position;
		}
		segmentMidPoint /= CoreObject.SegmentPositions.Count;
		transform.position = segmentMidPoint;

		var segmentLength = (CoreObject.SegmentPositions.First() - CoreObject.SegmentPositions.Last()).magnitude;
		transform.localScale = new Vector3(transform.localScale.x, segmentLength / 2.5f, 1);
	}

	protected override void Update()
	{
		var route = CoreObject.Route.Object as RouteData;
		if (route != null)
		{
			var spriteRenderer = GetComponent<SpriteRenderer>();
			if (spriteRenderer != null)
			{
				if (route.IsSelected)
				{
					//spriteRenderer.color = Color.green;
					spriteRenderer.color = new Color(0.6f, Mathf.Sin(Time.realtimeSinceStartup * 8f) * 0.05f + 0.95f, 0.6f, 0.8f);
				}
				else
				{
					spriteRenderer.color = Color.white;
				}
			}
		}
	}
}
