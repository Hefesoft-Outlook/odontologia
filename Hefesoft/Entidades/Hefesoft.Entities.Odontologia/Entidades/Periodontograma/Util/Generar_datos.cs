using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontologia.Periodontograma.Util
{
    public class Generar_datos
    {
        public ObservableCollection<Entidades.PeriodontogramaEntity> inicializarDatos()
        {
            ObservableCollection<Entidades.PeriodontogramaEntity> LstPeriodontogramaTodos = new ObservableCollection<Entidades.PeriodontogramaEntity>();

            //Parte 1
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 18, Parte = Enumeradores.Parte.parte1, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, FurcaVisualizacion = Enumeradores.Furca_Visualizacion.Visible_Un_Elemento });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 17, Parte = Enumeradores.Parte.parte1, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, FurcaVisualizacion = Enumeradores.Furca_Visualizacion.Visible_Un_Elemento });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 16, Parte = Enumeradores.Parte.parte1, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, FurcaVisualizacion = Enumeradores.Furca_Visualizacion.Visible_Un_Elemento });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 15, Parte = Enumeradores.Parte.parte1, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 14, Parte = Enumeradores.Parte.parte1, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 13, Parte = Enumeradores.Parte.parte1, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 12, Parte = Enumeradores.Parte.parte1, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 11, Parte = Enumeradores.Parte.parte1, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });

            //Parte 2
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 21, Parte = Enumeradores.Parte.parte2, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 22, Parte = Enumeradores.Parte.parte2, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 23, Parte = Enumeradores.Parte.parte2, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 24, Parte = Enumeradores.Parte.parte2, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 25, Parte = Enumeradores.Parte.parte2, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 26, Parte = Enumeradores.Parte.parte2, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 27, Parte = Enumeradores.Parte.parte2, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 28, Parte = Enumeradores.Parte.parte2, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba });

            //Parte 3
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 21, Parte = Enumeradores.Parte.parte3, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 22, Parte = Enumeradores.Parte.parte3, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 23, Parte = Enumeradores.Parte.parte3, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 24, Parte = Enumeradores.Parte.parte3, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 25, Parte = Enumeradores.Parte.parte3, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 26, Parte = Enumeradores.Parte.parte3, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 27, Parte = Enumeradores.Parte.parte3, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 28, Parte = Enumeradores.Parte.parte3, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });

            //Parte 4
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 18, Parte = Enumeradores.Parte.parte4, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b, FurcaVisualizacion = Enumeradores.Furca_Visualizacion.Visible_Dos_Elementos });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 17, Parte = Enumeradores.Parte.parte4, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b, FurcaVisualizacion = Enumeradores.Furca_Visualizacion.Visible_Dos_Elementos });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 16, Parte = Enumeradores.Parte.parte4, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b, FurcaVisualizacion = Enumeradores.Furca_Visualizacion.Visible_Un_Elemento });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 15, Parte = Enumeradores.Parte.parte4, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 14, Parte = Enumeradores.Parte.parte4, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 13, Parte = Enumeradores.Parte.parte4, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 12, Parte = Enumeradores.Parte.parte4, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 11, Parte = Enumeradores.Parte.parte4, Arriba_Abajo = Enumeradores.Arriba_Abajo.arriba, Cara = Enumeradores.Cara.b });

            //Parte 5
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 48, Parte = Enumeradores.Parte.parte5, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 47, Parte = Enumeradores.Parte.parte5, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 46, Parte = Enumeradores.Parte.parte5, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 45, Parte = Enumeradores.Parte.parte5, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 44, Parte = Enumeradores.Parte.parte5, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 43, Parte = Enumeradores.Parte.parte5, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 42, Parte = Enumeradores.Parte.parte5, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 41, Parte = Enumeradores.Parte.parte5, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });

            //Parte 6
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 31, Parte = Enumeradores.Parte.parte6, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 32, Parte = Enumeradores.Parte.parte6, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 33, Parte = Enumeradores.Parte.parte6, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 34, Parte = Enumeradores.Parte.parte6, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 35, Parte = Enumeradores.Parte.parte6, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 36, Parte = Enumeradores.Parte.parte6, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 37, Parte = Enumeradores.Parte.parte6, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 38, Parte = Enumeradores.Parte.parte6, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo });

            //Parte 7
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 48, Parte = Enumeradores.Parte.parte7, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 47, Parte = Enumeradores.Parte.parte7, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 46, Parte = Enumeradores.Parte.parte7, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 45, Parte = Enumeradores.Parte.parte7, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 44, Parte = Enumeradores.Parte.parte7, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 43, Parte = Enumeradores.Parte.parte7, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 42, Parte = Enumeradores.Parte.parte7, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 41, Parte = Enumeradores.Parte.parte7, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });

            //Parte 8
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 31, Parte = Enumeradores.Parte.parte8, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 32, Parte = Enumeradores.Parte.parte8, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 33, Parte = Enumeradores.Parte.parte8, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 34, Parte = Enumeradores.Parte.parte8, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 35, Parte = Enumeradores.Parte.parte8, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 36, Parte = Enumeradores.Parte.parte8, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 37, Parte = Enumeradores.Parte.parte8, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });
            LstPeriodontogramaTodos.Add(new Entidades.PeriodontogramaEntity() { Numero = 38, Parte = Enumeradores.Parte.parte8, Arriba_Abajo = Enumeradores.Arriba_Abajo.abajo, Cara = Enumeradores.Cara.b });

            return LstPeriodontogramaTodos;
            
        }

    }
}
