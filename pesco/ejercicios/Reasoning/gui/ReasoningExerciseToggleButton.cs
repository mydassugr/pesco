using System;
using Gtk;
using Gdk;


using System.Text;


using System.Diagnostics;

namespace pesco
{
	public class ReasoningExerciseToggleButton : Gtk.ToggleButton
	{
		string key;
		int position;
		bool isSolution;
		
		public string Key {
			get {
				return this.key;
			}
		}

		public int Position {
			get {return this.position;}
			set {position = value;}
		}

		public bool IsSolution {
			get {return this.isSolution;}
			set {isSolution = value;}
		}

		public ReasoningExerciseToggleButton (string k, int pos) : base()
		{
			key = k;
			position = pos;
			isSolution=false;
			
			ModifyBg (StateType.Active, new Gdk.Color (0xff, 0xff,0x77));
			
			this.Entered += delegate(object sender, EventArgs e) {
				
				(sender as ToggleButton).ModifyBg(StateType.Prelight);
				if ((sender as ToggleButton).Active && !isSolution){
					(sender as ToggleButton).ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff,0x77));
				}
				else{
					if(isSolution)
						IsSolutionButton();
				}
			};
			
			this.Clicked += delegate(object sender, EventArgs e) {
								
				(sender as ToggleButton).ModifyBg(StateType.Prelight);
				if ((sender as ToggleButton).Active && !isSolution){
					(sender as ToggleButton).ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff,0x77));
				}
				else{
					if(isSolution)
						IsSolutionButton();
						
				}
			};
				
			HBox hboxChild = new HBox();
			Gtk.Label btnLabel= new Gtk.Label(k); 
			GtkUtil.SetStyle(btnLabel, Configuration.Current.LargeFont);
						
			hboxChild.PackStart(btnLabel,true,true,0);		
		
			
			this.Child = hboxChild;
						
		}
		
		public ReasoningExerciseToggleButton (string k, string imageResourceName, int pos) : base()
		{
			key = k;
			Position = pos;
			isSolution=false;
			
			ModifyBg (StateType.Active, new Gdk.Color (0xff, 0xff,0x77));
				
			
			this.Entered += delegate(object sender, EventArgs e) {
				
								
				if ((sender as ToggleButton).Active && !isSolution){
					(sender as ToggleButton).ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff,0x77));
				}
				else{
					if(isSolution)
						IsSolutionButton();
				}
				
			};
			
			this.Clicked += delegate(object sender, EventArgs e) {
								
				if ((sender as ToggleButton).Active && !isSolution){
					(sender as ToggleButton).ModifyBg (StateType.Prelight, new Gdk.Color (0xff, 0xff,0x77));
				}
				else{
					if(isSolution)
						IsSolutionButton();
					
				}
				
			};
			
			HBox hboxChild = new HBox();
			//hboxChild.Add(new Gtk.Image(Gdk.Pixbuf.LoadFromResource(imageResourceName)));
			hboxChild.PackStart(new Gtk.Image(new Gdk.Pixbuf(imageResourceName)),true,true,0);		
		
			
			this.Child = hboxChild;
		}
		
		private void IsSolutionButton(){
			this.Active=false;
			this.FocusOnClick=false;
			this.CanFocus=false;
			ModifyBg (StateType.Active);
			ModifyBg (StateType.Prelight);
		}
		
		public void SetAsCorrect()
		{
			if(this.Children[0].GetType().ToString().Equals("Gtk.HBox")){
		
				if(((HBox)this.Children[0]).Children.GetLength(0) >1)
					((HBox)this.Children[0]).Children[1].Destroy();
			}
			
		}
		
		public void SetAsWrong()
		{
			if(this.Children[0].GetType().ToString().Equals("Gtk.HBox")){
		
				//Draw the croos lines
				ModifyBg (StateType.Normal);
				ModifyBg (StateType.Active);
				ModifyBg (StateType.Insensitive);
				
				HBox hboxChild= ((HBox)this.Children[0]);
				Gdk.Pixmap mDrawPixmap = new Gdk.Pixmap(null,this.Allocation.Width-10,this.Allocation.Height-10, 24);
				Gdk.GC mGraphicsContext = new Gdk.GC(mDrawPixmap);
				mGraphicsContext.RgbFgColor = new Color(255, 255, 255);
				
				mGraphicsContext.SetLineAttributes(1, Gdk.LineStyle.Solid, Gdk.CapStyle.Round, Gdk.JoinStyle.Bevel);
				
				mDrawPixmap.DrawRectangle(mGraphicsContext,true,0,0,this.Allocation.Width,this.Allocation.Height);
				
				mGraphicsContext = new Gdk.GC(mDrawPixmap);
				mGraphicsContext.RgbFgColor = new Color(255, 0, 0);
				mGraphicsContext.SetLineAttributes(10, Gdk.LineStyle.Solid, Gdk.CapStyle.Round, Gdk.JoinStyle.Bevel);
				mDrawPixmap.DrawLine(mGraphicsContext,0,0,this.Allocation.Width-10,this.Allocation.Height-10);
				mDrawPixmap.DrawLine(mGraphicsContext, this.Allocation.Width-10,0,0,this.Allocation.Height-10);
				
											
				
				// Create the image with red tick compositing original image and tick image
				Gdk.Pixbuf tick = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, true, 8,Allocation.Width-10,this.Allocation.Height-10);
				tick.Fill(0);
				//tick.AddAlpha(true,0,0,0);
				tick = tick.GetFromDrawable(mDrawPixmap,this.Colormap,0,0,0,0,Allocation.Width-10,this.Allocation.Height-10);
				
				Gdk.Pixbuf newPb = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, true, 8,Allocation.Width-10,this.Allocation.Height-10);
				newPb.Fill(0);	
				
			
				
				//Add the cross lines at the beggining of the hbox
				//((HBox)this.Children[0]).PackStart(new Gtk.Image(),false,false,0);
				((HBox)this.Children[0]).PackEnd(new Gtk.Image(),false,false,0);
				tick.Composite(newPb,0,0,tick.Width,tick.Height,
				               0,0,1,1,Gdk.InterpType.Bilinear,130);
				((Gtk.Image)((HBox)this.Children[0]).Children[1]).Pixbuf= newPb.Copy();
				
				//Change the hbox spacing
				//((HBox)this.Children[0]).Spacing=-1 *(((HBox)this.Children[0]).Children[0].Allocation.Width);
				((HBox)this.Children[0]).Spacing=-((HBox)this.Children[0]).Children[0].Allocation.Width;
				
				this.ShowAll();
			}
		}
	}
}

