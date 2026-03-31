using BaseLib.Abstracts;
using RedDwarf.Code.Extensions;
using Godot;

namespace RedDwarf.Code.Character;

public class RedDwarfPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => RedDwarf.Color;
    

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}