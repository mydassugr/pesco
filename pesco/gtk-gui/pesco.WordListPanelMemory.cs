
// This file has been generated by the GUI designer. Do not modify.
namespace pesco
{
	public partial class WordListPanelMemory
	{
		private global::Gtk.HBox hbox5;
		private global::Gtk.Alignment alignment5;
		private global::Gtk.VBox vbox4;
		private global::Gtk.Alignment alignment7;
		private global::Gtk.Frame frame3;
		private global::Gtk.Alignment GtkAlignment;
		private global::Gtk.VBox vbox5;
		private global::Gtk.Label label1;
		private global::Gtk.HBox hbox6;
		private global::Gtk.Alignment alignment8;
		private global::Gtk.VBox vboxCenter;
		private global::Gtk.Alignment alignment1;
		private global::Gtk.VBox vbox6;
		private global::Gtk.Fixed fixed5;
		private global::Gtk.Image image235;
		private global::Gtk.Alignment alignment2;
		private global::Gtk.Alignment alignment9;
		private global::Gtk.Alignment alignment6;
		private global::Gtk.Alignment alignment4;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget pesco.WordListPanelMemory
			global::Stetic.BinContainer.Attach (this);
			this.Name = "pesco.WordListPanelMemory";
			// Container child pesco.WordListPanelMemory.Gtk.Container+ContainerChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.alignment5 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment5.Name = "alignment5";
			this.hbox5.Add (this.alignment5);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.alignment5]));
			w1.Position = 0;
			// Container child hbox5.Gtk.Box+BoxChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.alignment7 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment7.Name = "alignment7";
			this.vbox4.Add (this.alignment7);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.alignment7]));
			w2.Position = 0;
			// Container child vbox4.Gtk.Box+BoxChild
			this.frame3 = new global::Gtk.Frame ();
			this.frame3.WidthRequest = 1250;
			this.frame3.HeightRequest = 660;
			this.frame3.Name = "frame3";
			this.frame3.LabelYalign = 0F;
			// Container child frame3.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.vbox5 = new global::Gtk.VBox ();
			this.vbox5.Name = "vbox5";
			this.vbox5.Spacing = 6;
			// Container child vbox5.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("<big><big><big><big><b>Memoriza las Palabras</b></big></big></big></big>");
			this.label1.UseMarkup = true;
			this.vbox5.Add (this.label1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.label1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.alignment8 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment8.Name = "alignment8";
			this.hbox6.Add (this.alignment8);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.alignment8]));
			w4.Position = 0;
			// Container child hbox6.Gtk.Box+BoxChild
			this.vboxCenter = new global::Gtk.VBox ();
			this.vboxCenter.Name = "vboxCenter";
			this.vboxCenter.Spacing = 6;
			// Container child vboxCenter.Gtk.Box+BoxChild
			this.alignment1 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment1.Name = "alignment1";
			this.vboxCenter.Add (this.alignment1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vboxCenter [this.alignment1]));
			w5.Position = 0;
			// Container child vboxCenter.Gtk.Box+BoxChild
			this.vbox6 = new global::Gtk.VBox ();
			this.vbox6.Name = "vbox6";
			this.vbox6.Spacing = 6;
			// Container child vbox6.Gtk.Box+BoxChild
			this.fixed5 = new global::Gtk.Fixed ();
			this.fixed5.Name = "fixed5";
			this.fixed5.HasWindow = false;
			// Container child fixed5.Gtk.Fixed+FixedChild
			this.image235 = new global::Gtk.Image ();
			this.image235.Name = "image235";
			this.image235.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("pesco.ejercicios.listaRecados.gui.notebook_paper.png");
			this.fixed5.Add (this.image235);
			this.vbox6.Add (this.fixed5);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.fixed5]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			this.vboxCenter.Add (this.vbox6);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vboxCenter [this.vbox6]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vboxCenter.Gtk.Box+BoxChild
			this.alignment2 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment2.Name = "alignment2";
			this.vboxCenter.Add (this.alignment2);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vboxCenter [this.alignment2]));
			w9.Position = 2;
			this.hbox6.Add (this.vboxCenter);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.vboxCenter]));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.alignment9 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment9.Name = "alignment9";
			this.hbox6.Add (this.alignment9);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.alignment9]));
			w11.PackType = ((global::Gtk.PackType)(1));
			w11.Position = 2;
			this.vbox5.Add (this.hbox6);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.hbox6]));
			w12.Position = 1;
			this.GtkAlignment.Add (this.vbox5);
			this.frame3.Add (this.GtkAlignment);
			this.vbox4.Add (this.frame3);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.frame3]));
			w15.Position = 1;
			// Container child vbox4.Gtk.Box+BoxChild
			this.alignment6 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment6.Name = "alignment6";
			this.vbox4.Add (this.alignment6);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.alignment6]));
			w16.PackType = ((global::Gtk.PackType)(1));
			w16.Position = 2;
			this.hbox5.Add (this.vbox4);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.vbox4]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.alignment4 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment4.Name = "alignment4";
			this.hbox5.Add (this.alignment4);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.alignment4]));
			w18.PackType = ((global::Gtk.PackType)(1));
			w18.Position = 2;
			this.Add (this.hbox5);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
