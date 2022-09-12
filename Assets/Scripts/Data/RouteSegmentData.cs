using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;
using UnityEngine;

/// <summary>
/// 路线中的一个直线段，一条路线由多个RouteSegment拼接而成
/// </summary>
public class RouteSegmentData : BaseObject
{
	public RouteSegmentData(uint id, ObjectReference route, bool isHorizontal, float axis)
		: base(id)
	{
		Route = route;
		IsHorizontal = isHorizontal;
		Axis = axis;
	}

	/// <summary>
	/// 所属路线
	/// </summary>
	public ObjectReference Route { get; }

	/// <summary>
	/// 路线段所在直线轴的坐标值，配合IsHorizontal决定路线段所在直线轴
	/// </summary>
	public float Axis { get; }

	/// <summary>
	/// 路线段是否水平，为false时表示竖直路线段
	/// </summary>
	public bool IsHorizontal { get; }

	/// <summary>
	/// 路线段连接的节点列表
	/// </summary>
	List<ObjectReference> ConnectedNodes => connectedNodes ?? (connectedNodes = new List<ObjectReference>());
	List<ObjectReference> connectedNodes;

	/// <summary>
	/// 路线段连接的其他路线段列表
	/// </summary>
	List<ObjectReference> ConnectedSegments => connectedSegments ?? (connectedSegments = new List<ObjectReference>());
	List<ObjectReference> connectedSegments;

	/// <summary>
	/// 路线段是否连接指定节点
	/// </summary>
	/// <param name="node">节点</param>
	/// <returns>是否连接节点</returns>
	public bool IsConnectedToNode(ObjectReference node) => ConnectedNodes.Contains(node);

	/// <summary>
	/// 设置路线段连接的节点与其他路线段
	/// </summary>
	/// <param name="connected"></param>
	public void SetConnected(IEnumerable<ObjectReference> connected)
	{
		ParseConnected(connected);
		CalculateSegmentPositions();
	}

	void ParseConnected(IEnumerable<ObjectReference> connected)
	{
		if (connected.Count() == 2)
		{
			foreach (var connectedItem in connected)
			{
				if (connectedItem.Type == typeof(NodeData))
				{
					ConnectedNodes.Add(connectedItem);
				}
				else if (connectedItem.Type == typeof(RouteSegmentData))
				{
					ConnectedSegments.Add(connectedItem);
				}
			}
		}
	}

	public List<Vector2> SegmentPositions => segmentPositions ?? (segmentPositions = new List<Vector2>());
	List<Vector2> segmentPositions;

	void CalculateSegmentPositions()
	{
		SegmentPositions.Clear();

		var firstNode = ConnectedNodes.FirstOrDefault();
		var firstSegment = ConnectedSegments.FirstOrDefault();

		if (firstNode.IsValid && firstSegment.IsValid)
		{
			// 路线段连接一个节点与另一个路线段
			if (IsHorizontal)
			{
				SegmentPositions.Add(new Vector2((firstNode.Object as NodeData).Position.x, Axis));
				SegmentPositions.Add(new Vector2((firstSegment.Object as RouteSegmentData).Axis, Axis));
			}
			else
			{
				SegmentPositions.Add(new Vector2(Axis, (firstNode.Object as NodeData).Position.y));
				SegmentPositions.Add(new Vector2(Axis, (firstSegment.Object as RouteSegmentData).Axis));
			}
		}
		else if (firstNode.IsValid)
		{
			// 路线段连接两个节点
			var secondNode = ConnectedNodes.Skip(1).FirstOrDefault();
			if (secondNode.IsValid)
			{
				if (IsHorizontal)
				{
					SegmentPositions.Add(new Vector2((firstNode.Object as NodeData).Position.x, Axis));
					SegmentPositions.Add(new Vector2((secondNode.Object as NodeData).Position.x, Axis));
				}
				else
				{
					SegmentPositions.Add(new Vector2(Axis, (firstNode.Object as NodeData).Position.y));
					SegmentPositions.Add(new Vector2(Axis, (secondNode.Object as NodeData).Position.y));
				}
			}
		}
		else if (firstSegment.IsValid)
		{
			// 路线段连接两个路线段
			var secondSegment = ConnectedSegments.Skip(1).FirstOrDefault();
			if (secondSegment.IsValid)
			{
				if (IsHorizontal)
				{
					SegmentPositions.Add(new Vector2((firstSegment.Object as RouteSegmentData).Axis, Axis));
					SegmentPositions.Add(new Vector2((secondSegment.Object as RouteSegmentData).Axis, Axis));
				}
				else
				{
					SegmentPositions.Add(new Vector2(Axis, (firstSegment.Object as RouteSegmentData).Axis));
					SegmentPositions.Add(new Vector2(Axis, (secondSegment.Object as RouteSegmentData).Axis));
				}
			}
		}
	}
}
