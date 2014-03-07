using System;
namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ResidencePane : Gtk.Bin
	{
		UsersToggleButton esposo = new UsersToggleButton("Esposo/a o pareja");
		UsersToggleButton solo = new UsersToggleButton("Solo/a");
		UsersToggleButton residencia = new UsersToggleButton("Residencia");
		UsersToggleButton otrosFamiliares = new UsersToggleButton("Hijo/a");
		UsersToggleButton hijo = new UsersToggleButton("Otros familiares");
		UsersToggleButton otros = new UsersToggleButton("Otros");
		
		public void SetLivesWith(User user){
			
			if (LivesWith.spouse == user.Lives)
				this.esposo.Active=true;
			else if (LivesWith.alone == user.Lives)
				this.solo.Active=true;
			else if (LivesWith.residency== user.Lives )
				this.residencia.Active=true ;
			else if (LivesWith.child == user.Lives)
				this.hijo.Active=true;
			else if (LivesWith.otherFamilyMenbers== user.Lives)
				this.otrosFamiliares.Active=true;
			else if (LivesWith.others == user.Lives)
				this.otros.Active=true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="ConQuienVive"/>
		/// </returns>
		public LivesWith GetLivesWith()
		{
			LivesWith cqv = LivesWith.error;
			
			if (this.esposo.Active)
				cqv = LivesWith.spouse;
			else if (this.solo.Active)
				cqv = LivesWith.alone;
			else if (this.residencia.Active)
				cqv = LivesWith.residency;
			else if (this.hijo.Active)
				cqv = LivesWith.child;
			else if (this.otrosFamiliares.Active)
				cqv = LivesWith.otherFamilyMenbers;
			else if (this.otros.Active)
				cqv = LivesWith.others;
			
			return cqv;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public ResidencePane ()
		{
			this.Build ();
			GtkUtil.SetStyle(this,Configuration.Current.LargeFont);	
			
			GtkUtil.SetStyle(subtitulo, Configuration.Current.SmallFont);
			
			optionButtons.PackStart(esposo,true, true,0);
			optionButtons.PackStart(solo,true, true,0);
			optionButtons.PackStart(residencia,true, true,0);
			optionButtons.PackStart(otrosFamiliares,true, true,0);
			optionButtons.PackStart(hijo,true, true,0);
			optionButtons.PackStart(otros,true, true,0);
			
			
			GtkUtil.SetStyle(this.esposo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.solo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.residencia, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.otrosFamiliares, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.hijo, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.otros, Configuration.Current.ButtonFont);
			
			this.ShowAll();
		}
	}
}

