using BaseLib.Abstracts;
using BaseLib.Utils;
using RedDwarf.Code.Character;

namespace RedDwarf.Code.Potions;

[Pool(typeof(RedDwarfPotionPool))]
public abstract class RedDwarfPotion : CustomPotionModel;