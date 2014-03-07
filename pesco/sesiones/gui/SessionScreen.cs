using System;
using Gtk;

namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	public partial class SessionScreen : Gtk.Window
	{
		
		protected User usuario;	
		SessionsTree listasesiones1;
				
		/// <summary>
		/// 
		/// </summary>
		public SessionScreen () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.Title = "Rehabilitación Cognitiva";
			
			this.Remove(hpaned1);
			
			this.DeleteEvent += OnDeleteEvent;
			usuario = User.Deserialize();
			
			listasesiones1 = new SessionsTree();
			listasesiones1.ShowAll();
			this.Add(listasesiones1);			
			GtkUtil.SetStyle(this.listasesiones1, Configuration.Current.SmallFont);
			
			//this.Maximize();
			this.Show();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnClickStartActivity (object sender, System.EventArgs e)
		{
			Exercise ej = Exercise.GetEjercicio(1);			
			ej.inicializar();
			ej.iniciar();
			
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnViewResultsClick (object sender, System.EventArgs e)
		{
			/*DialogoResultados d = new DialogoResultados();
			d.Show();*/
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="o">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="args">
		/// A <see cref="Gtk.DeleteEventArgs"/>
		/// </param>
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Application.Quit ();
			args.RetVal = true;
		}
	}
}

