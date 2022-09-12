using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;
using UnityEngine;

/// <summary>
/// ·���е�һ��ֱ�߶Σ�һ��·���ɶ��RouteSegmentƴ�Ӷ���
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
	/// ����·��
	/// </summary>
	public ObjectReference Route { get; }

	/// <summary>
	/// ·�߶�����ֱ���������ֵ�����IsHorizontal����·�߶�����ֱ����
	/// </summary>
	public float Axis { get; }

	/// <summary>
	/// ·�߶��Ƿ�ˮƽ��Ϊfalseʱ��ʾ��ֱ·�߶�
	/// </summary>
	public bool IsHorizontal { get; }

	/// <summary>
	/// ·�߶����ӵĽڵ��б�
	/// </summary>
	List<ObjectReference> ConnectedNodes => connectedNodes ?? (connectedNodes = new List<ObjectReference>());
	List<ObjectReference> connectedNodes;

	/// <summary>
	/// ·�߶����ӵ�����·�߶��б�
	/// </summary>
	List<ObjectReference> ConnectedSegments => connectedSegments ?? (connectedSegments = new List<ObjectReference>());
	List<ObjectReference> connectedSegments;

	/// <summary>
	/// ·�߶��Ƿ�����ָ���ڵ�
	/// </summary>
	/// <param name="node">�ڵ�</param>
	/// <returns>�Ƿ����ӽڵ�</returns>
	public bool IsConnectedToNode(ObjectReference node) => ConnectedNodes.Contains(node);

	/// <summary>
	/// ����·�߶����ӵĽڵ�������·�߶�
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
			// ·�߶�����һ���ڵ�����һ��·�߶�
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
			// ·�߶����������ڵ�
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
			// ·�߶���������·�߶�
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
