using System;
namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class MaritalStatusPane : Gtk.Bin
	{
		UsersToggleButton casado = new UsersToggleButton("Casado/a");
		UsersToggleButton viudo = new UsersToggleButton("Viudo/a");
		UsersToggleButton divorciado = new UsersToggleButton("Divorciado/a");
		UsersToggleButton soltero = new UsersToggleButton("Soltero/a");
		UsersToggleButton otro = new UsersToggleButton("Otro/a");
		
		public void SetMaritalStatus(User user){
			
			if (MaritalStatus.married == user.MaritalStat)
				this.casado.Active= true;
			else if(MaritalStatus.widow== user.MaritalStat)
				this.viudo.Active=true;
			else if (MaritalStatus.divorced== user.MaritalStat)
				this.divorciado.Active=true;
			else if (MaritalStatus.single== user.MaritalStat)
				this.soltero.Active=true ;
			else if (MaritalStatus.other==user.MaritalStat)
				this.otro.Active=true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="EstadoCivil"/>
		/// </returns>
		public MaritalStatus GetMaritalStatus(){
			
			MaritalStatus c = MaritalStatus.error;
			
			if (this.casado.Active)
				c = MaritalStatus.married;
			else if(this.viudo.Active)
				c = MaritalStatus.widow;
			else if (this.divorciado.Active)
				c = MaritalStatus.divorced;
			else if (this.soltero.Active)
				c = MaritalStatus.single;
			else if (this.otro.Active)
				c = MaritalStatus.other;
			
			return c;			
		}
		
		/// <summary>
		/// 
		/// </summary>
		public MaritalStatusPane ()
		{
			this.Build ();
			GtkUtil.SetStyle(this,Configuration.Current.LargeFont);	
			
			GtkUtil.SetStyle(subtitulo, Configuration.Current.SmallFont);
			
			optionButtons.PackStart(casado,true, true,0);
			optionButtons.PackStart(viudo,true, true,0);
			optionButtons.PackStart(divorciado,true, true,0);
			optionButtons.PackStart(soltero,true, true,0);
			optionButtons.PackStart(otro,true, true,0);
			
			
			GtkUtil.SetStyle(this.casado, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.viudo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.divorciado, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.soltero, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.otro, Configuration.Current.ButtonFont);
			
			this.ShowAll();
			
		}
	}
}

