

using tp1;
using tp2;

namespace tpfinal
{

	class Estrategia
	{
        // Consulta1 (ArbolBinario< DecisionData > arbol): Retorna un texto con todas las 
        // posibles predicciones que puede calcular el árbol de decisión del sistema.

        public String Consulta1(ArbolBinario<DecisionData> arbol)
		{
			string resultado = "";

            if (arbol == null) return resultado;

			if (arbol.esHoja()) 
			{
                resultado += arbol.getDatoRaiz().ToString() + "\n";
			}

			else
			{

			
                if (arbol.getHijoIzquierdo != null)
                {
                    resultado += Consulta1(arbol.getHijoIzquierdo());
                }

                if (arbol.getHijoDerecho != null)
                {
                    resultado += Consulta1(arbol.getHijoDerecho());
                }

            }

            return resultado;
		}

		//Consulta2(ArbolBinario<DecisionData> arbol) : Retorna un texto que contiene todos
		//los caminos hasta cada predicción.

		public String Consulta2(ArbolBinario<DecisionData> arbol, string prefijo = "", bool hijoDerecho = false)
		{
			string resultado = "";

            resultado += prefijo + (hijoDerecho ? "└──" : "├── ") + arbol.getDatoRaiz().ToString() + Environment.NewLine;

			if (arbol.esHoja())
			{
				return resultado;
			}

			else
			{
				string nuevoPrefijo = prefijo + (hijoDerecho ? "  " : "│ ");

				if (arbol.getHijoIzquierdo() != null) { 
				    resultado += Consulta2(arbol.getHijoIzquierdo(), nuevoPrefijo, false ); 
				
				}

				if (arbol.getHijoDerecho() != null)
				{
                    resultado += Consulta2(arbol.getHijoDerecho(), nuevoPrefijo, true);

                }

            }

            return resultado;
        }




        // Consulta3(ArbolBinario<DecisionData> arbol) : Retorna un texto que contiene los
        // datos almacenados en los nodos del árbol diferenciados por el nivel en que se
        // encuentran.

        public String Consulta3(ArbolBinario<DecisionData> arbol)
        {
            string result = "";

            if (arbol == null) { return result; }

           
            Dictionary<int, List<String>> niveles = new Dictionary<int, List<String>>();
            Cola<(ArbolBinario<DecisionData>, int)> cola = new Cola<(ArbolBinario<DecisionData>, int)>();


            cola.encolar((arbol, 0));

            while (!cola.esVacia())
            {

                var (nodo, nivel) = cola.desencolar();

                if (!niveles.ContainsKey(nivel))
                {
                    niveles[nivel] = new List<String>();
                }

                niveles[nivel].Add(nodo.getDatoRaiz().ToString());


                if (nodo.getHijoIzquierdo() != null)
                {
                    cola.encolar((nodo.getHijoIzquierdo(), nivel + 1));
                }

                if (nodo.getHijoDerecho() != null)
                {
                    cola.encolar((nodo.getHijoDerecho(), nivel + 1));
                }

            }


        
            foreach ( var datos in niveles)
            {
                result += " Nivel" + datos.Key + ": " + "\n";

                List<String> nodos = datos.Value;

                foreach (var d in nodos)
                {
                    result +=  d + "\n";

                }

                result += "\n";

            }


            return result;
        }




        public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
		{

            if (clasificador.crearHoja()) 
			{
                // predicción
                return new ArbolBinario<DecisionData>(new DecisionData(clasificador.obtenerDatoHoja()));

            }
			else
			{
				// pregunta

                ArbolBinario<DecisionData> nodoDecision = new ArbolBinario<DecisionData>(new DecisionData(clasificador.obtenerPregunta()));

                if (clasificador.obtenerClasificadorIzquierdo != null)
                {
                    nodoDecision.agregarHijoIzquierdo(CrearArbol(clasificador.obtenerClasificadorIzquierdo()));
                }

                if (clasificador.obtenerClasificadorDerecho() != null)
                {
                    nodoDecision.agregarHijoDerecho(CrearArbol(clasificador.obtenerClasificadorDerecho()));
                }


                return nodoDecision;

            }

		}
	}
}