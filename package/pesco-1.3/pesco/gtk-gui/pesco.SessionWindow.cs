
// This file has been generated by the GUI designer. Do not modify.
namespace pesco
{
	public partial class SessionWindow
	{
		private global::Gtk.VBox vboxContainer;
		private global::Gtk.VBox vboxIntroSessions;
		private global::Gtk.VBox vbox5;
		private global::Gtk.Alignment alignment11;
		private global::Gtk.HBox hbox4;
		private global::Gtk.Alignment alignment13;
		private global::Gtk.VBox vbox6;
		private global::pesco.DialogPanel dialogPanel;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Alignment alignment7;
		private global::Gtk.Button startSessionButton;
		private global::Gtk.Alignment alignment14;
		private global::Gtk.Alignment alignment12;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget pesco.SessionWindow
			this.Name = "pesco.SessionWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("PESCO (Programa de EStimulación COgnitiva)");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child pesco.SessionWindow.Gtk.Container+ContainerChild
			this.vboxContainer = new global::Gtk.VBox ();
			this.vboxContainer.Name = "vboxContainer";
			this.vboxContainer.Spacing = 6;
			// Container child vboxContainer.Gtk.Box+BoxChild
			this.vboxIntroSessions = new global::Gtk.VBox ();
			this.vboxIntroSessions.Name = "vboxIntroSessions";
			this.vboxIntroSessions.Spacing = 6;
			this.vboxIntroSessions.BorderWidth = ((uint)(6));
			// Container child vboxIntroSessions.Gtk.Box+BoxChild
			this.vbox5 = new global::Gtk.VBox ();
			this.vbox5.Name = "vbox5";
			this.vbox5.Spacing = 6;
			// Container child vbox5.Gtk.Box+BoxChild
			this.alignment11 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment11.Name = "alignment11";
			this.vbox5.Add (this.alignment11);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.alignment11]));
			w1.Position = 0;
			// Container child vbox5.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.alignment13 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment13.Name = "alignment13";
			this.hbox4.Add (this.alignment13);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.alignment13]));
			w2.Position = 0;
			// Container child hbox4.Gtk.Box+BoxChild
			this.vbox6 = new global::Gtk.VBox ();
			this.vbox6.Name = "vbox6";
			this.vbox6.Spacing = 6;
			// Container child vbox6.Gtk.Box+BoxChild
			this.dialogPanel = new global::pesco.DialogPanel ();
			this.dialogPanel.Events = ((global::Gdk.EventMask)(256));
			this.dialogPanel.Name = "dialogPanel";
			this.dialogPanel.CurrentStep = 0;
			this.vbox6.Add (this.dialogPanel);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.dialogPanel]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox6.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.alignment7 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment7.Name = "alignment7";
			this.hbox3.Add (this.alignment7);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.alignment7]));
			w4.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.startSessionButton = new global::Gtk.Button ();
			this.startSessionButton.CanFocus = true;
			this.startSessionButton.Name = "startSessionButton";
			this.startSessionButton.UseUnderline = true;
			// Container child startSessionButton.Gtk.Container+ContainerChild
			global::Gtk.Alignment w5 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w6 = new global::Gtk.HBox ();
			w6.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w7 = new global::Gtk.Image ();
			w7.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-forward", global::Gtk.IconSize.Dnd);
			w6.Add (w7);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w9 = new global::Gtk.Label ();
			w9.LabelProp = global::Mono.Unix.Catalog.GetString ("Comenzar");
			w9.UseUnderline = true;
			w6.Add (w9);
			w5.Add (w6);
			this.startSessionButton.Add (w5);
			this.hbox3.Add (this.startSessionButton);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.startSessionButton]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.vbox6.Add (this.hbox3);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.hbox3]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			this.hbox4.Add (this.vbox6);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.vbox6]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.alignment14 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment14.Name = "alignment14";
			this.hbox4.Add (this.alignment14);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.alignment14]));
			w16.Position = 2;
			this.vbox5.Add (this.hbox4);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.hbox4]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.alignment12 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment12.Name = "alignment12";
			this.vbox5.Add (this.alignment12);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox5 [this.alignment12]));
			w18.Position = 2;
			this.vboxIntroSessions.Add (this.vbox5);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vboxIntroSessions [this.vbox5]));
			w19.Position = 0;
			this.vboxContainer.Add (this.vboxIntroSessions);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vboxContainer [this.vboxIntroSessions]));
			w20.Position = 0;
			this.Add (this.vboxContainer);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 1270;
			this.DefaultHeight = 761;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
			this.KeyPressEvent += new global::Gtk.KeyPressEventHandler (this.OnKeyPressEvent);
			this.startSessionButton.Clicked += new global::System.EventHandler (this.startExerciseClicked);
		}
	}
}