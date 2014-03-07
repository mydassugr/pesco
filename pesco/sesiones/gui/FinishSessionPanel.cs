using System;
namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class FinishSessionPanel : Gtk.Bin
	{
		public FinishSessionPanel (int totalGold, int totalSilver, int totalBronze)
		{
			this.Build ();
			
			uint fila = 0;
			uint columna = 2;
			
			label6.ModifyFg(Gtk.StateType.Normal, new Gdk.Color(0xff, 0x99,0));
			label9.ModifyFg(Gtk.StateType.Normal, new  Gdk.Color(0x62, 0x62, 0x62));
			label10.ModifyFg(Gtk.StateType.Normal,new Gdk.Color(0xb4, 0x5f, 0x04));
			//GtkUtil.SetStyle(label5, Configuration.Current.BubbleStyle);
			label5.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 14"));
			// add gold medals
			for ( int i=0; i < totalGold; i++, columna++)
			{
				medalsTable.Attach(new Gtk.Image(Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.figures.oro_peque.png")), columna, columna+1, fila, fila+1);
			}
			
			// add silver medals
			fila++;
			columna = 2;		
			for (  int i=0; i < totalSilver; i++, columna++)
			{
				medalsTable.Attach(new Gtk.Image(Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.figures.plata_peque.png")), columna, columna+1, fila, fila+1);
			}
			
			// add gold medals
			fila++;
			columna = 2;
			for (int i=0; i < totalBronze; i++, columna++)
			{
				medalsTable.Attach(new Gtk.Image(Gdk.Pixbuf.LoadFromResource("pesco.ejercicios.figures.bronze_peque.png")), columna, columna+1, fila, fila+1);
			}
			
			GtkUtil.SetStyle(button7, Configuration.Current.ButtonFont);
			
			button7.Clicked += delegate(object sender, EventArgs e) {
				SessionManager.GetInstance().FinishTimer();
			
				Gtk.Application.Quit ();
				//e.RetVal = false;
			};
			
			medalsTable.ShowAll();
			this.ShowAll();
		}
	}
}

