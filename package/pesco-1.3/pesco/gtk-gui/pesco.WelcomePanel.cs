
// This file has been generated by the GUI designer. Do not modify.
namespace pesco
{
	public partial class WelcomePanel
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Image imageBackground;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget pesco.WelcomePanel
			global::Stetic.BinContainer.Attach (this);
			this.Name = "pesco.WelcomePanel";
			// Container child pesco.WelcomePanel.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.imageBackground = new global::Gtk.Image ();
			this.imageBackground.Name = "imageBackground";
			this.hbox1.Add (this.imageBackground);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.imageBackground]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox1]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
