using System;
using Gdk;
using Gtk;

namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class LeisureActivitiesPane : Gtk.Bin
	{
		public void SetLeasureActivities(User user){
			this.horasLeer.Text= (user.ReadingHours == -1)? "" : user.ReadingHours.ToString();
			this.horasTaller.Text=(user.WorkshopHours == -1)? "":user.WorkshopHours.ToString();
			this.horasEjercicio.Text=(user.ExerciseHours == -1)? "": user.ExerciseHours.ToString();
			this.horasOrdenador.Text=(user.ComputerHours == -1)? "":user.ComputerHours.ToString();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public int GetReadingTime(){
			if(this.horasLeer.Text ==""){
				MarkWidget(this.horasLeer);
				return -1;
			}
			
			this.horasLeer.ModifyBase(Gtk.StateType.Normal);
			return  Convert.ToInt16(this.horasLeer.Text);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public int GetWorkshopTime(){
			if(this.horasTaller.Text ==""){
				MarkWidget(this.horasTaller);
				return -1;
			}
			this.horasTaller.ModifyBase(Gtk.StateType.Normal);
			return  Convert.ToInt16(this.horasTaller.Text);
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public int GetExerciseTime(){
			if(this.horasEjercicio.Text ==""){
				MarkWidget(this.horasEjercicio);
				return -1;
			}
			this.horasEjercicio.ModifyBase(Gtk.StateType.Normal);
			return  Convert.ToInt16(this.horasEjercicio.Text);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public int GetComputerTime(){
			
			if(this.horasOrdenador.Text ==""){
				MarkWidget(this.horasOrdenador);
				return -1;
			}
			this.horasOrdenador.ModifyBase(Gtk.StateType.Normal);
			return Convert.ToInt16(this.horasOrdenador.Text);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public LeisureActivitiesPane ()
		{
			this.Build ();
			GtkUtil.SetStyle(this,Configuration.Current.LargeFont);	
			GtkUtil.SetStyle(subtitulo, Configuration.Current.SmallFont);
			GtkUtil.SetStyle(label2, Configuration.Current.MediumFont);
			GtkUtil.SetStyle(label3, Configuration.Current.MediumFont);
			GtkUtil.SetStyle(label4, Configuration.Current.MediumFont);
			GtkUtil.SetStyle(label5, Configuration.Current.MediumFont);
			GtkUtil.SetStyle(label6, Configuration.Current.SmallFont);
			GtkUtil.SetStyle(label7, Configuration.Current.SmallFont);
			GtkUtil.SetStyle(label8, Configuration.Current.SmallFont);
			GtkUtil.SetStyle(label9, Configuration.Current.SmallFont);
			
			
			
		}
		protected virtual void ValidateKey (Gtk.WidgetEventArgs args)
		{
			
			string s_charvalidos = "0123456789";
			if (Array.IndexOf(s_charvalidos.ToCharArray(), Convert.ToChar(((Gdk.EventKey)args.Event).Key)) == -1 && 
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.BackSpace && 
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Delete && 
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Left && 
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Right &&
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Tab &&
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Shift_L &&
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Shift_R &&
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Shift_Lock &&			    
			    !(((Gdk.EventKey)args.Event).Key >=Gdk.Key.KP_0 && ((Gdk.EventKey)args.Event).Key <=Gdk.Key.KP_9))
	             args.RetVal=true;
			
			
		}
		protected virtual void OnHorasLeerWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateKey(args);
		}
		
		protected virtual void OnHorasTallerWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateKey(args);
		}
		
		protected virtual void OnHorasEjercicioWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateKey(args);
		}
		
		protected virtual void OnHorasOrdenadorWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateKey(args);
		}
		private static void MarkWidget(Gtk.Widget w)
		{
			w.GrabFocus();			
			w.ModifyBase(Gtk.StateType.Normal, new Gdk.Color(0xff, 0xbb, 0xbb));
		}
		
		public void MarkEntryAsNormal()
		{
			this.horasLeer.ModifyBase(Gtk.StateType.Normal);
			this.horasTaller.ModifyBase(Gtk.StateType.Normal);
			this.horasEjercicio.ModifyBase(Gtk.StateType.Normal);
			this.horasOrdenador.ModifyBase(Gtk.StateType.Normal);
			
		}
	}
}

