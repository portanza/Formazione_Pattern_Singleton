
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
        #region CLASSE LAZY - serve ad inizializzare proprietà statiche e metodi statici quando serve, quando chiamate
        //Se un sistema è multitrhead potrebbe creare due instanze contemporaneamente
        // e allora in questi casi bisogna assicurarsi che una sola istanza venga creata
        //imponendo il passaggio da un solo thread. 
        //In questo ci aiuta C# con la seguente classe, e scegliamo noi se mettere o meno il
        //lock per l'oggetto


        //Prima viene inizializzato le proprietà statiche che potrebbero servire al costruttore
        //poi il costruttore, poi eventuali metodi statici. Se non vogliamo che questi metodi statici
        //vengano eseguiti automaticamente usiamo la classe Lazy scrivendo
        static private readonly Lazy<int> valore = new Lazy<int>(() => Sum(1, 2));
        static public int Valore => valore.Value;
        static public int Sum(int a, int b)
        {
            Console.WriteLine($"Esecuzione Sum {a} + {b}");
            return a + b;
        }

        

        #endregion

        //Quindi usanndo la classe Lazy (pigro) il sistema sa che deve ritardarne l'assegnazione.
        static private readonly object forLok = new object();

        static private MyClass? instance = null;
        static public MyClass Instance
        {

            get
            {
                //dopo che un trhead ha ottenuto un lock, occorre fare attenzione a non fare un lock dopo aver interrotto il precedente.
                // In questo modo siamo sicuri che avverrà un solo lock 
                if (instance is null)
                //per comandare il lock
                lock (forLok)
                {
                    //qui dentro mettiamo chi passerà per il trhead critico
                    // poi uscirà rilasciando il lock, durante il lock l'applicazione è più lenta.
                    //chi inizia il lock non sarà interrotto.
                    if (instance is null) instance = new MyClass();
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

            int c = MyClass.Sum(5, 3);

            // solo sollecitando l'attributo Valore il sistema legge la variabile valore e poi Sum. 
            int d = MyClass.Valore;
        }

    }

}

