using System;
using System.Collections.Generic;
using Gtk;
using Gdk;

namespace pesco
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class TrialPanel : Gtk.Bin
	{
		BotonObjetoClasificable selected = null;
		SortedDictionary<string, Table> categoriesBoxList = new SortedDictionary<string, Table>();
		SortedDictionary<string, Point> categoriesTablePositions = new SortedDictionary<string, Point>();
		SortedDictionary<string, bool[]> categoriesTablePositions2 = new SortedDictionary<string, bool[]>();
		
		
		public TrialPanel (List<string> categories, List<string> objects)
		{
			this.Build ();
           
			vbox1.ShowAll();
			TaskListExercise.Shuffle<string>(objects);
			
            //Crear tabla de imagenes
			uint fil=0, col=0;
			
			BotonObjetoClasificable b2;
			foreach (string s in objects) {
				b2 = new BotonObjetoClasificable(s);
				
				b2.Clicked += delegate(object sender, EventArgs e) {
					// if the sender is a BotonObjetoClasificable, as it may be...
					if ((sender as BotonObjetoClasificable).Active) {
                   		selected = sender as BotonObjetoClasificable;
						selected.ModifyBg(StateType.Prelight, new Gdk.Color(0x1c, 0x0c, 0xf9));
						// unselect all the buttons but the last selected
						foreach (BotonObjetoClasificable wb in objectsBox)
							if (!wb.Equals (sender)){
                               wb.Active = false;
                               wb.ModifyBg(StateType.Prelight, new Gdk.Color(0xd0, 0xcf, 0xed));
                               //wb.ModifyBg(StateType.Prelight, new Gdk.Color(0xff, 0xff,0xff));
                        }
								
					}
                    else{
                       (sender as BotonObjetoClasificable).ModifyBg(StateType.Prelight, new Gdk.Color(0xd0, 0xcf, 0xed));
                    }
				};
			
				objectsBox.Attach(b2, col, col+1, fil, fil+1,AttachOptions.Fill|AttachOptions.Expand,AttachOptions.Fill|AttachOptions.Expand,0,0);
            
				col++;
				if (col == 8)
				{
					col = 0;
					fil++;
				}
			}
			
			//Crear tabla de categorias
			uint numCat=0;
			foreach(string s in categories)
			{
                numCat++;
				categoriesTablePositions.Add(s, new Point(0,0));
				categoriesTablePositions2.Add(s, new bool[4]);
				if (numCat==1){
                    buttonc1.Label=s;
                    buttonc1.Clicked+=new EventHandler(OnButtoncClicked );
                    buttonc1.ModifyBg(StateType.Normal, new Gdk.Color(0xc8, 0x0a, 0x1c));
                    buttonc1.ModifyBg(StateType.Active, new Gdk.Color(0xc8, 0x0a, 0x1c));
                    buttonc1.ModifyBg(StateType.Prelight, new Gdk.Color(0xf3, 0xb6, 0xbc));
                    GtkUtil.SetStyle(buttonc1, Configuration.Current.MediumFont);
                    categoriesBoxList.Add(s, tablec1);
                    eventboxc1.ButtonPressEvent+=new ButtonPressEventHandler(OnButtoncClicked);
                    eventboxc1.Name=s;
            	   
                }
                if (numCat==2){
                    buttonc2.Label=s;
                    buttonc2.Clicked+=new EventHandler(OnButtoncClicked );
                    buttonc2.ModifyBg(StateType.Normal, new Gdk.Color(0x21, 0x99, 0xe0));
                    buttonc2.ModifyBg(StateType.Active, new Gdk.Color(0x21, 0x99, 0xe0));
                    buttonc2.ModifyBg(StateType.Prelight, new Gdk.Color(0xd8, 0xee, 0xfb));
                    GtkUtil.SetStyle(buttonc2, Configuration.Current.MediumFont);
                    categoriesBoxList.Add(s, tablec2);
                    eventboxc2.ButtonPressEvent+=new ButtonPressEventHandler(OnButtoncClicked);
                    eventboxc2.Name=s;
                }
                if (numCat==3){
                    buttonc3.Label=s;
                    buttonc3.Clicked+=new EventHandler(OnButtoncClicked );
                    buttonc3.ModifyBg(StateType.Normal, new Gdk.Color(0xf9, 0x4b, 0x0f));
                    buttonc3.ModifyBg(StateType.Active, new Gdk.Color(0xf9, 0x4b, 0x0f));
                    buttonc3.ModifyBg(StateType.Prelight, new Gdk.Color(0xf4, 0xbb, 0xae));
                    GtkUtil.SetStyle(buttonc3, Configuration.Current.MediumFont);
                    categoriesBoxList.Add(s, tablec3);
                    eventboxc3.ButtonPressEvent+=new ButtonPressEventHandler(OnButtoncClicked);
                    eventboxc3.Name=s;
                }
                if (numCat==4){
                    buttonc4.Label=s;
                    buttonc4.Clicked+=new EventHandler(OnButtoncClicked );
                    buttonc4.ModifyBg(StateType.Normal, new Gdk.Color(0x3d, 0xe9, 0x15));
                    buttonc4.ModifyBg(StateType.Active, new Gdk.Color(0x3d, 0xe9, 0x15));
                    buttonc4.ModifyBg(StateType.Prelight, new Gdk.Color(0xdb, 0xf6, 0xd5));
                    GtkUtil.SetStyle(buttonc4, Configuration.Current.MediumFont);
                    categoriesBoxList.Add(s, tablec4);
                    eventboxc4.ButtonPressEvent+=new ButtonPressEventHandler(OnButtoncClicked);
                    eventboxc4.Name=s;
                }
                categoriesBox.ShowAll();
			}
			
		//	GtkUtil.SetStyle(this.button1, Configuration.Current.ButtonFont);
			GtkUtil.SetStyle(this.buttonNext, Configuration.Current.ButtonFont);
			//this.labelPauseText.ModifyFont(Pango.FontDescription.FromString("Ahafoni CLM Bold 10"));
            
            this.ShowAll();
		}
		
		public  SortedDictionary<string, List<string>> GetUserClassification()
		{
			SortedDictionary<string, List<string>> res = new SortedDictionary<string, List<string>>();
			
			foreach (string s in categoriesBoxList.Keys)
			{
				Table t = categoriesBoxList[s];
				List<string> objects = new List<string>();
				foreach(BotonObjetoClasificable b in t.Children)
					objects.Add(b.ResourceName);
					
				res.Add(s, objects);
			}
			
			return res;
		}
        
        public  void ShowExplanation(){
            explanation.Markup = "<b>Pulsa una imagen para seleccionarla y luego Pulsa</b> sobre el nombre de su categoría</b>";
        //    imagepepe.PixbufAnimation= new Gdk.PixbufAnimation (Configuration.CommandDirectory+"/ejercicios/resources/img/reasoning/pepehabla.gif");                
        }
       
        
            //Al hacer click en la etiqueta de categoria
  protected virtual void OnButtoncClicked (object sender, System.EventArgs e)
        { 
            string cat ="";
            if (sender is EventBox)
                cat=(sender as EventBox).Name;
            else cat = (sender as Button).Label;
            int CountCat=0;
            foreach(BotonObjetoClasificable bOC in categoriesBoxList[cat]){
                  CountCat++;
            }
            if (CountCat>=4)
                return;
            
            if (selected != null ){                      
                selected.Sensitive = false;
               
                uint x = (uint)categoriesTablePositions[cat].X;
                uint y = (uint)categoriesTablePositions[cat].Y;
                BotonObjetoClasificable j = new BotonObjetoClasificable(selected.ResourceName);
                
                //Al hacer Click en la imagen de la categoria
                j.Clicked += delegate(object sender2, EventArgs e2) {
                            if ((sender2 as BotonObjetoClasificable) == null) return;
                            
                            foreach (BotonObjetoClasificable w in objectsBox)
                                if (w.ResourceName == (sender2 as BotonObjetoClasificable).ResourceName){
                                   w.Sensitive = true;
                                   w.Active=false;
                                   w.ModifyBg(StateType.Prelight, new Gdk.Color(0xd0, 0xcf, 0xed));
                                   
                        }
                            
                            categoriesBoxList[cat].Remove((sender2 as BotonObjetoClasificable));
                            
                            List<BotonObjetoClasificable> restantes = new List<BotonObjetoClasificable>();
                            
                            foreach(BotonObjetoClasificable bOC in categoriesBoxList[cat]){
                                restantes.Add(bOC);
                                categoriesBoxList[cat].Remove(bOC);
                            }
                            
                            uint x2=0, y2=0;
                            foreach(BotonObjetoClasificable bOC in restantes)
                            {   
                                categoriesBoxList[cat].Attach(bOC, x2, x2+1, y2, y2+1,AttachOptions.Fill|AttachOptions.Expand,AttachOptions.Fill|AttachOptions.Expand,5,5);
                                
                                x2++;
                                
                                if (x2 == 2)
                                {
                                    y2++;
                                    x2 = 0;
                                }
                                                           
                            }
                            
                            categoriesTablePositions[cat] = new Point((int)x2, (int) y2);
                    
                };//Fin del delegate
                    
                categoriesBoxList[cat].Attach(j, x, x+1, y, y+1,AttachOptions.Fill|AttachOptions.Expand,AttachOptions.Fill|AttachOptions.Expand,5,5);
               // Console.WriteLine("({0},{1})", x, y);
                
                x++;
                if (x == 2)
                {
                    y++;
                    x = 0;
                }
                
                categoriesTablePositions[cat] = new Point((int)x, (int)y);
                
                categoriesBox.ShowAll();
                selected = null;
            }
        }
        
      		public Button ButtonNext{
			get	{
				return this.buttonNext;	
			}
		}
	}
	
	
}

