using System;
using Gdk;

namespace pesco
{
	public class WelcomeCloudImg
	{
		
		private const double FACTOR = 0.01;
		
		private int width, height;
		private GPosition position;
		private Gdk.Pixbuf img;

		public int Height {
			get {
				return this.height;
			}
			set {
				height = value;
			}
		}

		public Pixbuf Img {
			get {
				return this.img;
			}
			set {
				img = value;
			}
		}

		public GPosition Position {
			get {
				return this.position;
			}
			set {
				position = value;
			}
		}

		public int Width {
			get {
				return this.width;
			}
			set {
				width = value;
			}
		}

		public WelcomeCloudImg ()
		{

		}
		
		public void GenerateRandom( int positionX ) {
		
			img = new Pixbuf(Configuration.CommandDirectory+"/gui/welcome/img/cloud.png");
			
			Random r = new Random( DateTime.Now.Millisecond );
			int randomFactor = r.Next( 5, 15 );
			int newWidth = (int) ( img.Width * randomFactor * FACTOR );
			int newHeight = (int) ( img.Height * randomFactor * FACTOR );
						
			int newPositionY = r.Next( 0, 150 );
			position = new GPosition( positionX, newPositionY );
			
			img = img.ScaleSimple( newWidth, newHeight, Gdk.InterpType.Bilinear );
		}
		
		public void Update () {
		
			position.X -= 1;
			if ( position.x + img.Width <= 0 ) {
				position.x = 1000 + img.Width;	
			}			
			
		}
		
		public void Draw (Gdk.Pixmap pixmap, Gdk.GC gc ) {
			
			int auxSourceX = 0;
			pixmap.DrawPixbuf( gc, img, 0, 0, position.x, position.y, img.Width, img.Height, 0, 0, 0 );
			
		}
	}
}

