
using System;

// Il pattern Singleton è utilizzato per eliminare la possibilità che una classe venga instanziata più volte.
// In pratica la rendiamo statica per non doverla creare dall'esterno, viene creata una sola volta se non esiste
// già. Pensa alla classe che gestisce i codici univoci di una banca: non possiamo permetterci il rischio di avere due 
// codici uguali.

namespace SingletonEasy
{
	class MyClass
	{

		static private MyClass? instance = null;
		static public MyClass Instance
		{
			get
			{
				//	if (instance == null) instance= new MyClass();	
				//	return instance; }	
				//sostituisce le due righe precedenti.

				return instance ??= new MyClass(); //null-coalescing
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
