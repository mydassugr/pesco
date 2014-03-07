using System;
using System.Text.RegularExpressions;
using Gtk;
using Gdk;
namespace pesco
{
	/// <summary>
	/// 
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PersonalDataPane : Gtk.Bin
	{
		public void SetNewUser (User user){
			this.campoNombre.Text= user.Name;
			this.campoApellidos.Text= user.Firstlastname;
			this.campoTelefono.Text= user.Phone;
			this.campoPoblacion.Text= user.City;
			
			if( user.Sex.CompareTo('m')==0)
				this.sexoMujer.Active=true;
			else
				this.sexoHombre.Active=true;
			
			this.dia.Text= user.BirthDate.Day.ToString();
			this.mes.Text= user.BirthDate.Month.ToString();
			this.anyo.Text= user.BirthDate.Year.ToString();
			
		
			
		}
		
		/// <summary>
		/// Recupera los datos personales introducidos en el componente
		/// </summary>
		/// <returns>
		/// Un nuevo <see cref="Usuario"/> con los datos introducidos
		/// </returns>
		public User GetNewUser(){
			
			User u = new User();
			
			//TODO validacion de los datos!!!!
			
			// Nombres
			u.Name = this.campoNombre.Text;
			if (u.Name == "")
			{
				MarkWidget(campoNombre);
				u.Ok = false;
			}
			// Apellidos
			u.Firstlastname = this.campoApellidos.Text;
			if (u.Firstlastname == "")
			{
				MarkWidget(campoApellidos);
				u.Ok = false;
			}
			
			// Población			
			u.City = this.campoPoblacion.Text;
			if (!Regex.Match(u.City, "^[A-Z a-z]*$").Success || u.City == "" )
			{
				MarkWidget(campoPoblacion);
				u.Ok = false;
			}
			
			// Teléfono
			/*u.Phone = this.campoTelefono.Text;
			if (!Regex.Match(u.Phone, @"[69][0-9]{8}").Success ||u.Phone == "")
			{
				MarkWidget(this.campoTelefono);
				u.Ok = false;
			}*/
			
			u.Phone = this.campoTelefono.Text;
			if (u.Phone == "")
			{
				MarkWidget(this.campoTelefono);
				u.Ok = false;
			}
			
			// Alias
			/*u.Alias = Util.ComputeHash(this.campoAlias.Text);
			
			if (campoAlias.Text == "")
			{
				MarkWidget(campoAlias);
				u.Ok = false;
			}*/
			
			// DNI
			//u.Dni = this.campoDNI.Text;
			/*if (!Regex.Match(u.Dni, @"^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][a-zA-Z]$").Success || u.Dni == "")
			{
				MarkWidget(campoDNI);
				u.Ok = false;
			}*/
			
			// sexo
			if(!this.sexoMujer.Active && !this.sexoHombre.Active){
				this.sexoMujer.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(0xff, 0xbb, 0xbb));
				this.sexoHombre.ModifyBg(Gtk.StateType.Normal, new Gdk.Color(0xff, 0xbb, 0xbb));
				MarkWidget(sexoMujer);
				u.Ok = false;
			}
			else{
				if (this.sexoMujer.Active)
					u.Sex = 'm';
				else if(this.sexoHombre.Active)
					u.Sex = 'h';
			}
			// fecha de nacimiento
			//TODO controlar que la fecha sea correcta. ¿Utilizar spin buttons?
			try{
				u.BirthDate = new DateTime(Int32.Parse(this.anyo.Text), Int32.Parse(this.mes.Text), Int32.Parse(this.dia.Text));
				if(u.BirthDate >= DateTime.Now){
					SystemException sysEx = new SystemException();
					throw( sysEx);
				}
				if(u.BirthDate.Year<1900){
					
					SystemException sysEx = new SystemException();
					throw( sysEx);
				}
			}
			catch(System.Exception e)
			{
				MarkWidget(this.dia);
				MarkWidget(this.mes);
				MarkWidget(this.anyo);
				
				dia.GrabFocus();
				
				u.Ok = false;
			}
			return u;
		}
		
		private static void MarkWidget(Gtk.Widget w)
		{
			w.GrabFocus();			
			w.ModifyBase(Gtk.StateType.Normal, new Gdk.Color(0xff, 0xbb, 0xbb));
		}
		
		/// <summary>
		/// Crea el panel para recoger los datos personales de un usuario en el proceso de registro
		/// </summary>
		public PersonalDataPane ()
		{
			this.Build ();			
			GtkUtil.SetStyle(this,Configuration.Current.MediumFont);
			
		}
		protected virtual void OnSexoHombreToggled (object sender, System.EventArgs e)
		{
			
			if(this.sexoHombre.Active){
				this.sexoMujer.Active =false;
				//this.sexoHombre.Active =true;
				this.sexoHombre.ModifyBg (StateType.Active, new Gdk.Color (0xff, 0xff,0x77));
				this.sexoHombre.ModifyBg (StateType.Prelight,new Gdk.Color (0xff, 0xff,0x77));
				this.sexoMujer.ModifyBg(StateType.Active);
				this.sexoMujer.ModifyBg(StateType.Prelight);
				this.sexoMujer.ModifyBg(Gtk.StateType.Normal);
				this.sexoHombre.ModifyBg(Gtk.StateType.Normal);
			}
			
		}
		
		protected virtual void OnSexoMujerToggled (object sender, System.EventArgs e)
		{
			if(this.sexoMujer.Active){
				this.sexoHombre.Active=false;
				this.sexoMujer.ModifyBg (StateType.Active, new Gdk.Color (0xff, 0xff,0x77));
				this.sexoMujer.ModifyBg (StateType.Prelight,new Gdk.Color (0xff, 0xff,0x77));
				this.sexoHombre.ModifyBg(StateType.Active);
				this.sexoHombre.ModifyBg(StateType.Prelight);
				this.sexoMujer.ModifyBg(Gtk.StateType.Normal);
				this.sexoHombre.ModifyBg(Gtk.StateType.Normal);
			}
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
		protected virtual void ValidateLettersKey (Gtk.WidgetEventArgs args)
		{
			
			
			if ((((Gdk.EventKey)args.Event).KeyValue < 65 || ((Gdk.EventKey)args.Event).KeyValue > 122) && 
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.BackSpace && 
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Delete && 
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Left && 
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Right &&
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Shift_L &&
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Shift_R &&
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.Shift_Lock &&			    
			    ((Gdk.EventKey)args.Event).Key!=Gdk.Key.space)
		    {
		        args.RetVal = true;
		    }

			
		}
		
				
		protected virtual void OnAnyoWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateKey(args);
		}
		
		protected virtual void OnMesWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateKey(args);
		}
		
		protected virtual void OnDiaWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateKey(args);
		}
		
		protected virtual void OnCampoTelefonoWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateKey(args);
		}
		
		protected virtual void OnCampoPoblacionWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateLettersKey(args);
		}
		
		protected virtual void OnCampoApellidosWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateLettersKey(args);
		}
		
		protected virtual void OnCampoNombreWidgetEvent (object o, Gtk.WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress)
				ValidateLettersKey(args);
		}
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
	
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
	}
}

