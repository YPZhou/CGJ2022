using System.Collections.Generic;
using BitButterCORE.V2;
using static Constants;

/// <summary>
/// 警卫类，表示路线的通过条件
/// </summary>
public class GuardData : BaseObject
{
	public GuardData(uint id, ObjectReference route)
		: base(id)
	{
		Route = route;
	}

	/// <summary>
	/// 警卫所处路线
	/// </summary>
	public ObjectReference Route { get; }

	/// <summary>
	/// 添加检查项
	/// </summary>
	/// <param name="lootCheck"></param>
	public void AddLootCheck(LootCheck lootCheck)
	{
		LootChecks.Add(lootCheck);
	}

	/// <summary>
	/// 检查项列表
	/// </summary>
	public List<LootCheck> LootChecks => lootChecks ?? (lootChecks = new List<LootCheck>());
	List<LootCheck> lootChecks;
}

/// <summary>
/// 警卫检查项
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
	/// 检查背包位置
	/// </summary>
	public InventoryPosition LootPosition { get; }

	/// <summary>
	/// 检查图案
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
