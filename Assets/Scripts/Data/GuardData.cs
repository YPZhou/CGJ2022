using System.Collections.Generic;
using BitButterCORE.V2;
using static Constants;

/// <summary>
/// �����࣬��ʾ·�ߵ�ͨ������
/// </summary>
public class GuardData : BaseObject
{
	public GuardData(uint id, ObjectReference route)
		: base(id)
	{
		Route = route;
	}

	/// <summary>
	/// ��������·��
	/// </summary>
	public ObjectReference Route { get; }

	/// <summary>
	/// ��Ӽ����
	/// </summary>
	/// <param name="lootCheck"></param>
	public void AddLootCheck(LootCheck lootCheck)
	{
		LootChecks.Add(lootCheck);
	}

	/// <summary>
	/// ������б�
	/// </summary>
	public List<LootCheck> LootChecks => lootChecks ?? (lootChecks = new List<LootCheck>());
	List<LootCheck> lootChecks;
}

/// <summary>
/// ���������
/// </summary>
public struct LootCheck
{
	public LootCheck(InventoryPosition lootPosition, LootIcon lootIcon, bool shouldExclude)
	{
		LootPosition = lootPosition;
		LootIcon = lootIcon;
		ShouldExclude = shouldExclude;
	}

	/// <summary>
	/// ��鱳��λ��
	/// </summary>
	public InventoryPosition LootPosition { get; }

	/// <summary>
	/// ���ͼ��
	/// </summary>
	public LootIcon LootIcon { get; }

	public bool ShouldExclude { get; }

	public bool CanPassCheck(InventoryPosition lootPosition, LootIcon lootIcon)
	{
		var result = false;
		if (ShouldExclude)
		{
			result = LootPosition != lootPosition || LootIcon != lootIcon;
		}
		else
		{
			result = LootPosition == lootPosition && LootIcon == lootIcon;
		}
		return result;
	}
}
