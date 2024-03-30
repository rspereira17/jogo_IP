using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace testeFinal
{
    internal class Jogador
    {


       
      
        public int Id { get; set; }
        public string Name { get; set; }
        public char Simbolo { get; private set; }

        public int Victories { get; set; }

        public int jogadasEspecias;

        public Jogador(int id, string name, char simbolo)
        {
            Id = id;
            Simbolo=simbolo;
            Name = name;
            Victories = 0;
            jogadasEspecias = 1;
           
        }
      


    }
}
