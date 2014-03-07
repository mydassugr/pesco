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
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

namespace pesco
{


	public class ListUtils
	{

		public ListUtils ()
		{
		}
		
		public static List<E> MixList<E>(List<E> inputList)
		{
		     List<E> randomList = new List<E>();
		
		     Random r = new Random();
		     int randomIndex = 0;
		     while (inputList.Count > 0)
		     {
		          randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
		          randomList.Add(inputList[randomIndex]); //add it to the new, random list
		          inputList.RemoveAt(randomIndex); //remove to avoid duplicates
		     }
		
		     return randomList; //return the new random list
		}
		
		public static List<T> Shuffle<T>(List<T> list)
		{
		    RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
		    int n = list.Count;
		    while (n > 1)
		    {
		        byte[] box = new byte[1];
		        do provider.GetBytes(box);
		        while (!(box[0] < n * (Byte.MaxValue / n)));
		        int k = (box[0] % n);
		        n--;
		        T value = list[k];
		        list[k] = list[n];
		        list[n] = value;
		    }
			
			return list;
		}
		
		public static T[] ShuffleArray<T>(T[] list)
		{
		    RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
		    int n = list.Length;
		    while (n > 1)
		    {
		        byte[] box = new byte[1];
		        do provider.GetBytes(box);
		        while (!(box[0] < n * (Byte.MaxValue / n)));
		        int k = (box[0] % n);
		        n--;
		        T value = list[k];
		        list[k] = list[n];
		        list[n] = value;
		    }
			
			return list;
		}
		
		public static int NextInt(int min, int max)
		{
		    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
		    byte[] buffer = new byte[4];
		    
		    rng.GetBytes(buffer);
		    int result = BitConverter.ToInt32(buffer, 0);
		
		    return new Random(result).Next(min, max);
		}
}

}

