/**

Copyright 2011 Grupo de Investigación GEDES
Lenguajes y sistemas informáticos
Universidad de Granada

Licensed under the EUPL, Version 1.1 or – as soon they 
will be approved by the European Commission - subsequent  
versions of the EUPL (the "Licence"); 
You may not use this work except in compliance with the 
Licence. 
You may obtain a copy of the Licence at: 

http://ec.europa.eu/idabc/eupl  

Unless required by applicable law or agreed to in 
writing, software distributed under the Licence is 
distributed on an "AS IS" basis, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
express or implied. 
See the Licence for the specific language governing 
permissions and limitations under the Licence. 



*/
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;

namespace pesco
{


	public class ExerciseGiftsShopping : Exercise
	{

		public static string xmlUserFile = "giftsshopping.xml";
		private static ExerciseGiftsShopping myOwnInstance;
		private PanelVacio myPanel = null;
		private ExerciseDemoGiftsShopping panelDemo = null;
		
		[XmlIgnore]
		public GS_CriteriaManager criteriaManager = null;
		[XmlIgnore]
		public GS_ItemManager itemManager = null;
		[XmlIgnore]
		public GS_ShopManager shopManager = null;
		[XmlIgnore]
		public GS_PersonManager personManager = null;
		
		// Results
		// private List <GS_Results> results = null;
		GiftsShoppingResults results = XmlUtil.DeserializeForUser<GiftsShoppingResults>(Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "GiftsShopping.xml");
		
		private int[] criteriasByLevel = new int[3] {4, 6, 9};
		private int[] shopsByLevel = new int[3] {6, 12, 15};
		
		private GS_PanelCriterias currentPanelCriterias = null;
		private GS_PanelSituation currentPanelSituation = null;
		private GS_PanelShops currentPanelShops = null;
		private GS_PanelMyShopping currentPanelMyShopping = null;		
		private List<GS_PanelItemsShop> currentPanelItemsShop = null;
		
		private GS_Situation currentSituation = null;
		
		#region Timers variables
		private int auxAnimationTime;
		private uint auxTimer = 0;
		private GLib.TimeoutHandler	currentHandler;
		private uint currentInterval;
		private bool pausedExercise = false;
		#endregion

		public int CurrentLevel {
			get {
				return results.CurrentLevel;
			}
			set {
				results.CurrentLevel = value;
			}
		}
		
		[XmlIgnore]
		public GS_Situation CurrentSituation {
			get {
				return currentSituation;
			}
			set {
				currentSituation = value;
			}
		}
		
		public GiftsShoppingResults Results {
			get {
				return results;
			}
			set {
				results = value;
			}
		}

		public ExerciseGiftsShopping ()
		{
			category = ExerciseCategory.Planification;
		}
		
		// Function Singleton
		public static ExerciseGiftsShopping getInstance() {
		
			if ( myOwnInstance != null )
				return myOwnInstance;
			else
			{
				myOwnInstance = new ExerciseGiftsShopping();
				return myOwnInstance;
			}
		}

		public override void finalizar ()
		{
			if ( !pausedExercise )
				pausa();
			
			myOwnInstance = null;
		}
		
		public override int idEjercicio ()
		{
			return 16;
		}
		
		public override bool inicializar ()
		{					
			myPanel = new PanelVacio();
			
			// Init panels
			panelDemo = new ExerciseDemoGiftsShopping();
			myPanel.Add( panelDemo );
			SessionManager.GetInstance().ReplacePanel(myPanel);
			myPanel.ShowAll();
			
			// Init results
			ExerciceExecutionResult<SingleResultGiftsShopping> giftsShoppingExecutionResult = 
					new ExerciceExecutionResult<SingleResultGiftsShopping>(
						SessionManager.GetInstance().CurSession.IdSession, SessionManager.GetInstance().CurExecInd );
			results.GiftsShoppingExecutionResults.Add( giftsShoppingExecutionResult );
			
			// Set category and exercise Id
			if ( Results.CategoryId == 0 || Results.ExerciseId == 0 ) {
				Results.CategoryId = Convert.ToInt16(ExerciseCategory.Attention);
				Results.ExerciseId = this.idEjercicio();				
			}

			// Init demo and notify session manager
			SessionManager.GetInstance().ChangeExerciseStatus("demo");
			panelDemo.InitPanel();
			
			itemManager = XmlUtil.Deserialize<GS_ItemManager>("/ejercicios/GiftsShopping/xml-templates/gs-items.xml" );
			personManager = XmlUtil.Deserialize<GS_PersonManager>("/ejercicios/GiftsShopping/xml-templates/gs-people.xml" );
			shopManager = XmlUtil.Deserialize<GS_ShopManager>("/ejercicios/GiftsShopping/xml-templates/gs-shops.xml");
			criteriaManager = XmlUtil.Deserialize<GS_CriteriaManager>("/ejercicios/GiftsShopping/xml-templates/gs-criterias.xml");
						
			return true;
		}
		
		public override void iniciar ()
		{
			
		}
		
		public void NewSituation ()
		{

			CreateSituation();
			currentPanelMyShopping = new GS_PanelMyShopping();
			currentPanelSituation = new GS_PanelSituation();
			currentPanelSituation.AddSituation( currentSituation );
			currentPanelCriterias = new GS_PanelCriterias();
			currentPanelCriterias.AddSituation( currentSituation );
			currentPanelShops = new GS_PanelShops();
			currentPanelShops.AddSituation( currentSituation );
			currentPanelItemsShop = new List<GS_PanelItemsShop>();
			
			for ( int i = 0; i < currentSituation.Shops.Count; i++ ) {
			
				GS_PanelItemsShop auxPanelItemsShop = new GS_PanelItemsShop();
				auxPanelItemsShop.AddShop( i, currentSituation.Shops[i] );
				currentPanelItemsShop.Add( auxPanelItemsShop );
				
			}
			
			ShowSituation();
			ShowCriterias();
			
			if ( auxTimer == 0 ) {
				currentHandler = new GLib.TimeoutHandler (UpdatePanels);
				currentInterval = 1000;
				auxTimer = GLib.Timeout.Add ( currentInterval, currentHandler);
			}
		}
		
		private void CreateSituation() {
			
			List <GS_Criteria> selectedCriterias = new List<GS_Criteria>();
			List <GS_Shop> selectedShops = new List<GS_Shop>();
			
			List <GS_Criteria> auxAvalaibleCriterias = new List<GS_Criteria>(criteriaManager.Criterias);
			List <GS_Shop> auxAvalaibleShops = new List<GS_Shop>(shopManager.Shops);
			
			int finalPrice = 0;
			int finalBudget = 0;
			// Choose criterias according to level and removing avalaible shops from criterias
			Random r = new Random();
			for ( int i = 0; i < criteriasByLevel[CurrentLevel]; i++ ) {
				// Get random criteria
				GS_Criteria auxCriteria = auxAvalaibleCriterias[r.Next(0,auxAvalaibleCriterias.Count)];
				// Get item of criteria
				GS_Item auxItemSelected = itemManager.GetItem( auxCriteria.Item );
				// Remove criterias with the same shop
				int auxCounter = auxAvalaibleCriterias.Count;
				int j = 0;
				while ( j < auxCounter ) {
					GS_Item auxItem2 = itemManager.GetItem( auxAvalaibleCriterias[j].Item );
					if ( auxItemSelected.Shop == auxItem2.Shop ) {
						auxAvalaibleCriterias.Remove(auxAvalaibleCriterias[j]);
						auxCounter = auxAvalaibleCriterias.Count;						
					} else {
						j++;	
					}
				}
				
				// Remove shop of the criterion from avalaibles
				auxAvalaibleShops.Remove( shopManager.GetShop( (itemManager.GetItem(auxCriteria.Item).Shop ) ) );
				
				// Get random cheap price
				int auxCheapPrice = auxItemSelected.CheapPriceMin + 
						ListUtils.NextInt( 0, (auxItemSelected.CheapPriceMax - auxItemSelected.CheapPriceMin) / 5 ) * 5;

				// Get random expensive price
				int auxExpensivePrice = auxItemSelected.ExpensivePriceMin + 
						ListUtils.NextInt( 0, (auxItemSelected.ExpensivePriceMax -auxItemSelected.ExpensivePriceMin) / 5 ) * 5;
				
				auxCriteria.Price = auxCheapPrice;
				finalPrice += auxCheapPrice;
				// Add criteria to the selected list
				selectedCriterias.Add( auxCriteria );
				// Filling shop				
				GS_Shop auxShop = shopManager.GetShop(auxItemSelected.Shop);
				auxShop.Items.Clear();
				auxShop.Items = new List<GS_Item>();
				// Add cheap and expensive version of the Item
				auxItemSelected.Similarity = 1;
				auxItemSelected.FinalPrice = auxCheapPrice;
				auxShop.Items.Add( new GS_Item(auxItemSelected) );
				auxItemSelected.FinalPrice = auxExpensivePrice;
				auxShop.Items.Add( new GS_Item(auxItemSelected) );
				// Add other items by similarity and random prices
				auxShop.FillShopBySimilarity( itemManager.GetItemsOfShop(auxItemSelected.Shop), auxItemSelected );
				// Add filled shop
				selectedShops.Add( auxShop );
			}			
			
			// Get final budget
			finalBudget = ( (finalPrice / 50) + 1 ) * 50;
						
			// Add extra shops
			for ( int i = selectedShops.Count; i < shopsByLevel[CurrentLevel]; i++ ) {
				GS_Shop auxShop = auxAvalaibleShops[r.Next(0, auxAvalaibleShops.Count)];
				auxAvalaibleShops.Remove( auxShop );
				auxShop.Items.Clear();
				auxShop.FillShop( itemManager.GetItemsOfShop( auxShop.Id) );
				selectedShops.Add( auxShop );				
			}
					
			selectedShops = ListUtils.Shuffle( selectedShops );
			
			currentSituation = new GS_Situation(selectedCriterias, selectedShops, finalBudget);
			
		}
		
		public bool UpdatePanels() {
						
			if ( pausedExercise )
				return false;
			
			CurrentSituation.TimeUsed += 1;			
			currentPanelSituation.UpdateTimer();
			return true;
			
		}
		
		public override string NombreEjercicio ()
		{
			return "Compra de regalos";
		}
		
		public override void pausa ()
		{
			if ( !pausedExercise ) {
				pausedExercise = true;
				GLib.Source.Remove( auxTimer );
				auxTimer = 0;
			} else {
				pausedExercise = false;
				auxTimer = GLib.Timeout.Add( currentInterval, currentHandler );	
			}
		}

		public void GoToShops() {
			currentPanelSituation.ReplaceView( currentPanelShops );
			myPanel.ShowAll();
		}

		public void ShowSituation() {
		
			if ( myPanel.Children.Length > 0 ) {
				myPanel.Remove( myPanel.Children[0] );
			}
			myPanel.Add( currentPanelSituation );
			myPanel.ShowAll();
			
		}
		
		public void ShowCriterias() {
		
			currentPanelSituation.ReplaceView( currentPanelCriterias );
			myPanel.ShowAll();
			
		}
		
		public void ShowShops() {
		
			currentPanelSituation.ReplaceView( currentPanelShops );
			myPanel.ShowAll();
			
		}
		
		public void BuyItem(int idShop, int idItem) {			
			
			currentSituation.Shoppingcart.Add( currentSituation.Shops[idShop].Items[idItem] );
			currentSituation.Budgetused += currentSituation.Shops[idShop].Items[idItem].FinalPrice;
			
			currentPanelSituation.SetBudget( currentSituation.Budgetused );
		}
		
		public void DropItem(int idShop, int idItem) {

			currentSituation.Shoppingcart.Remove( currentSituation.Shops[idShop].Items[idItem] );
			currentSituation.Budgetused -= currentSituation.Shops[idShop].Items[idItem].FinalPrice;
			
			currentPanelSituation.SetBudget( currentSituation.Budgetused );
		}
		
		public void DropItemFromCart(int idInCart) {
			// Look for item in the shopping cart
			GS_Item auxItem = currentSituation.Shoppingcart[idInCart];
			
			// Look for shop with the item
			for ( int j = 0; j < CurrentSituation.Shops.Count; j++ ) {
				if ( CurrentSituation.Shops[j].Id == auxItem.Shop ) {
					// Removing item bought label from shop
					currentPanelItemsShop[j].RemoveBought( auxItem );
				}
			}
			// Updating budget
			currentSituation.Budgetused -= auxItem.FinalPrice;			
			currentPanelSituation.SetBudget( currentSituation.Budgetused );
			
			// Removing item from cart			
			currentSituation.Shoppingcart.Remove(currentSituation.Shoppingcart[idInCart]);
		}
		
		public void ShowMyShopping() {
			
			currentPanelMyShopping.UpdatePanel();
			currentPanelSituation.ReplaceView( currentPanelMyShopping );
			myPanel.ShowAll();
			
		}
		
		public void ShowItemsShop( int shop ) {
						
			currentPanelSituation.ReplaceView( currentPanelItemsShop[shop] );
			myPanel.ShowAll();
			
		}
		
		public void FinishShopping() {
		
			if ( auxTimer != null ) {
				GLib.Source.Remove( auxTimer );
				auxTimer = 0;
			}
			
			int auxAciertos = 0;
			double auxSimilarity = 0;
			// Check and save similarity reached
			for ( int i = 0; i < currentSituation.Shoppingcart.Count; i++ ) {
				GS_Item auxItem = currentSituation.Shoppingcart[i];
				auxSimilarity += auxItem.Similarity;
				if ( auxItem.Similarity == 1 )
					auxAciertos += 1;
				
			}
			
			// TODO: Check budget to give medals
			auxSimilarity = (int) auxSimilarity * ( 100 / criteriasByLevel[CurrentLevel] );
					
			SingleResultGiftsShopping auxResult = new SingleResultGiftsShopping ( CurrentSituation.Criterias, 
			                          				CurrentSituation.Shoppingcart, 
			                                       CurrentSituation.Budget,
			                                       CurrentSituation.Budgetused,
			                                       CurrentSituation.TimeUsed,
			                                       (int) auxSimilarity,
			                                       auxAciertos);
			
			Results.setResult ( auxResult );
			Results.CurrentExecution.TotalTimeElapsed += CurrentSituation.TimeUsed;
			
			if ( auxSimilarity > 80 ) {
				if ( CurrentLevel < 2 ) {
					CurrentLevel++;
				}
			}
			
			Serialize();
			SessionManager.GetInstance().RepetitionFinished();
			if ( SessionManager.GetInstance().HaveToFinishCurrentExercise() ) {				
				SessionManager.GetInstance().ExerciseFinished( (int) auxSimilarity );
				pausa();
				SessionManager.GetInstance().TakeControl();
				finalizar();
			} else {
				PodiumPanel auxMedalsPanel = new PodiumPanel( (int) auxSimilarity );
				auxMedalsPanel.BalloonText = "¡Enhorabuena! Has completado la compra y has obtenido una medalla de...";
				myPanel.Remove( currentPanelSituation );				
				currentPanelSituation.Destroy();
				currentPanelItemsShop.Clear();
				currentPanelCriterias.Destroy();
				myPanel.Add( auxMedalsPanel );
				auxMedalsPanel.ShowAll();
				auxMedalsPanel.ButtonOK.Label = "Realizar otra compra de regalos";
				GtkUtil.SetStyle( auxMedalsPanel.ButtonOK, Configuration.Current.MediumFont );
				auxMedalsPanel.ButtonOK.Clicked += delegate {
					myPanel.Remove( auxMedalsPanel );
					NewSituation();
					auxMedalsPanel.Destroy();
				};
				// NewSituation();
			}
			
		}
		
		public void Serialize(){
			
			XmlUtil.SerializeForUser<GiftsShoppingResults>( results, Configuration.Current.GetExerciseConfigurationFolderPath () + Path.DirectorySeparatorChar + "GiftsShopping.xml" );
			
			/*
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath()+ "/" + xmlUserFile;

			XmlTextWriter escritor = new XmlTextWriter(fullPath, null);
		
			try
			{
				XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
				
				namespaces.Add("","");
								
				escritor.Formatting = Formatting.Indented;				
				escritor.WriteStartDocument();				
				escritor.WriteDocType("giftsshopping-exercise", null, null, null);
				
				XmlSerializer serializer = new XmlSerializer(typeof(ExerciseGiftsShopping));
				serializer.Serialize(escritor, this, namespaces);
				
				escritor.WriteEndDocument();
				escritor.Close();
			}
			catch(Exception e)
			{
				escritor.Close();
				Console.WriteLine("Error al serializar" +  e.Message);
			}
			*/
		}	
	
		public static ExerciseGiftsShopping Deserialize()
		{
			ExerciseGiftsShopping exercise = new ExerciseGiftsShopping();
			myOwnInstance = exercise;
			return exercise;
			/*
			string fullPath = Configuration.Current.GetExerciseConfigurationFolderPath() + "/" + xmlUserFile;
			
			if (!File.Exists(fullPath))
			{
				string s = Environment.CommandLine;			
				fullPath = Configuration.CommandDirectory + "/ejercicios/GiftsShopping/xml-templates/" + xmlUserFile;
				Console.WriteLine("Full path: " + fullPath);
			}
			
			XmlTextReader lector = new XmlTextReader(fullPath);
			try
			{	
				ExerciseGiftsShopping exercise = new ExerciseGiftsShopping();
				
				XmlSerializer serializer = new XmlSerializer(typeof(ExerciseGiftsShopping));				
				exercise = (ExerciseGiftsShopping) serializer.Deserialize(lector);
				
				myOwnInstance = exercise;
				
				lector.Close();				
				return exercise;
			}
			catch( Exception e)
			{
				Console.WriteLine( e.ToString() );
				lector.Close();
				return null;
			}
			*/
		}
		
	}
}

