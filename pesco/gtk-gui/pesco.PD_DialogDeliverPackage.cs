
// This file has been generated by the GUI designer. Do not modify.
namespace pesco
{
	public partial class PD_DialogDeliverPackage
	{
		private global::Gtk.Label labelQuestion;
		private global::Gtk.VBox vbox6;
		private global::Gtk.Alignment alignment7;
		private global::Gtk.HBox hboxBoxes;
		private global::Gtk.Alignment alignment8;
		private global::Gtk.HButtonBox __gtksharp_74_Stetic_TopLevelDialog_ActionArea1;
		private global::Gtk.Button buttonCancel;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget pesco.PD_DialogDeliverPackage
			this.Name = "pesco.PD_DialogDeliverPackage";
			this.Title = global::Mono.Unix.Catalog.GetString ("Entregar caja");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child pesco.PD_DialogDeliverPackage.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.labelQuestion = new global::Gtk.Label ();
			this.labelQuestion.Name = "labelQuestion";
			this.labelQuestion.LabelProp = global::Mono.Unix.Catalog.GetString ("¿Qué caja quieres entregar?");
			w1.Add (this.labelQuestion);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(w1 [this.labelQuestion]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox6 = new global::Gtk.VBox ();
			this.vbox6.Name = "vbox6";
			this.vbox6.Spacing = 6;
			// Container child vbox6.Gtk.Box+BoxChild
			this.alignment7 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment7.Name = "alignment7";
			// Container child alignment7.Gtk.Container+ContainerChild
			this.hboxBoxes = new global::Gtk.HBox ();
			this.hboxBoxes.Name = "hboxBoxes";
			this.hboxBoxes.Spacing = 6;
			this.alignment7.Add (this.hboxBoxes);
			this.vbox6.Add (this.alignment7);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.alignment7]));
			w4.Position = 0;
			// Container child vbox6.Gtk.Box+BoxChild
			this.alignment8 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
			this.alignment8.Name = "alignment8";
			// Container child alignment8.Gtk.Container+ContainerChild
			this.__gtksharp_74_Stetic_TopLevelDialog_ActionArea1 = new global::Gtk.HButtonBox ();
			this.__gtksharp_74_Stetic_TopLevelDialog_ActionArea1.Name = "__gtksharp_74_Stetic_TopLevelDialog_ActionArea1";
			this.alignment8.Add (this.__gtksharp_74_Stetic_TopLevelDialog_ActionArea1);
			this.vbox6.Add (this.alignment8);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.alignment8]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			w1.Add (this.vbox6);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox6]));
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 2;
			// Internal child pesco.PD_DialogDeliverPackage.ActionArea
			global::Gtk.HButtonBox w8 = this.ActionArea;
			w8.Name = "__gtksharp_74_Stetic_TopLevelDialog_ActionArea";
			// Container child __gtksharp_74_Stetic_TopLevelDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("Cancelar");
			this.AddActionWidget (this.buttonCancel, 0);
			global::Gtk.ButtonBox.ButtonBoxChild w9 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w8 [this.buttonCancel]));
			w9.Expand = false;
			w9.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show ();
			this.buttonCancel.Clicked += new global::System.EventHandler (this.cancelCallback);
		}
	}
}
