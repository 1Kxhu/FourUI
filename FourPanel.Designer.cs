using FourUI;
using System.ComponentModel;
using System.Windows.Forms.Design;

public class FourPanelDesigner : ControlDesigner
{
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);
        var panel = (FourPanel)component;

        EnableDesignMode(panel, "FourPanelContent");
    }
}
