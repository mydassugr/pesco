
// This file has been generated by the GUI designer. Do not modify.
namespace pesco
{
	public partial class PanelPyramidsBoard
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Label labelPyramidsSelected;
		private global::Gtk.Alignment alignment1;
		private global::Gtk.Label labelTimeLeft;
		private global::Gtk.Fixed fixedLayout;
		private global::Gtk.Alignment alignment2;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget pesco.PanelPyramidsBoard
			global::Stetic.BinContainer.Attach (this);
			this.Name = "pesco.PanelPyramidsBoard";
			// Container child pesco.PanelPyramidsBoard.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.labelPyramidsSelected = new global::Gtk.Label ();
			this.labelPyramidsSelected.Name = "labelPyramidsSelected";
			this.hbox1.Add (this.labelPyramidsSelected);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.labelPyramidsSelected]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.alignment1 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment1.Name = "alignment1";
			this.hbox1.Add (this.alignment1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.alignment1]));
			w2.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.labelTimeLeft = new global::Gtk.Label ();
			this.labelTimeLeft.Name = "labelTimeLeft";
			this.labelTimeLeft.Justify = ((global::Gtk.Justification)(1));
			this.hbox1.Add (this.labelTimeLeft);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.labelTimeLeft]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox1]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.fixedLayout = new global::Gtk.Fixed ();
			this.fixedLayout.Name = "fixedLayout";
			this.fixedLayout.HasWindow = false;
			this.vbox2.Add (this.fixedLayout);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.fixedLayout]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.alignment2 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment2.Name = "alignment2";
			this.vbox2.Add (this.alignment2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.alignment2]));
			w6.Position = 2;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}