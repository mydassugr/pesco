
using System;
using Gtk;


namespace pesco
{


	public class BotonObjetoClasificable : ToggleButton
	{
		string resourceName;
		public string ResourceName {
			get {
				return resourceName;
			}
		}
		
		public BotonObjetoClasificable(string r)
		{
			resourceName = r;
			Gtk.Image image = Gtk.Image.LoadFromResource(r);
		    ModifyBg(StateType.Active, new Gdk.Color(0x1c, 0x0c, 0xf9));
            ModifyBg(StateType.Prelight, new Gdk.Color(0xd0, 0xcf, 0xed));
            Image = image;
        }
	}
}
