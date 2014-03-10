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
using Gdk;
using System;
using System.Collections.Generic;

namespace pesco
{

	static class CARCONSTS
	{
    	public const int NW = 0;
    	public const int N = 1;
		public const int NE = 2;
		public const int W = 3;
		public const int E = 4;
		public const int SW = 5;
		public const int S = 6;
		public const int SE = 7;
		
		public const int STOPPED = 0;
		public const int MOVING = 1;
		public const int MOVINGSTEP1 = 1;
		public const int MOVINGSTEP2 = 2;
		public const int MOVINGSTEP3 = 3;
		
		public const int DISTANCEBYSTEPX = 4;
		public const int DISTANCEBYSTEPY = 4;
		
		public const int MAXITEMS = 3;
		
		public const int MAXFUEL = 30;
	}

	public class PD_Car
	{

		public int WIDTH = 125;
		public int HEIGHT = 125;
		private Gdk.Pixbuf currentCar = null;
		private Gdk.Pixbuf [] carImages = new Gdk.Pixbuf[8];		
		private PD_Position position = new PD_Position(120,120);
		
		private PD_Position origin = new PD_Position();
		private PD_Position destination = new PD_Position();
		
		private int state;
		private int movingState;
		private int currentDirection = CARCONSTS.E;
		private int currentPlace = 0;
		private int currentFuel = CARCONSTS.MAXFUEL;
		private int originPlace = 0;
		private int destinationPlace = 0;
		
		private List <int> items = new List<int>(CARCONSTS.MAXITEMS);
		
		public PD_Position Position {
			get {
				return position;
			}
			set {
				position = value;
			}
		}
		
		
		public Gdk.Pixbuf CurrentCar {
			get {
				return currentCar;
			}
			set {
				currentCar = value;
			}
		}
				
		public int CurrentPlace {
			get {
				return currentPlace;
			}
			set {
				currentPlace = value;
			}
		}
				
		public int State {
			get {
				return state;
			}
			set {
				state = value;
			}
		}		
		
		public List<int> Items {
			get {
				return items;
			}
			set {
				items = value;
			}
		}
		
		public int CurrentFuel {
			get {
				return currentFuel;
			}
			set {
				currentFuel = value;
			}
		}
		
		
		public PD_Car ()
		{
			currentPlace = 0;
			for ( int i = 0; i < 8; i++ ) {			
				carImages[i] = Gdk.Pixbuf.LoadFromResource( "pesco.ejercicios.PackageDelivering.img.car.orangevan"+i+".png" );
				carImages[i] = carImages[i].ScaleSimple( WIDTH, HEIGHT, Gdk.InterpType.Bilinear );
			}
			currentCar = carImages[CARCONSTS.E];
			state = CARCONSTS.STOPPED;
		}
		
		public void MoveTo ( int place, PD_Position destination ) {
			
			originPlace = CurrentPlace;
			CurrentPlace = place;
			destinationPlace = place;
			this.origin.X = position.X;
			this.origin.Y = position.Y;
			this.destination.X = destination.X;			
			this.destination.Y = destination.Y;
			state = CARCONSTS.MOVING;
			movingState = CARCONSTS.MOVINGSTEP1;
			if ( currentFuel > 0 )
				currentFuel--;
			
		}
		
		public bool UpdatePosition() {

			if ( state == CARCONSTS.MOVING ) {
				// Console.WriteLine( "Origin place: "+originPlace+" | Destination place: "+destinationPlace+" | State: "+this.movingState+" Going: "+currentDirection+" | Destino: "+destination.X+","+destination.Y+" | Origen: "+origin.X+","+origin.Y+" | Actual: "+position.X+","+position.Y );			
				// Move X to intersection
				if ( movingState == CARCONSTS.MOVINGSTEP1 ) {
					if ( destination.X == origin.X ) {
						CurrentCar = carImages[CARCONSTS.W];
						if ( originPlace == 11 && destinationPlace == 15 ) {
							position.X += CARCONSTS.DISTANCEBYSTEPX;
							CurrentCar = carImages[CARCONSTS.E];
						} else if ( originPlace == 15 && destinationPlace == 11 ) {
							position.X += CARCONSTS.DISTANCEBYSTEPX;
							CurrentCar = carImages[CARCONSTS.E];
						} else if ( originPlace == 9 && destinationPlace == 13 ) {
							movingState = CARCONSTS.MOVINGSTEP2;
							currentDirection = CARCONSTS.S;
							CurrentCar = carImages[CARCONSTS.S];
						} else if ( originPlace == 13 && destinationPlace == 9 ) {
							movingState = CARCONSTS.MOVINGSTEP2;
							currentDirection = CARCONSTS.N;
							CurrentCar = carImages[CARCONSTS.N];
						} else {
							position.X -= CARCONSTS.DISTANCEBYSTEPX;
						}
					}
					else if ( destination.X > origin.X ) {
						CurrentCar = carImages[CARCONSTS.E];
						position.X += CARCONSTS.DISTANCEBYSTEPX;
					} else if ( destination.X < origin.X ) {
						CurrentCar = carImages[CARCONSTS.W];
						position.X -= CARCONSTS.DISTANCEBYSTEPX;
					}
					if ( movingState == CARCONSTS.MOVINGSTEP1 && 
					    Math.Abs( position.X - origin.X ) > 60 ) {
						movingState = CARCONSTS.MOVINGSTEP2;				
					
					#region Change image before start next moving
					// North-West
					if ( destination.X < origin.X && destination.Y < origin.Y ) {
						currentDirection = CARCONSTS.NW;
						currentCar = carImages[CARCONSTS.NW];
					} 
					// North
					else if ( destination.X == origin.X && destination.Y < origin.Y ) {
						currentDirection = CARCONSTS.N;
						currentCar = carImages[CARCONSTS.N];
					} 
					// North-East
					else if ( destination.X > origin.X && destination.Y < origin.Y ) {
						currentDirection = CARCONSTS.NE;
						currentCar = carImages[CARCONSTS.NE];
					} 
					// West
					else if ( destination.X < origin.X && destination.Y == origin.Y ) {
						currentDirection = CARCONSTS.W;
						currentCar = carImages[CARCONSTS.W];
					} 
					// East
					else if ( destination.X > origin.X && destination.Y == origin.Y ) {
						// Console.WriteLine("Hola");
						currentDirection = CARCONSTS.E;
						currentCar = carImages[CARCONSTS.E];
					} 
					// South-West
					else if ( destination.X < origin.X && destination.Y > origin.Y ) {
						currentDirection = CARCONSTS.SW;
						currentCar = carImages[CARCONSTS.SW];
					} 
					// South
					else if ( destination.X == origin.X && destination.Y > origin.Y ) {
						currentDirection = CARCONSTS.S;
						currentCar = carImages[CARCONSTS.S];
					}
					// South-East
					else if ( destination.X > origin.X && destination.Y > origin.Y ) {
						currentDirection = CARCONSTS.SE;
						currentCar = carImages[CARCONSTS.SE];
					}
					#endregion
					}
				} 
				// Move X and Y to destiny
				else if ( movingState == CARCONSTS.MOVINGSTEP2 ) {
					// 8 Ways
					// North-West
					if ( currentDirection == CARCONSTS.NW) {
						position.X -= CARCONSTS.DISTANCEBYSTEPX;
						position.Y -= CARCONSTS.DISTANCEBYSTEPY;
					} 
					// North
					else if ( currentDirection == CARCONSTS.N) {
						position.Y -= CARCONSTS.DISTANCEBYSTEPY;
					} 
					// North-East
					else if ( currentDirection == CARCONSTS.NE) {
						position.X += CARCONSTS.DISTANCEBYSTEPX;
						position.Y -= CARCONSTS.DISTANCEBYSTEPY;
					} 
					// West
					else if ( currentDirection == CARCONSTS.W) {
						position.X -= CARCONSTS.DISTANCEBYSTEPX;
					} 
					// East
					else if ( currentDirection == CARCONSTS.E) {
						position.X += CARCONSTS.DISTANCEBYSTEPX;
					} 
					// South-West
					else if ( currentDirection == CARCONSTS.SW) {
						position.X -= CARCONSTS.DISTANCEBYSTEPX;
						position.Y += CARCONSTS.DISTANCEBYSTEPY;
					} 
					// South
					else if ( currentDirection == CARCONSTS.S) {
						position.Y += CARCONSTS.DISTANCEBYSTEPY;
					}
					// South-East
					else if ( currentDirection == CARCONSTS.SE) {
						position.X += CARCONSTS.DISTANCEBYSTEPX;
						position.Y += CARCONSTS.DISTANCEBYSTEPY;
					}
					
					if ( Math.Abs(position.X - destination.X) < 65 && Math.Abs(position.Y - destination.Y) < 30 ) {
						movingState = CARCONSTS.MOVINGSTEP3;
						position.Y = destination.Y;
					}
										
				} 
				// Move X to destiny
				else if ( movingState == CARCONSTS.MOVINGSTEP3 ) {
					if ( position.X > destination.X ) {
						CurrentCar = carImages[CARCONSTS.W];
						position.X -= CARCONSTS.DISTANCEBYSTEPX;
					} else {
						CurrentCar = carImages[CARCONSTS.E];
						position.X += CARCONSTS.DISTANCEBYSTEPX;
					}
					if ( Math.Abs( position.X - destination.X ) < 6 ) {
						state = CARCONSTS.STOPPED;
						position.X = destination.X;
					}
				}
				// True will be returned cause the car is moving
				return true;
			} else {
				// False will be returned cause the car is stopped
				return false;
			}
			
		}
		
	}
}

