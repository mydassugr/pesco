
using System;
using Gtk;

namespace pesco
{
	
	/// <summary>
	/// 
	/// </summary>
	public partial class UserRegistrationDialog : Gtk.Bin
	{
		private int currentStep = 0;
		private User u = null;
		
		private ResponseType response;
		
		MaritalStatusPane psc = null;
		ResidencePane pr = null;
		EducationalLevelPane pne = null;
		LeisureActivitiesPane par = null;
		PersonalDataPane panelregistronuevousuario = null;
		BubblePanel bp;
		/// <summary>
		/// 
		/// </summary>
		public UserRegistrationDialog ()
		{
			
			this.Build ();
			u= User.Deserialize();
			if(u== null)
				u = new User();
			//Buttons Style
			GtkUtil.SetStyle(this.button144, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.buttonOk, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.buttonPre, Configuration.Current.ButtonFont);
			this.button144.Hide();
			this.buttonOk.Show();		
			
			SetStepPanel();
			

		}
		
		public void SetStep(){
			if( u.Phone != null)
				currentStep ++;
			if( u.MaritalStat !=MaritalStatus.error)
				currentStep ++;
			if(u.Lives !=LivesWith.error)
				currentStep ++;
			if(u.EducationalLevel !=Education.error)
				currentStep ++;
			
			if(currentStep >0)
				buttonPre.Sensitive =true;
		}
		public void SetStepPanel(){
			
			SetStep();
			if (currentStep == 0) {
				panelregistronuevousuario = new PersonalDataPane();
				hbox1.Add(panelregistronuevousuario);
				panelregistronuevousuario.Show();
				hbox1.Show();
			}
			else if (currentStep == 1){
				
				psc = new MaritalStatusPane();				
				GtkUtil.Put(hbox1, psc, 0);
				psc.Show(); 
				buttonPre.Sensitive= true;
				
			}
			else if (currentStep == 2){
				
				pr = new ResidencePane();				
				GtkUtil.Put(hbox1, pr, 0);
				pr.ShowAll(); 
			}
			else if (currentStep == 3)
			{
				pne = new EducationalLevelPane();				
				GtkUtil.Put(hbox1, pne, 0);
				pne.ShowAll(); 
			}
			else if (currentStep == 4)
			{
				par = new LeisureActivitiesPane();
				GtkUtil.Put(hbox1, par, 0);
				par.ShowAll(); 
				
				this.button144.Show();
				this.buttonOk.Hide();
			}
			
		}
		
		/// <summary>
		/// 
		/// </summary>
		public new ResponseType Response {
			get {
				return response;
			}
		}
		
		#region Eventos
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <param name="e">
		/// A <see cref="System.EventArgs"/>
		/// </param>
		protected virtual void OnNextClick (object sender, System.EventArgs e)
		{
			
			if (currentStep == 0) {
				u = this.panelregistronuevousuario.GetNewUser();
				
				if (u.Ok)
				{
					u.Serialize();
					this.hbox1.Remove(this.panelregistronuevousuario);
					
					if(psc == null)
						psc = new MaritalStatusPane();				
					if(u.MaritalStat != MaritalStatus.error)
						psc.SetMaritalStatus(u);
					
					GtkUtil.Put(hbox1, psc, 0);
					
					psc.Show(); 
					currentStep++;
					buttonPre.Sensitive= true;
				}
				else {
					MessageDialog md = new MessageDialog ( null, 
										DialogFlags.DestroyWithParent,
										MessageType.Error, 
										ButtonsType.Ok, "<span size=\"xx-large\">Repasa los datos  en rojo. Si tienes dudas, pide ayuda al monitor de la sala.</span>");
					
					GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
					this.Sensitive=false;
					md.Show();
					md.GdkWindow.Cursor= GtkUtil.ChangeCursor();
					ResponseType result = (ResponseType)md.Run ();	
														
					if(result == ResponseType.Ok){
						this.Sensitive=true;
					}
					md.Destroy ();
				}
			}
			else if (currentStep == 1){
				
				u.MaritalStat = psc.GetMaritalStatus();
				
				if(u.MaritalStat== MaritalStatus.error){
					MessageDialog md = new MessageDialog ( null, 
										DialogFlags.DestroyWithParent,
										MessageType.Error, 
										ButtonsType.Ok, "<span size=\"xx-large\">Debes pulsar una opción para continuar.</span>");
					
					GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
					this.Sensitive=false;
					md.Show();
					md.GdkWindow.Cursor= GtkUtil.ChangeCursor();
					ResponseType result = (ResponseType)md.Run ();	
														
					if(result == ResponseType.Ok){
						this.Sensitive=true;
					}
					md.Destroy ();
				}
				else{
					u.Serialize();
						
					hbox1.Remove(psc);
					
					if(pr == null)
						pr = new ResidencePane();				
					if(u.Lives != LivesWith.error)
						pr.SetLivesWith(u);
						
					GtkUtil.Put(hbox1, pr, 0);
					
					pr.ShowAll(); 
					currentStep++;
				}
			}
			else if (currentStep == 2){
				
				u.Lives = pr.GetLivesWith();
					
				if(u.Lives == LivesWith.error){
					MessageDialog md = new MessageDialog ( null, 
										DialogFlags.DestroyWithParent,
										MessageType.Error, 
										ButtonsType.Ok, "<span size=\"xx-large\">Debes pulsar una opción para continuar.</span>");
					
					GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
					this.Sensitive=false;
					md.Show();
					md.GdkWindow.Cursor= GtkUtil.ChangeCursor();
					ResponseType result = (ResponseType)md.Run ();	
														
					if(result == ResponseType.Ok){
						this.Sensitive=true;
					}
					md.Destroy ();
				}
				else{
					u.Serialize();
					hbox1.Remove(pr);
					
					if(pne == null)
						pne = new EducationalLevelPane();				
					if(u.EducationalLevel != Education.error)
						pne.SetEducationalLevel(u);
					GtkUtil.Put(hbox1, pne, 0);
					
					pne.ShowAll(); 
					currentStep++;
				}
			}
			else if (currentStep == 3)
			{
				u.EducationalLevel = pne.GetEducationalLevel();
				
				if(u.EducationalLevel == Education.error){
					MessageDialog md = new MessageDialog ( null, 
										DialogFlags.DestroyWithParent,
										MessageType.Error, 
										ButtonsType.Ok, "<span size=\"xx-large\">Debes pulsar una opción para continuar.</span>");
					
					GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
					this.Sensitive=false;
					md.Show();
					md.GdkWindow.Cursor= GtkUtil.ChangeCursor();
					ResponseType result = (ResponseType)md.Run ();	
														
					if(result == ResponseType.Ok){
						this.Sensitive=true;
					}
					md.Destroy ();
				}
				else{
					u.Serialize();
					hbox1.Remove(pne);
					
					if(par == null){
						par = new LeisureActivitiesPane();
					}
					if( u.ComputerHours != -1 || u.ExerciseHours != -1 || u.ReadingHours != -1 || u.WorkshopHours != -1)
						par.SetLeasureActivities(u);
					
					GtkUtil.Put(hbox1, par, 0);
					
					par.ShowAll(); 
					currentStep++;
					
					this.button144.Show();
					this.buttonOk.Hide();
				}
			}
			else if (currentStep == 4)
			{
				
			}	
		}
		protected virtual void OnButtonPreClicked (object sender, System.EventArgs e)
		{
			currentStep --;
			
			
						
			if (currentStep == 0) {
				if(this.panelregistronuevousuario ==null)
					this.panelregistronuevousuario= new PersonalDataPane();
				
				this.panelregistronuevousuario.SetNewUser(u);
				this.hbox1.Remove(psc);
				GtkUtil.Put(hbox1,this.panelregistronuevousuario,0);
				hbox1.ShowAll();
				buttonPre.Sensitive=false;
			}
			else if (currentStep == 1){
				
				if(this.psc ==null)
					psc = new MaritalStatusPane();
				this.psc.SetMaritalStatus(u);
				this.hbox1.Remove(pr);
				GtkUtil.Put(hbox1,this.psc,0);
				buttonPre.Sensitive=true;
			}
			else if (currentStep == 2){
				
				if(this.pr == null)
					pr= new ResidencePane();
				
				this.pr.SetLivesWith(u);
				this.hbox1.Remove(pne);
				GtkUtil.Put(hbox1,this.pr,0);
			}
			else if (currentStep == 3)
			{
				u.ReadingHours = par.GetReadingTime();
				u.WorkshopHours = par.GetWorkshopTime();
				u.ExerciseHours = par.GetExerciseTime();
				u.ComputerHours = par.GetComputerTime();
				par.MarkEntryAsNormal();
				
				if(this.pne ==null)
					pne = new EducationalLevelPane();
				
				this.pne.SetEducationalLevel(u);
				this.hbox1.Remove(par);
				GtkUtil.Put(hbox1,this.pne,0);
				
				this.button144.Hide();
				this.buttonOk.Show();
			}
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
		protected virtual void OnSaveClick(object sender, System.EventArgs e){
			
			u.ReadingHours = par.GetReadingTime();
			u.WorkshopHours = par.GetWorkshopTime();
			u.ExerciseHours = par.GetExerciseTime();
			u.ComputerHours = par.GetComputerTime();
			
			
			if(u.ReadingHours == -1 || u.WorkshopHours == -1 || u.ExerciseHours == -1 || u.ComputerHours == -1){
				MessageDialog md = new MessageDialog ( null, 
										DialogFlags.DestroyWithParent,
										MessageType.Error, 
										ButtonsType.Ok, "<span size=\"xx-large\">Repasa los datos  en rojo. Si tienes dudas, pide ayuda al monitor de la sala.</span>");
					
				GtkUtil.SetStyleRecursive( md, Configuration.Current.MediumFont );
				this.Sensitive=false;
				md.Show();
				md.GdkWindow.Cursor= GtkUtil.ChangeCursor();
				ResponseType result = (ResponseType)md.Run ();	
													
				if(result == ResponseType.Ok){
					this.Sensitive=true;
				}
				md.Destroy ();
			}
			else{
			
				u.Serialize();
				
				this.response = ResponseType.Ok;
				//this.Destroy();
				SessionManager.GetInstance().ExerciseFinished(-1);
				SessionManager.GetInstance().TakeControl();
			}
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
		protected virtual void OnCloseEvent (object sender, System.EventArgs e)
		{
			this.response = ResponseType.Close;
			this.Destroy();
		}
		
		#endregion
	}
}
