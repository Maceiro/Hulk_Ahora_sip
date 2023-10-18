
  public class Symbol {

   public string Class { get; set; }
   public bool IsTerminal { get { return Data.gramatik.Terminals.Contains(this) ; } }
   public Symbol( string s ) { Class= s ; }
   public bool IsEpsilon { get{ return Class.Length==0 ; } }

   public override bool Equals( object other) {

   if( other is Symbol ) {

    Symbol s= (Symbol)(other) ;
    return this.Class==s.Class ;
   }

   else return false;
     
   }


  }


  public class Production {

   public Symbol Left { get; set; }
   public List<Symbol> Right{ get; set; }
   public bool IsEpsilon { get { return Right[0].Class.Length== 0 ; } }
   public Production( Symbol left, params Symbol[] right ) {

    Left= left;
    var aux= new List<Symbol>();
    for( int i=0; i< right.Length; i++ )
     aux.Add( right[i] );

    Right= aux;
   }
   
  }

  public class Gramatik {

   public List<Production>  Productions { get; set; }
   public List<Symbol> Terminals { get; set; }
   public List<Symbol> No_Terminals { get; set; }
   public Symbol Initial { get; set; }
   public Symbol Epsilon{ get; set; }
   
  }



  public class SymbolSet {

   public Dictionary<string, List<Symbol>> dicc;

   public void Add( Symbol s, List<Symbol> list ) { dicc.Add( s.Class, list );  }
   public List<Symbol> this[ Symbol s] { get{ return dicc[ s.Class] ; }  }
   public SymbolSet() { dicc= new Dictionary<string, List<Symbol>>();  }

  }



  public class Table {

   List< Terna> list;
   public void Add( Symbol key1, Symbol key2, Production p ) {

    list.Add( new Terna( key1, key2, p ));
   }

   public Production Search( Symbol key1, Symbol key2 ) {

    foreach( var terna in list ) 
     if( key1.Equals( terna.No_Terminal) && key2.Equals( terna.Terminal ) ) return terna.P ;

     return null;  

     }

     public Table() { list= new List<Terna>() ; }


  }



  public class Terna {

    public Symbol No_Terminal{ get; set; } 
    public Symbol Terminal{ get; set; } 
    public Production P { get; set; }  

    public Terna( Symbol key1, Symbol key2, Production p ) {
     if( key1.IsTerminal ) throw new Exception( "El primer parametro debe ser un no_terminal") ;
     if( !key2.IsTerminal ) throw new Exception( "El segundo parametro debe de ser un terminal") ;
     No_Terminal= key1;
     Terminal= key2;
     P = p;
    }

  }

  public class Symbol_Node  {

   public Symbol Symbol { get; set; }
   public Node Ref { get; set; }

   public Symbol_Node( Symbol s, Node r ) {
    Symbol= s;
    Ref= r;
   }

  }


  public class Node {

  public string Symbol { get; set; }
  public Node Parent { get; set; }
  public List<Node> Children { get; set; }
  public string Chain { get; set; }
  public Node( string symbol, Node parent ) {

    Symbol= symbol;
    Parent= parent;
    Children= new List<Node>();
  }

  }


  public static class List_Extensions {

   public static bool Add_Bool( this List<Symbol> list, Symbol s ) {

    for( int i= 0; i< list.Count; i++ ) 
     if( s.Equals( list[i]) ) return false;

     list.Add( s);
     return true;
   }

  
  public static bool Add_All( this List<Symbol> list, List<Symbol> items ) {

   bool Is_Copied= false;

   for( int i=0; i< items.Count; i++ ) 
    if( !list.Contains( items[i]) ) {
      Is_Copied= true;
      list.Add( items[i] );
   }
   return Is_Copied ;

  }

  public static bool Contains_Epsilon( this List<Symbol> list ) {

    for( int i=0; i< list.Count; i++ ) 
     if( list[i].Class.Length==0 ) return true;

     return false;
  }


  public static void Remove_Epsilon( this List<Symbol> list ) {

     int index= -1;
    for( int i= 0; i< list.Count; i++ )
     if( list[i].Class.Length==0 ) {
      index= i;
      break;
     }
      
      Console.WriteLine( "Realizado hasta aqui ");
     if( index>=0 ) list.RemoveAt(index) ;

  }

  public static bool Contains_Bool( this List<Symbol> list, Symbol s ) {

    for( int i=0; i< list.Count; i++ ) 
     if( list[i].Class== s.Class ) return true;

     return false;

  }



  }


