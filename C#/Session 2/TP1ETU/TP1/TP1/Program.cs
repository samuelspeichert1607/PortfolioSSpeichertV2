using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
  class Program
  {
    static void Main( string[] args )
    {
        Application app = new Application("TP1 - BoringTetris", 512, 768);   //768
      app.Run( );
    }
  }
}
