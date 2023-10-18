

  public class Context  {

   public Dictionary<string, object> variables ;
   public Dictionary<string, KeyValuePair<int, Def_Func>> functions ;
   public Context Parent ;

   public Context() {
    
    variables= new Dictionary<string, object>();
    functions= new Dictionary<string, KeyValuePair<int, Def_Func>>();
    
   }

   public Context Create_Chield() {

     var result = new Context() ;
     result.Parent= this ;
     return result ;

  }
   
   public bool Is_Defined( string variable ) {  return variables.ContainsKey( variable) || ( Parent!= null &&  Parent.Is_Defined( variable) )  ;  }

   public bool Is_Defined( string function, int args ) {  return ( functions.ContainsKey( function) && functions[ function ].Key== args )  ||  ( Parent!= null &&  Parent.Is_Defined( function, args )  )  ;    }


   public bool Define( string variable ) {

    if( variables.ContainsKey( variable) ) return false ;
    variables.Add( variable, null );
    return true ;

   }

   public bool Define( string function, int args, Def_Func node ) {

   if( functions.ContainsKey( function) ) return false ;
   functions[ function ]= new KeyValuePair<int, Def_Func>( args, node ) ;
   return true ;

   }

   public bool Define( string variable, Object value ) {

    bool truth= variables.ContainsKey( variable ) ;
    variables[variable]= value ;
    return !truth ;

   }

   public object Obtain_Value( string variable ) { 
    
    if( variables.ContainsKey( variable )) return variables[variable] ;  
     if( Parent== null ) return null ;
    else return Parent.Obtain_Value( variable );
    
    }

   public Def_Func Obtain_Node( string function, int args ) {  

    if( functions.ContainsKey( function) && functions[function].Key== args ) return functions[function].Value ;
    if( Parent== null ) return null ;
    else return Parent.Obtain_Node( function, args );

    }

  }

  
  public class List_Node<T> : Expr_Or_Stat {

    public List<T> list ;
    public List_Node( T expr )  { 

      var result= new List<T>() ;
      result.Add( expr );
      list= result ;

      }

    public List_Node() { list= new List<T>();  }

    public List_Node( T expr, List<T> list_expr )  {

     var result= new List<T>() ;
     result.Add( expr );
     for( int i=0; i< list_expr.Count; i++ )
      result.Add( list_expr[i] ); 

    }

   public List<T> Descompress() { return list ;  } 

   public void Add( T item ) {  list.Insert( 0, item ) ;  }

   public override Bool_Object Validate( Context context, bool Validate_Only ) { return new Bool_Object( true, null ) ;  }

  }


  public class Bool_Object {

   public bool Bool ;
   public object Object ;

   public Bool_Object( bool truth, object obj ) {

    Bool= truth ;
    Object= obj ;
   }

  public bool Same_Type( Bool_Object other ) {

   if( ( Object is string && other.Object is string ) || ( Object is double && other.Object is double ) || ( Object is bool && other.Object is bool ) ) return true ;
   else return false ;
   
  }

  }

  
 
    

  

