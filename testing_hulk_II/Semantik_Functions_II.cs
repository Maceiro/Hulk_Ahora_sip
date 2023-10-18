
public class Assignment : Statement {

    public ID Variable ;
    public Expr_Or_Stat Expr ;

    public Assignment( ID variable, Expr_Or_Stat expr ) {

      Variable= variable ;
      Expr= expr ;
    }

    public override Bool_Object Validate( Context context, bool Validate_Only )  {  
       
       var pair= Expr.Validate( context, Validate_Only ); 
       if( !pair.Bool) return new Bool_Object( false, null );

       if( Validate_Only ) context.Define( Variable.Name );
       else context.Define( Variable.Name, pair.Object );

       return new Bool_Object( true, null );
 
     }


   } 

  public class If_Else : Statement  {

   public Boolean_Expression Condition ;
   public Expr_Or_Stat if_part ; 
   public Expr_Or_Stat else_part ;

   public If_Else( Boolean_Expression condition, Expr_Or_Stat if_part, Expr_Or_Stat else_part ) {

    Condition= condition ;
    this.if_part= if_part ;
    this.else_part= else_part ;
    
   }

   public override Bool_Object Validate( Context context, bool Validate_Only ) {
    
    if( Validate_Only ) return new Bool_Object( Condition.Validate( context, true ).Bool && if_part.Validate( context, true ).Bool && else_part.Validate( context, true ).Bool, null ) ; 
    
    var pair= Condition.Validate( context, Validate_Only ) ;
    if( !pair.Bool ) return new Bool_Object( false, null ) ;

    var value= (bool)pair.Object ;
    if( value ) return if_part.Validate( context, Validate_Only );
    else return else_part.Validate( context, Validate_Only ) ;

   }
    
  }

  public class Print : Statement  {

   public Expr_Or_Stat Arg ;

   public Print( Expr_Or_Stat arg ) { Arg= arg ; }

   public override Bool_Object Validate( Context context, bool Validate_Only ) { 

     if( Validate_Only ) return Arg.Validate( context, true ) ;  
     
     var pair= Arg.Validate( context, Validate_Only );
     if( !pair.Bool ) return new Bool_Object( false, null ) ;

     if( pair.Object!= null ) Print_in_Console( pair.Object );
     return new Bool_Object( true, pair.Object ) ;

      }

    public static void Print_in_Console( object obj ) {  Console.WriteLine(obj) ;   } 

  }

  public class Def_Func : Statement {

    public ID Name ;
    public List<ID> Args ;
    public Expr_Or_Stat Body ;

    public Def_Func( ID name, List<ID> args, Expr_Or_Stat body ) {

     Name= name ;
     Args= args ;
     Body= body ; 

    }

    public override Bool_Object Validate( Context context, bool Validate_Only ) { 

    var chield= context.Create_Chield() ;
    for( int i=0 ; i< Args.Count; i++ ) 
     chield.Define( Args[i].Name ) ;
     chield.Define( Name.Name, Args.Count, null );

     var pair= Body.Validate( chield, true ) ;
     if( !pair.Bool) return new Bool_Object( false, null ) ;

     if( !context.Define( Name.Name, Args.Count, this ) ) {
      Operation_System.Print_in_Console( "Semantik Error!! :  Ya existe una funcion con el nombre " + Name.Name );
      return new Bool_Object( false, null ) ;
     }
     else return new Bool_Object( true, null );

    }
    

    public Bool_Object Evaluation( Context context, List<Expr_Or_Stat> list ) {
     
     if( Args.Count!= list.Count ) return new Bool_Object( false, null ) ;

     Context chield= context.Create_Chield() ;
     for( int i=0; i< list.Count; i++ ) {

      var pair= list[i].Validate( context, false ) ;
      if( !pair.Bool ) return new Bool_Object( false, null ) ;
      else chield.Define( Args[i].Name, pair.Object );

     }

     return Body.Validate( chield, false );

    }


  }


  public class Condition : Boolean_Expression {
    
    public Expr_Or_Stat Left ;
    public Expr_Or_Stat Right ;
    

    public Condition( Expr_Or_Stat left, string op, Expr_Or_Stat right ) : base( null, op, null ) {
     
     Left= left ;
     Right= right ;
    
    }

    public override Bool_Object Validate( Context context, bool Validate_Only ) {  
      
      if( Validate_Only ) return new Bool_Object( Left.Validate( context, true ).Bool && Right.Validate( context, true ).Bool, null ) ; 

      var pair1= Left.Validate( context, Validate_Only ) ;
      var pair2= Right.Validate( context, Validate_Only ) ;
      if( !pair1.Bool || !pair2.Bool || pair1.Object== null || pair2.Object== null )  return new Bool_Object( false, null ) ;

      if( !pair1.Same_Type( pair2 ) ) {

       Operation_System.Print_in_Console( "Semantik Error :  Los tipos de los operando no coinciden") ;
       return new Bool_Object( false, null ) ;
      }

       if( ( ( pair1.Object is string ) || ( pair1.Object is bool ) ) && Op!="==" ) {

       Operation_System.Print_in_Console( "Semantik Error :  En los tipos string y boolean solo pueden establecerse comparaciones de igualdad o desigualdad ") ;
       return new Bool_Object( false, null ) ;        
       }
      
      if( pair1.Object is string ) return new Bool_Object( true, ( pair1.Object as string )==( pair2.Object as string ) ) ;
      if( pair1.Object is bool ) return new Bool_Object( true,  pair1.Object.ToString() == pair2.Object.ToString() ) ;

      var value1= (double)pair1.Object ;
      var value2= (double)pair2.Object ;
      
      bool result= false;

      switch( Op ) {

       case"==": 
       result= value1==value2 ;
       break;
       case">=": 
       result= value1>=value2 ;
       break;
       case"<=": 
       result= value1<=value2 ;
       break;
       case"<": 
       result= value1<value2 ;
       break;
       case">": 
       result= value1>value2 ;
       break;

      }  
      
      return new Bool_Object( true, result );
      
      }

  }


   public class Boolean_Expression : Expr_Or_Stat {

    public Boolean_Expression Left_Expr ;
    public Boolean_Expression Right_Expr ;
    public string Op ;

    public Boolean_Expression( Boolean_Expression left, string op, Boolean_Expression right ) {

     Left_Expr= left ;
     Right_Expr= right ;
     Op= op ;

    }

    public override Bool_Object Validate( Context context, bool Validate_Only ) {

     if( Validate_Only ) return new Bool_Object( Left_Expr.Validate( context, true ).Bool && Right_Expr.Validate( context, true ).Bool, null ) ;

     var pair1= Left_Expr.Validate( context, Validate_Only ) ;
     var pair2= Right_Expr.Validate( context, Validate_Only ) ;
     if( !pair1.Bool || !pair2.Bool ) return new Bool_Object( false, null ) ;
     if( !(pair1.Object is bool) || !(pair2.Object is bool ) ) return new Bool_Object( false, null ) ;

     bool op1= (bool)pair1.Object ;
     bool op2= (bool)pair2.Object ;
     bool result= false ;
     switch( Op ) {

      case "&":
      result= op1 && op2 ;
      break ;
      case "|":
      result= op1 || op2 ;
      break ;

     }

     return new Bool_Object( true, result ) ;

    }


   }

