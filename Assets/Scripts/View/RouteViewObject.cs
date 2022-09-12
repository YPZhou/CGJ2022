using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RouteViewObject : BaseViewObject<RouteData>
{
	[SerializeField]
	Text RemainingPassCount;

	protected override void Start()
	{
		var canvasParent = GameObject.Find("Canvas");
		if (canvasParent != null)
		{
			transform.SetParent(canvasParent.transform, false);
		}

		var connectedNodes = CoreObject.Nodes.Select(node => node.Object as NodeData);
		var nodesMidPoint = new Vector2();
		foreach (var node in connectedNodes)
		{
			nodesMidPoint += node.Position;
		}
		nodesMidPoint /= connectedNodes.Count();

		var routeSegments = CoreObject.RouteSegments;
		if (routeSegments.Count() % 2 == 0)
		{
			transform.position = Camera.main.WorldToScreenPoint(nodesMidPoint + new Vector2(-2, 2));
		}
		else
		{
			var midSegment = routeSegments.ElementAt(routeSegments.Count() / 2);
			var offset = (midSegment.Object as RouteSegmentData).IsHorizontal ? new Vector2(0, 2) : new Vector2(-2, 0);
			transform.position = Camera.main.WorldToScreenPoint(nodesMidPoint + offset);
		}
	}

	protected override void Update()
	{
		if (RemainingPassCount != null)
		{
			var baseFontSize = 32;
			RemainingPassCount.text = (CoreObject.MaxPassCount - CoreObject.RoutePassedCount).ToString();

			if (CoreObject.IsSelected)
			{
				//RemainingPassCount.color = new Color(1f, 0.3f, 0.5f);
				RemainingPassCount.color = Color.gray;
				RemainingPassCount.fontSize = Mathf.FloorToInt(Mathf.Sin(Time.realtimeSinceStartup * 10f) * 5 + baseFontSize + 8);
				RemainingPassCount.fontStyle = FontStyle.Bold;
			}
			else
			{
				//RemainingPassCount.color = new Color(0.8f, 0.3f, 0.5f);
				RemainingPassCount.color = Color.gray;
				RemainingPassCount.fontSize = baseFontSize;
				RemainingPassCount.fontStyle = FontStyle.Normal;
			}
		}
	}
}