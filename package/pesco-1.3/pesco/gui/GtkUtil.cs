using System;
using Gtk;
using Gdk;

namespace pesco
{
	public class MyMessageDialod: Gtk.MessageDialog{
		public Gtk.Button accept= new Gtk.Button();
		
		public Button Accept {
			get {return this.accept;}
			set {accept = value;}
		}

		public MyMessageDialod (string text, string btnText, int winWidth):base()
		{
			accept.Label= btnText;
			accept.Clicked += delegate {
				this.Destroy();
				
			};
			
			GtkUtil.SetStyle(accept,Configuration.Current.ButtonFont);
			this.Icon = Gtk.Image.LoadFromResource("pesco.gui.abuelo3.png").Pixbuf;
			this.Text="<big><big>"+ text+"</big></big>";
          	this.UseMarkup=true;
			this.DestroyWithParent=true;
			this.MessageType = Gtk.MessageType.Error;
			this.DefaultResponse= Gtk.ResponseType.Ok;
			this.VBox.Add(accept);
			this.KeepAbove=true;
			this.HasFocus=true;
			this.HeightRequest=200;
			this.WidthRequest=winWidth;
			this.SetPosition(Gtk.WindowPosition.CenterAlways);
			//GtkUtil.ChangeFont(this,Configuration.Current.ExtraHugeFont.FontType);
			
			this.ShowAll();
			this.GdkWindow.Cursor=GtkUtil.ChangeCursor();
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public class GtkUtil
	{
		
		private GtkUtil ()
		{
		}
		
		public static Cursor ChangeCursor(){
			
			Gtk.Image cursor= new Gtk.Image(Pixbuf.LoadFromResource("pesco.gui.flecha2.png"));
			Gdk.Cursor cc=new Gdk.Cursor(Gdk.Screen.Default.Display,cursor.Pixbuf,0,0);
			//Gdk.Cursor cc=new Gdk.Cursor(win.Display,cursor.Pixbuf,0,0);
			return cc;
			
		}
		public static void SetStyle(Widget w, FontStyle f){
			
			
			// si es un widget que permite cambiar la fuente directamente, la cambiamos
			if (w.GetType().Equals(typeof(Label)) || w.GetType().Equals(typeof(Entry))  || w.GetType().Equals(typeof(TextView))  || w.GetType().Equals(typeof(TreeView)) || w.GetType().Equals(typeof(SpinButton)))
			{
				
				//Console.WriteLine( "Toqueteando widget...");
				w.ModifyFont(Pango.FontDescription.FromString(f.FontType + " " + f.Size));
				w.ModifyFg(StateType.Normal, new Gdk.Color(f.Red, f.Green, f.Blue));
			}
			// si no es un label comprobamos que sea un boton
			else if (w.GetType().IsSubclassOf(typeof(Container)))
			{	
				
				foreach(Widget wi in  ((Container)w).Children)
					SetStyle(wi,  f);
			}
		}
		
		/// <summary>
		/// Cambia la fuente de un Widget. Si es un container cambia la fuente de todos los widgets que incluya
		/// </summary>
		/// <param name="w">
		/// <see cref="Widget"/>  a cambiar la fuente
		/// </param>
		/// <param name="font">
		/// A <see cref="System.String"/> con el nombre de la fuente y el tamaño
		/// </param>
		public static void ChangeFont(Widget w, string font)
		{
						
			// si es un widget que permite cambiar la fuente directamente, la cambiamos
			if (w.GetType().Equals(typeof(Label)) || w.GetType().Equals(typeof(Entry))  || w.GetType().Equals(typeof(TextView))  || w.GetType().Equals(typeof(TreeView)) || w.GetType().Equals(typeof(SpinButton)))
			{
				w.ModifyFont(Pango.FontDescription.FromString(font));
			}
			// si no es un label comprobamos que sea un boton
			else if (w.GetType().IsSubclassOf(typeof(Container)))
			{	
				
				foreach(Widget wi in  ((Container)w).Children)				
					ChangeFont(wi, font);
			}
		}
		
		public static void PimpButtonFromResource(Button b, string labelText, string resourceName)
		{
            /* Now on to the image stuff */
            Gtk.Image image = Gtk.Image.LoadFromResource(resourceName);                        
            PimpButton(b, labelText, image);
		}
		
		
		public static void PimpVerticalButtonFromResource(Button b, string labelText, string resourceName)
		{
            /* Now on to the image stuff */
            Gtk.Image image = Gtk.Image.LoadFromResource(resourceName);                        
            PimpVerticalButton(b, labelText, image);
		}
		public static void PimpButtonFromStock(Button b, string labelText, string resourceName)
		{
			/* Now on to the image stuff */
            Gtk.Image image = new Gtk.Image(resourceName, Gtk.IconSize.Dialog);
			 
			PimpButton(b, labelText, image);
		}
		
		public static void PimpButton(Button b, string labelText, Gtk.Image image)
		{
			
			b.Remove(b.Child);
			
			/* Create box for image and label */
			HBox box = new HBox(false, 0);
           	box.BorderWidth =  2;
			
            /* Create a label for the button */
           	Label label = new Label (labelText);
			label.Justify = Justification.Fill;
			
                        
            /* Pack the image and label into the box */
            box.PackStart (image, false, false, 3);
            box.PackStart(label, false, false, 3);
                        
            image.Show();
         	label.Show();
			box.Show();
			b.Add(box);
		}
		
		public static void PimpVerticalButton(Button b, string labelText, Gtk.Image image)
		{
			
			b.Remove(b.Child);
			
			/* Create box for image and label */
			VBox box = new VBox(false, 0);
           	box.BorderWidth =  2;
			
            /* Create a label for the button */
           	Label label = new Label (labelText);
			label.Justify = Justification.Fill;
			
                        
            /* Pack the image and label into the box */
            box.PackStart (image, false, false, 3);
            box.PackStart(label, false, false, 3);
                        
            image.Show();
         	label.Show();
			box.Show();
			b.Add(box);
		}
		

		
		/// <summary>
		/// Coloca un Widget en una caja en la posicion indicada
		/// </summary>
		/// <param name="c">
		/// El <see cref="Box"/> en el que se quiere agregar el widget
		/// </param>
		/// <param name="w">
		/// El <see cref="Widget"/> que se quiere colocar en la caja
		/// </param>
		/// <param name="pos">
		/// Un <see cref="System.Int32"/> indicando la posicion en la caja
		/// </param>
		public static void Put(Box c, Widget w, int pos)
		{
			c.Add (w);
			
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(c[w]));
				w3.Position = pos;
				w3.Expand = true;
				w3.Fill = true;
		}
		
		public static void SetStyleRecursive(Widget w, FontStyle f){
			
			// If widget allow to change it font, change it
			if (w.GetType().Equals(typeof(Label)) || w.GetType().Equals(typeof(Entry))  || w.GetType().Equals(typeof(TextView))  || w.GetType().Equals(typeof(TreeView)) || w.GetType().Equals(typeof(SpinButton)))
			{	
				
				w.ModifyFont(Pango.FontDescription.FromString(f.FontType + " " + f.Size));
				w.ModifyFg(StateType.Normal, new Gdk.Color(f.Red, f.Green, f.Blue));
				w.ShowAll();
			}
			// If not, check if it is a container
			else if (w.GetType().IsSubclassOf(typeof(Container)) )
			{	
				foreach(Widget wi in  ((Container)w).Children)
					SetStyleRecursive(wi,  f);
			} else if ( w.GetType().IsSubclassOf(typeof(MessageDialog) ) ) {
				foreach(Widget wi in  ((MessageDialog)w).Children)
					SetStyleRecursive(wi,  f);
			}
		}
	}
}

