
// This file has been generated by the GUI designer. Do not modify.
namespace pesco
{
	public partial class WordListCheckingPanel
	{
		private global::Gtk.HBox hbox1;
		private global::Gtk.Alignment alignment7;
		private global::Gtk.VBox vbox2;
		private global::Gtk.Alignment alignment4;
		private global::Gtk.Frame frame1;
		private global::Gtk.Alignment GtkAlignment;
		private global::Gtk.HBox hbox4;
		private global::Gtk.VBox vbox5;
		private global::Gtk.Image imagepepe;
		private global::Gtk.VBox vbox3;
		private global::Gtk.HBox hbox5;
		private global::Gtk.VBox vbox4;
		private global::Gtk.Label label2;
		private global::Gtk.Alignment alignment8;
		private global::Gtk.Label explanation;
		private global::Gtk.Alignment alignment3;
		private global::Gtk.HBox hbox6;
		private global::Gtk.VBox vbox1;
		private global::Gtk.Alignment alignment9;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Label label1;
		private global::Gtk.HSeparator hseparator1;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Alignment alignment2;
		private global::Gtk.VSeparator vseparator2;
		private global::Gtk.Button buttonPrevious;
		private global::Gtk.Button buttonNext;
		private global::Gtk.VSeparator vseparator1;
		private global::Gtk.Button botonListo;
		private global::Gtk.Alignment alignment5;
		private global::Gtk.Alignment alignment6;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget pesco.WordListCheckingPanel
			global::Stetic.BinContainer.Attach (this);
			this.Name = "pesco.WordListCheckingPanel";
			// Container child pesco.WordListCheckingPanel.Gtk.Container+ContainerChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.alignment7 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment7.Name = "alignment7";
			this.hbox1.Add (this.alignment7);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.alignment7]));
			w1.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.alignment4 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment4.Name = "alignment4";
			this.vbox2.Add (this.alignment4);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.alignment4]));
			w2.Position = 0;
			// Container child vbox2.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.WidthRequest = 1250;
			this.frame1.HeightRequest = 660;
			this.frame1.Name = "frame1";
			this.frame1.LabelYalign = 0F;
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.vbox5 = new global::Gtk.VBox ();
			this.vbox5.Name = "vbox5";
			this.vbox5.Spacing = 6;
			// Container child vbox5.Gtk.Box+BoxChild
			this.imagepepe = new global::Gtk.Image ();
			this.imagepepe.Name = "imagepepe";
			this.vbox5.Add (this.imagepepe);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.imagepepe]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			this.hbox4.Add (this.vbox5);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.vbox5]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			// Container child hbox5.Gtk.Box+BoxChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = -8;
			// Container child vbox4.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("<big><big><big><big><b>Selecciona las palabras que recuerdes de la lista azul</b></big></big></big></big>");
			this.label2.UseMarkup = true;
			this.label2.UseUnderline = true;
			this.vbox4.Add (this.label2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.label2]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.alignment8 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment8.Name = "alignment8";
			this.alignment8.LeftPadding = ((uint)(50));
			// Container child alignment8.Gtk.Container+ContainerChild
			this.explanation = new global::Gtk.Label ();
			this.explanation.WidthRequest = 1000;
			this.explanation.Name = "explanation";
			this.explanation.Yalign = 1F;
			this.explanation.LabelProp = global::Mono.Unix.Catalog.GetString ("<big><big><b>Para seleccionar</b> pulsa sobre la palabra</big></big>\n<big><big><b>Para deseleccionar</b> pulsa de nuevo sobre ella</big></big>");
			this.explanation.UseMarkup = true;
			this.explanation.Wrap = true;
			this.alignment8.Add (this.explanation);
			this.vbox4.Add (this.alignment8);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.alignment8]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			w7.Padding = ((uint)(5));
			this.hbox5.Add (this.vbox4);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.vbox4]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			this.vbox3.Add (this.hbox5);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox5]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.alignment3 = new global::Gtk.Alignment (0.5F, 1F, 1F, 1F);
			this.alignment3.Name = "alignment3";
			// Container child alignment3.Gtk.Container+ContainerChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.alignment9 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment9.Name = "alignment9";
			this.vbox1.Add (this.alignment9);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.alignment9]));
			w10.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("label1");
			this.hbox3.Add (this.label1);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label1]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			this.vbox1.Add (this.hbox3);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox3]));
			w12.Position = 1;
			w12.Expand = false;
			w12.Fill = false;
			this.hbox6.Add (this.vbox1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.vbox1]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.alignment3.Add (this.hbox6);
			this.vbox3.Add (this.alignment3);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.alignment3]));
			w15.Position = 1;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator ();
			this.hseparator1.Name = "hseparator1";
			this.vbox3.Add (this.hseparator1);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hseparator1]));
			w16.Position = 2;
			w16.Expand = false;
			w16.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.alignment2 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment2.Name = "alignment2";
			this.hbox2.Add (this.alignment2);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.alignment2]));
			w17.Position = 0;
			// Container child hbox2.Gtk.Box+BoxChild
			this.vseparator2 = new global::Gtk.VSeparator ();
			this.vseparator2.Name = "vseparator2";
			this.hbox2.Add (this.vseparator2);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.vseparator2]));
			w18.Position = 1;
			w18.Expand = false;
			w18.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonPrevious = new global::Gtk.Button ();
			this.buttonPrevious.CanFocus = true;
			this.buttonPrevious.Name = "buttonPrevious";
			this.buttonPrevious.UseUnderline = true;
			// Container child buttonPrevious.Gtk.Container+ContainerChild
			global::Gtk.Alignment w19 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w20 = new global::Gtk.HBox ();
			w20.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w21 = new global::Gtk.Image ();
			w21.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-back", global::Gtk.IconSize.Dnd);
			w20.Add (w21);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w23 = new global::Gtk.Label ();
			w23.LabelProp = global::Mono.Unix.Catalog.GetString ("Anterior");
			w23.UseUnderline = true;
			w20.Add (w23);
			w19.Add (w20);
			this.buttonPrevious.Add (w19);
			this.hbox2.Add (this.buttonPrevious);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonPrevious]));
			w27.Position = 2;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonNext = new global::Gtk.Button ();
			this.buttonNext.CanFocus = true;
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.UseUnderline = true;
			// Container child buttonNext.Gtk.Container+ContainerChild
			global::Gtk.Alignment w28 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w29 = new global::Gtk.HBox ();
			w29.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w30 = new global::Gtk.Image ();
			w30.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-forward", global::Gtk.IconSize.Dnd);
			w29.Add (w30);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w32 = new global::Gtk.Label ();
			w32.LabelProp = global::Mono.Unix.Catalog.GetString ("Siguiente");
			w32.UseUnderline = true;
			w29.Add (w32);
			w28.Add (w29);
			this.buttonNext.Add (w28);
			this.hbox2.Add (this.buttonNext);
			global::Gtk.Box.BoxChild w36 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonNext]));
			w36.Position = 3;
			// Container child hbox2.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator ();
			this.vseparator1.Name = "vseparator1";
			this.hbox2.Add (this.vseparator1);
			global::Gtk.Box.BoxChild w37 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.vseparator1]));
			w37.Position = 4;
			w37.Expand = false;
			w37.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.botonListo = new global::Gtk.Button ();
			this.botonListo.CanFocus = true;
			this.botonListo.Name = "botonListo";
			this.botonListo.UseUnderline = true;
			// Container child botonListo.Gtk.Container+ContainerChild
			global::Gtk.Alignment w38 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w39 = new global::Gtk.HBox ();
			w39.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w40 = new global::Gtk.Image ();
			w40.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Dnd);
			w39.Add (w40);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w42 = new global::Gtk.Label ();
			w42.LabelProp = global::Mono.Unix.Catalog.GetString ("¡Listo!");
			w42.UseUnderline = true;
			w39.Add (w42);
			w38.Add (w39);
			this.botonListo.Add (w38);
			this.hbox2.Add (this.botonListo);
			global::Gtk.Box.BoxChild w46 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.botonListo]));
			w46.Position = 5;
			this.vbox3.Add (this.hbox2);
			global::Gtk.Box.BoxChild w47 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox2]));
			w47.Position = 3;
			w47.Expand = false;
			w47.Fill = false;
			this.hbox4.Add (this.vbox3);
			global::Gtk.Box.BoxChild w48 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.vbox3]));
			w48.Position = 1;
			this.GtkAlignment.Add (this.hbox4);
			this.frame1.Add (this.GtkAlignment);
			this.vbox2.Add (this.frame1);
			global::Gtk.Box.BoxChild w51 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.frame1]));
			w51.Position = 1;
			w51.Expand = false;
			w51.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.alignment5 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment5.Name = "alignment5";
			this.vbox2.Add (this.alignment5);
			global::Gtk.Box.BoxChild w52 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.alignment5]));
			w52.Position = 2;
			this.hbox1.Add (this.vbox2);
			global::Gtk.Box.BoxChild w53 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox2]));
			w53.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.alignment6 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment6.Name = "alignment6";
			this.hbox1.Add (this.alignment6);
			global::Gtk.Box.BoxChild w54 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.alignment6]));
			w54.Position = 2;
			this.Add (this.hbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}