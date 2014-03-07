
// This file has been generated by the GUI designer. Do not modify.
namespace pesco
{
	public partial class ExerciseDemoSemanticSeries
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.Alignment alignment2;
		private global::Gtk.HBox hboxCentral;
		private global::Gtk.Alignment alignment3;
		private global::Gtk.VBox vbox3;
		private global::Gtk.Image imageBackground;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Button buttonGoLast;
		private global::Gtk.Alignment alignment6;
		private global::Gtk.Button buttonStartExercise;
		private global::Gtk.Button buttonGoBack;
		private global::Gtk.Button buttonGoForward;
		private global::Gtk.Alignment alignment4;
		private global::Gtk.Alignment alignment5;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget pesco.ExerciseDemoSemanticSeries
			global::Stetic.BinContainer.Attach (this);
			this.Name = "pesco.ExerciseDemoSemanticSeries";
			// Container child pesco.ExerciseDemoSemanticSeries.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.alignment2 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment2.Name = "alignment2";
			this.vbox2.Add (this.alignment2);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.alignment2]));
			w1.Position = 0;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hboxCentral = new global::Gtk.HBox ();
			this.hboxCentral.Name = "hboxCentral";
			this.hboxCentral.Spacing = 6;
			// Container child hboxCentral.Gtk.Box+BoxChild
			this.alignment3 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment3.Name = "alignment3";
			this.hboxCentral.Add (this.alignment3);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hboxCentral [this.alignment3]));
			w2.Position = 0;
			// Container child hboxCentral.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.HeightRequest = 650;
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.imageBackground = new global::Gtk.Image ();
			this.imageBackground.WidthRequest = 1000;
			this.imageBackground.HeightRequest = 600;
			this.imageBackground.Name = "imageBackground";
			this.vbox3.Add (this.imageBackground);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.imageBackground]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonGoLast = new global::Gtk.Button ();
			this.buttonGoLast.CanFocus = true;
			this.buttonGoLast.Name = "buttonGoLast";
			this.buttonGoLast.UseUnderline = true;
			// Container child buttonGoLast.Gtk.Container+ContainerChild
			global::Gtk.Alignment w4 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w5 = new global::Gtk.HBox ();
			w5.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w6 = new global::Gtk.Image ();
			w6.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-goto-last", global::Gtk.IconSize.Dnd);
			w5.Add (w6);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w8 = new global::Gtk.Label ();
			w8.LabelProp = global::Mono.Unix.Catalog.GetString ("Saltar instrucciones");
			w8.UseUnderline = true;
			w5.Add (w8);
			w4.Add (w5);
			this.buttonGoLast.Add (w4);
			this.hbox2.Add (this.buttonGoLast);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonGoLast]));
			w12.Position = 0;
			// Container child hbox2.Gtk.Box+BoxChild
			this.alignment6 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment6.Name = "alignment6";
			this.hbox2.Add (this.alignment6);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.alignment6]));
			w13.Position = 1;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonStartExercise = new global::Gtk.Button ();
			this.buttonStartExercise.CanFocus = true;
			this.buttonStartExercise.Name = "buttonStartExercise";
			this.buttonStartExercise.UseUnderline = true;
			// Container child buttonStartExercise.Gtk.Container+ContainerChild
			global::Gtk.Alignment w14 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w15 = new global::Gtk.HBox ();
			w15.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w16 = new global::Gtk.Image ();
			w16.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Dnd);
			w15.Add (w16);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w18 = new global::Gtk.Label ();
			w18.LabelProp = global::Mono.Unix.Catalog.GetString ("Comenzar ejercicio");
			w18.UseUnderline = true;
			w15.Add (w18);
			w14.Add (w15);
			this.buttonStartExercise.Add (w14);
			this.hbox2.Add (this.buttonStartExercise);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonStartExercise]));
			w22.Position = 2;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonGoBack = new global::Gtk.Button ();
			this.buttonGoBack.CanFocus = true;
			this.buttonGoBack.Name = "buttonGoBack";
			this.buttonGoBack.UseUnderline = true;
			// Container child buttonGoBack.Gtk.Container+ContainerChild
			global::Gtk.Alignment w23 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w24 = new global::Gtk.HBox ();
			w24.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w25 = new global::Gtk.Image ();
			w25.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-back", global::Gtk.IconSize.Dnd);
			w24.Add (w25);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w27 = new global::Gtk.Label ();
			w27.LabelProp = global::Mono.Unix.Catalog.GetString ("Anterior");
			w27.UseUnderline = true;
			w24.Add (w27);
			w23.Add (w24);
			this.buttonGoBack.Add (w23);
			this.hbox2.Add (this.buttonGoBack);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonGoBack]));
			w31.Position = 3;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonGoForward = new global::Gtk.Button ();
			this.buttonGoForward.CanFocus = true;
			this.buttonGoForward.Name = "buttonGoForward";
			this.buttonGoForward.UseUnderline = true;
			// Container child buttonGoForward.Gtk.Container+ContainerChild
			global::Gtk.Alignment w32 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w33 = new global::Gtk.HBox ();
			w33.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w34 = new global::Gtk.Image ();
			w34.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-forward", global::Gtk.IconSize.Dnd);
			w33.Add (w34);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w36 = new global::Gtk.Label ();
			w36.LabelProp = global::Mono.Unix.Catalog.GetString ("Siguiente");
			w36.UseUnderline = true;
			w33.Add (w36);
			w32.Add (w33);
			this.buttonGoForward.Add (w32);
			this.hbox2.Add (this.buttonGoForward);
			global::Gtk.Box.BoxChild w40 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.buttonGoForward]));
			w40.Position = 4;
			this.vbox3.Add (this.hbox2);
			global::Gtk.Box.BoxChild w41 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox2]));
			w41.Position = 1;
			this.hboxCentral.Add (this.vbox3);
			global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.hboxCentral [this.vbox3]));
			w42.Position = 1;
			w42.Expand = false;
			w42.Fill = false;
			// Container child hboxCentral.Gtk.Box+BoxChild
			this.alignment4 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment4.Name = "alignment4";
			this.hboxCentral.Add (this.alignment4);
			global::Gtk.Box.BoxChild w43 = ((global::Gtk.Box.BoxChild)(this.hboxCentral [this.alignment4]));
			w43.Position = 2;
			this.vbox2.Add (this.hboxCentral);
			global::Gtk.Box.BoxChild w44 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hboxCentral]));
			w44.Position = 1;
			w44.Expand = false;
			w44.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.alignment5 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment5.Name = "alignment5";
			this.vbox2.Add (this.alignment5);
			global::Gtk.Box.BoxChild w45 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.alignment5]));
			w45.Position = 2;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonGoLast.Clicked += new global::System.EventHandler (this.OnButtonGoLastClicked);
			this.buttonStartExercise.Clicked += new global::System.EventHandler (this.OnButtonStartExerciseClicked);
			this.buttonGoBack.Clicked += new global::System.EventHandler (this.OnButtonGoBackClicked);
			this.buttonGoForward.Clicked += new global::System.EventHandler (this.OnButtonGoForwardClicked);
		}
	}
}
