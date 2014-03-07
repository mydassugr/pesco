using System;
namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class EducationalLevelPane : Gtk.Bin
	{
		
		UsersToggleButton leerEscribir = new UsersToggleButton("Leer/Escribir");
		UsersToggleButton estudiosPrimarios = new UsersToggleButton("Estudios primarios");
		UsersToggleButton graduadoEscolar = new UsersToggleButton("Graduado escolar/EGB");
		UsersToggleButton bachillerato = new UsersToggleButton("Bachillerato/COU");
		UsersToggleButton fp = new UsersToggleButton("Formación Profesional");
		UsersToggleButton diplomatura = new UsersToggleButton("Diplomatura");
		UsersToggleButton licenciatura = new UsersToggleButton("Licenciatura");
		UsersToggleButton doctorado = new UsersToggleButton("Doctorado");
		
		public void SetEducationalLevel(User u){
			
			if (Education.writing_reading == u.EducationalLevel )
				this.leerEscribir.Active=true;
			else if (Education.school== u.EducationalLevel)
				this.estudiosPrimarios.Active=true;
			else if (Education.middle_school == u.EducationalLevel)
				this.graduadoEscolar.Active=true;
			else if (Education.high_school== u.EducationalLevel)
				this.bachillerato.Active=true;
			else if (Education.vocational_school== u.EducationalLevel )
				this.fp.Active=true;
			else if (Education.degree== u.EducationalLevel )
				this.diplomatura.Active=true;
			else if (Education.bachelor == u.EducationalLevel)
				this.licenciatura.Active=true;
			else if (Education.ph_d== u.EducationalLevel)
				this.doctorado.Active=true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// A <see cref="NivelEstudios"/>
		/// </returns>
		public Education GetEducationalLevel()
		{
			Education ne = Education.error;
			
			if (this.leerEscribir.Active)
				ne = Education.writing_reading;
			else if (this.estudiosPrimarios.Active)
				ne = Education.school;
			else if (this.graduadoEscolar.Active)
				ne = Education.middle_school;
			else if (this.bachillerato.Active)
				ne = Education.high_school;
			else if (this.fp.Active)
				ne = Education.vocational_school;
			else if (this.diplomatura.Active)
				ne = Education.degree;
			else if (this.licenciatura.Active)
				ne = Education.bachelor;
			else if (this.doctorado.Active)
				ne = Education.ph_d;
			
			return ne;
		}
		
		
		
		/// <summary>
		/// 
		/// </summary>
		public EducationalLevelPane ()
		{
			this.Build ();
			GtkUtil.SetStyle(this,Configuration.Current.LargeFont);	
			
			GtkUtil.SetStyle(subtitulo, Configuration.Current.SmallFont);
			
			optionButtons.PackStart(leerEscribir,true, true,0);
			optionButtons.PackStart(estudiosPrimarios,true, true,0);
			optionButtons.PackStart(graduadoEscolar,true, true,0);
			optionButtons.PackStart(bachillerato,true, true,0);
			optionButtons.PackStart(fp,true, true,0);
			optionButtons.PackStart(diplomatura,true, true,0);
			optionButtons.PackStart(licenciatura,true, true,0);
			optionButtons.PackStart(doctorado,true, true,0);
			
			
			GtkUtil.SetStyle(this.leerEscribir, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.estudiosPrimarios, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.graduadoEscolar, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.bachillerato, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.fp, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.diplomatura, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.licenciatura, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.doctorado, Configuration.Current.ButtonFont);
			
			this.ShowAll();
		}
	}
}

