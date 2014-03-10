
// This file has been generated by the GUI designer. Do not modify.
namespace pesco
{
	public partial class MainWindow
	{
		private global::Gtk.VBox vboxMain;
		private global::Gtk.Fixed fixedMain;
		private global::pesco.WelcomePanel welcomePanel;
		private global::Gtk.VBox vboxBottom;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Alignment alignment1;
		private global::Gtk.Button button2;
		private global::Gtk.Alignment alignment2;
		private global::Gtk.Alignment alignment3;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Button buttonShowMedals;
		private global::Gtk.Alignment alignment5;
		private global::Gtk.Alignment alignment4;
		private global::Gtk.Button buttonExercises;
		private global::Gtk.Button buttonAbout;
		private global::Gtk.Label label1;
		private global::Gtk.Label label3;
		private global::Gtk.HBox hbox4;
		private global::Gtk.Button button3;
		private global::Gtk.Button completeTestButton;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget pesco.MainWindow
			this.Name = "pesco.MainWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
			this.WindowPosition = ((global::Gtk.WindowPosition)(3));
			this.Resizable = false;
			this.AllowGrow = false;
			// Container child pesco.MainWindow.Gtk.Container+ContainerChild
			this.vboxMain = new global::Gtk.VBox ();
			this.vboxMain.Name = "vboxMain";
			// Container child vboxMain.Gtk.Box+BoxChild
			this.fixedMain = new global::Gtk.Fixed ();
			this.fixedMain.WidthRequest = 1000;
			this.fixedMain.HeightRequest = 600;
			this.fixedMain.Name = "fixedMain";
			this.fixedMain.HasWindow = false;
			// Container child fixedMain.Gtk.Fixed+FixedChild
			this.welcomePanel = new global::pesco.WelcomePanel ();
			this.welcomePanel.Events = ((global::Gdk.EventMask)(256));
			this.welcomePanel.Name = "welcomePanel";
			this.fixedMain.Add (this.welcomePanel);
			// Container child fixedMain.Gtk.Fixed+FixedChild
			this.vboxBottom = new global::Gtk.VBox ();
			this.vboxBottom.WidthRequest = 900;
			this.vboxBottom.HeightRequest = 120;
			this.vboxBottom.Name = "vboxBottom";
			this.vboxBottom.Spacing = 6;
			// Container child vboxBottom.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.alignment1 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment1.Name = "alignment1";
			this.hbox2.Add (this.alignment1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.alignment1]));
			w2.Position = 0;
			// Container child hbox2.Gtk.Box+BoxChild
			this.button2 = new global::Gtk.Button ();
			this.button2.CanFocus = true;
			this.button2.Name = "button2";
			this.button2.UseUnderline = true;
			// Container child button2.Gtk.Container+ContainerChild
			global::Gtk.Alignment w3 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w4 = new global::Gtk.HBox ();
			w4.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w5 = new global::Gtk.Image ();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-dialog-authentication", global::Gtk.IconSize.Dnd);
			w4.Add (w5);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w7 = new global::Gtk.Label ();
			w7.LabelProp = global::Mono.Unix.Catalog.GetString ("Entrar");
			w7.UseUnderline = true;
			w4.Add (w7);
			w3.Add (w4);
			this.button2.Add (w3);
			this.hbox2.Add (this.button2);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.button2]));
			w11.Position = 1;
			// Container child hbox2.Gtk.Box+BoxChild
			this.alignment2 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment2.Name = "alignment2";
			this.hbox2.Add (this.alignment2);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.alignment2]));
			w12.Position = 2;
			this.vboxBottom.Add (this.hbox2);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vboxBottom [this.hbox2]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vboxBottom.Gtk.Box+BoxChild
			this.alignment3 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment3.Name = "alignment3";
			this.vboxBottom.Add (this.alignment3);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vboxBottom [this.alignment3]));
			w14.Position = 1;
			// Container child vboxBottom.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.buttonShowMedals = new global::Gtk.Button ();
			this.buttonShowMedals.CanFocus = true;
			this.buttonShowMedals.Name = "buttonShowMedals";
			this.buttonShowMedals.UseUnderline = true;
			this.buttonShowMedals.Label = global::Mono.Unix.Catalog.GetString ("Ver medallas");
			this.hbox3.Add (this.buttonShowMedals);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.buttonShowMedals]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.alignment5 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment5.Name = "alignment5";
			// Container child alignment5.Gtk.Container+ContainerChild
			this.alignment4 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment4.Name = "alignment4";
			this.alignment5.Add (this.alignment4);
			this.hbox3.Add (this.alignment5);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.alignment5]));
			w17.Position = 1;
			// Container child hbox3.Gtk.Box+BoxChild
			this.buttonExercises = new global::Gtk.Button ();
			this.buttonExercises.CanFocus = true;
			this.buttonExercises.Name = "buttonExercises";
			this.buttonExercises.UseUnderline = true;
			this.buttonExercises.Label = global::Mono.Unix.Catalog.GetString ("Ejercicios");
			this.hbox3.Add (this.buttonExercises);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.buttonExercises]));
			w18.Position = 2;
			w18.Expand = false;
			w18.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.buttonAbout = new global::Gtk.Button ();
			this.buttonAbout.CanFocus = true;
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.UseUnderline = true;
			// Container child buttonAbout.Gtk.Container+ContainerChild
			global::Gtk.Alignment w19 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w20 = new global::Gtk.HBox ();
			w20.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w21 = new global::Gtk.Image ();
			w21.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-about", global::Gtk.IconSize.Button);
			w20.Add (w21);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w23 = new global::Gtk.Label ();
			w23.LabelProp = global::Mono.Unix.Catalog.GetString ("Acerca _de...");
			w23.UseUnderline = true;
			w20.Add (w23);
			w19.Add (w20);
			this.buttonAbout.Add (w19);
			this.hbox3.Add (this.buttonAbout);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.buttonAbout]));
			w27.Position = 3;
			w27.Expand = false;
			w27.Fill = false;
			this.vboxBottom.Add (this.hbox3);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vboxBottom [this.hbox3]));
			w28.Position = 2;
			w28.Expand = false;
			w28.Fill = false;
			this.fixedMain.Add (this.vboxBottom);
			global::Gtk.Fixed.FixedChild w29 = ((global::Gtk.Fixed.FixedChild)(this.fixedMain [this.vboxBottom]));
			w29.X = 50;
			w29.Y = 460;
			// Container child fixedMain.Gtk.Fixed+FixedChild
			this.label1 = new global::Gtk.Label ();
			this.label1.WidthRequest = 40;
			this.label1.HeightRequest = 10;
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("<span font_size=\"xx-small\" color=\"white\">1.0.2</span>");
			this.label1.UseMarkup = true;
			this.fixedMain.Add (this.label1);
			global::Gtk.Fixed.FixedChild w30 = ((global::Gtk.Fixed.FixedChild)(this.fixedMain [this.label1]));
			w30.X = 960;
			w30.Y = 590;
			this.vboxMain.Add (this.fixedMain);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vboxMain [this.fixedMain]));
			w31.Position = 0;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>¡Error, nombre de usuario incorrecto!</b>");
			this.label3.UseMarkup = true;
			this.vboxMain.Add (this.label3);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.vboxMain [this.label3]));
			w32.Position = 2;
			w32.Expand = false;
			w32.Fill = false;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			this.vboxMain.Add (this.hbox4);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.vboxMain [this.hbox4]));
			w33.Position = 3;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.button3 = new global::Gtk.Button ();
			this.button3.CanFocus = true;
			this.button3.Name = "button3";
			// Container child button3.Gtk.Container+ContainerChild
			global::Gtk.Alignment w34 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w35 = new global::Gtk.HBox ();
			w35.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w36 = new global::Gtk.Image ();
			w36.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-edit", global::Gtk.IconSize.LargeToolbar);
			w35.Add (w36);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w38 = new global::Gtk.Label ();
			w38.LabelProp = global::Mono.Unix.Catalog.GetString ("Soy nuevo usuario y quiero registrarme");
			w35.Add (w38);
			w34.Add (w35);
			this.button3.Add (w34);
			this.vboxMain.Add (this.button3);
			global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.vboxMain [this.button3]));
			w42.Position = 4;
			w42.Fill = false;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.completeTestButton = new global::Gtk.Button ();
			this.completeTestButton.CanFocus = true;
			this.completeTestButton.Name = "completeTestButton";
			this.completeTestButton.UseUnderline = true;
			// Container child completeTestButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w43 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w44 = new global::Gtk.HBox ();
			w44.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w45 = new global::Gtk.Image ();
			w45.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-copy", global::Gtk.IconSize.Menu);
			w44.Add (w45);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w47 = new global::Gtk.Label ();
			w47.LabelProp = global::Mono.Unix.Catalog.GetString ("Completar Test");
			w47.UseUnderline = true;
			w44.Add (w47);
			w43.Add (w44);
			this.completeTestButton.Add (w43);
			this.vboxMain.Add (this.completeTestButton);
			global::Gtk.Box.BoxChild w51 = ((global::Gtk.Box.BoxChild)(this.vboxMain [this.completeTestButton]));
			w51.Position = 5;
			w51.Expand = false;
			w51.Fill = false;
			this.Add (this.vboxMain);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 1016;
			this.DefaultHeight = 796;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnKeyPressEvent);
			this.buttonShowMedals.Clicked += new global::System.EventHandler (this.showMedalsClicked);
			this.buttonExercises.Clicked += new global::System.EventHandler (this.exercisesClicked);
			this.buttonAbout.Clicked += new global::System.EventHandler (this.OnClickAbout);
			this.button3.Clicked += new global::System.EventHandler (this.OnClickRegister);
		}
	}
}
