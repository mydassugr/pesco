
using System;
using System.Reflection;

namespace pesco
{

	public delegate void TimerAction();

	//TODO implementar cuenta atras
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class Timer : Gtk.Bin
	{
		
		DateTime start;
		TimerAction t;
		double limite = 0;
		
		uint increment = 1000;
		uint id;
		
		bool countdown = false;
		bool stop = false;
		
		
		public bool Stop {
			get {
				return stop;
			}
			set {
				stop = value;
			}
		}
		
		public uint Increment {
			get {
				return increment;
			}
			set {
				increment = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public uint Id {
			get {
				return id;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void StopTimer()
		{
			GLib.Source.Remove(id);	
			stop = true;
		}
		
		public void ResetTimer()
		{
			this.StopTimer();
			this.StartClock();
		}
		
		public void ResetTimer(TimerAction action)
		{
			this.StopTimer();
			t = action;
			this.StartClock();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="accion">
		/// A <see cref="TimerAction"/>
		/// </param>
		/// <param name="limite">
		/// A <see cref="System.Int32"/>
		/// </param>
		public Timer (TimerAction accion, double limit)
		{
			this.Build ();
			StartClock();
			t = accion;
			limite = limit;		
			
			GtkUtil.SetStyle(this, Configuration.Current.LargeFont);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="accion">
		/// A <see cref="TimerAction"/>
		/// </param>
		/// <param name="limit">
		/// A <see cref="System.Double"/> indicando el limite del temporizador en segundos
		/// </param>
		/// <param name="ca">
		/// A <see cref="System.Boolean"/>
		/// </param>
		public Timer (TimerAction accion, double limit, bool ca)
		{
			this.Build ();
			//startClock();
			t = accion;
			limite = limit;
			countdown = ca;
		}
		
		public void StartClock()
		{
			label1.Text = "0s / " + limite  + "s";
			
			stop = false;
			id = GLib.Timeout.Add(increment, new GLib.TimeoutHandler(Tick));
			start = DateTime.Now;
		}
		
		bool Tick()
		{
			
			if (stop){
				stop = false;
				return false;
			}
			
			TimeSpan transcurrido = DateTime.Now - start;
			
			string label = "";
			
			if (transcurrido.Minutes > 0)
				label += transcurrido.Minutes + "m  ";
			
			label += transcurrido.Seconds + "s / " + limite  + "s";
			label1.Text = label;
			
			// si hemos llegado al total del temporizador
			if (limite <= transcurrido.TotalSeconds)
			{
				stop = true;
				t();
				return false;
			}
			else
				return true;
		}	
	}
}
