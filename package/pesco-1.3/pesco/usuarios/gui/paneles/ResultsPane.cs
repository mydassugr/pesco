using System;
namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ResultsPane : Gtk.Bin
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnExitButton (object sender, System.EventArgs e)
		{
			
			this.ParentWindow.Destroy();
		}
		
	/// <summary>
	/// 
	/// </summary>
		public ResultsPane ()
		{
			this.Build ();
			
			GtkUtil.SetStyle(this.table2, Configuration.Current.MediumFont);
			
			barraAtencion.Fraction = .75;
			barraMemoria1.Fraction = .55;
			barraRazonamiento.Fraction = .85;
			barraPlanificacion.Fraction = .95;
		}
	}
}

