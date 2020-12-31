using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemple
{
  class Program
  {
    static void Main( string[] args )
    {
      Application app = new Application( "My app title", 1024, 768 );
      app.Run( );
    }
  }
}
