
using System;

// Il pattern Singleton è utilizzato per eliminare la possibilità che una classe venga instanziata più volte.
// In pratica la rendiamo statica per non doverla creare dall'esterno, viene creata una sola volta se non esiste
// già. Pensa alla classe che gestisce i codici univoci di una banca: non possiamo permetterci il rischio di avere due 
// codici uguali.

namespace SingletonEasy
{


	//[Serializable]
	class MyClass
	{
		//Se un sistema è multitrhead potrebbe creare due instanze contemporaneamente
		// e allora in questi casi bisogna assicurarsi che una sola istanza venga creata
		//imponendo il passaggio da un solo thread. 
		//In questo ci aiuta C# con la seguente classe, e scegliamo noi se mettere o meno il
		//lock per l'oggetto

		static private readonly object forLok = new object();

		static private MyClass? instance = null;
		static public MyClass Instance
		{
			get
			{
				//per comandare il lock
				lock(forLok)
				{
					//qui dentro mettiamo chi passerà per il trhead critico
					// poi uscirà rilasciando il lock, durante il lock l'applicazione è più lenta.
					if (instance == null) instance= new MyClass();	
				}

				//sostituisce le due righe precedenti.

			return instance; 
				//return instance ??= new MyClass(); //null-coalescing
			}
		}
		private MyClass() { Console.WriteLine("Oggetto creato"); } 


		public void Metodo()
		{
			Console.WriteLine("Metodo richiamato");
		}

		

	}

	class Program
	{
		static void Main(string[] args)
		{
            //Mostra "Oggetto creato" solo con la prima chiamata a Metodo(), che 
            MyClass.Instance.Metodo();
            MyClass.Instance.Metodo();
        }

	}

}
