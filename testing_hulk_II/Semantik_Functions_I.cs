
public abstract class Semantik_Node {
 
  public abstract Bool_Object Validate( Context context, bool Validate_Only ) ;

}

 public class Program_Node : Semantik_Node {

  public  List<Expr_Or_Stat> lines;

  public Program_Node() { lines= new List<Expr_Or_Stat>() ;  }

  public override Bool_Object Validate( Context context, bool Validate_Only ) {      
    
    List<object> results= new List<object>() ;
    for( int i= 0; i< lines.Count; i++ ) {

    var pair= lines[i].Validate( context, Validate_Only ) ;
    if( !pair.Bool )   return new Bool_Object( false, null ) ;
    else if( pair.Bool!= null) results.Add( pair.Object );

    }

    for( int i= 0; i< results.Count; i++ )
     Console.WriteLine( results[i]) ;

    return new Bool_Object( true, results ) ;

  }

 }


 public abstract class Expr_Or_Stat : Semantik_Node {}
 public abstract class Statement : Expr_Or_Stat {} ;
 public abstract class Expression : Expr_Or_Stat {} ;
 
 
 public class Binary_Operation : Expression {

  public Expr_Or_Stat Left;
  public Expr_Or_Stat Right;
  public string Op; 

   public Binary_Operation( Expr_Or_Stat left, string op, Expr_Or_Stat right ) {

    Left= left ;
    Op= op ;
    Right= right ;

   }

   
   public override Bool_Object Validate( Context context, bool Validate_Only ) { 

     if( Validate_Only ) return new Bool_Object( Left.Validate( context, true ).Bool && Right.Validate( context, true ).Bool, null ) ;

     var pair_left= Left.Validate(context, Validate_Only ); 
     var pair_right= Right.Validate(context, Validate_Only ) ; 
     if( !( pair_left.Bool && pair_right.Bool )  ||  ( pair_left.Object== null || pair_right.Object== null ) )  return new Bool_Object( false, null );

      if( !pair_left.Same_Type( pair_right ) ) {

        Operation_System.Print_in_Console("Semantik Error!! : Los operandos deben de ser del mismo tipo");
        return new Bool_Object( false, null );
      }

      if( pair_left.Object is bool ) {

      Operation_System.Print_in_Console("Semantik Error!! : Los operandos no pueden ser de tipo boolean") ;
        return new Bool_Object( false, null );
      }

      if ( Op!="+" && ( pair_left.Object is string ) ) {

        Operation_System.Print_in_Console("Semantik Error!! : El unico operador aritmetico que puede utilizarse entre strings es el de suma") ;
        return new Bool_Object( false, null );
      }

      if( pair_left.Object is string )  return new Bool_Object( true, string.Concat( ( pair_left.Object as string), ( pair_right.Object as string ) ) ) ;
      
      double op1= (double)pair_left.Object ;
      double op2= (double)pair_right.Object ;
     
      double result= 0 ; 
      switch( Op) {

        case"+": 
        result= op1+op2 ;
        break;
        case"-":
        result= op1-op2 ;
        break;
        case"/":
        result= op1/op2;
        break;
        case"*":
        result= op1*op2;
        break;
        case"^":
        result= Math.Pow( op1, op2) ;
        break;
        case"%":
        result= op1%op2;
        break;

      }

      return new Bool_Object( true, result );
     }
  
 }

  public class Func_Call : Expression  {

   public ID Name; 
   public List< Expr_Or_Stat > args;

   public Func_Call( ID name, List<Expr_Or_Stat> args ) {

    Name= name ;
    this.args= args ;

   }

  public override Bool_Object Validate( Context context, bool Validate_Only ) {  
    
     if( Validate_Only ) {
    
     for( int i=0; i< args.Count; i++ )
      if( !args[i].Validate( context, true ).Bool ) return new Bool_Object( false, null );
      
      bool truth= context.Is_Defined( Name.Name, args.Count ) ;
      if( !truth) Operation_System.Print_in_Console( "Semantik Error!! :  La funcion " + Name.Name + " no ha sido definida" );
      return  new Bool_Object( truth, null ); 

      }

     if( !context.Is_Defined( Name.Name, args.Count ) ) {
      
      Operation_System.Print_in_Console( "Semantik Error!! :  La funcion " + Name.Name + " no ha sido definida" ) ;
      return new Bool_Object( false, null ) ;
     }

     Def_Func function= context.Obtain_Node( Name.Name, args.Count );
     return function.Evaluation( context, args ) ;
   }

  }

  public class ID : Expression  {
  
   public string Name ; 

   public ID( string name ) { Name= name ; }

   public override Bool_Object Validate( Context context, bool Validate_Only ) { 
    
    bool truth= context.Is_Defined( Name) ;
    if( !truth ) Operation_System.Print_in_Console( "Semantik Error!! : La variable " + Name + " no se encuentra definida " );

    if( Validate_Only ) return new Bool_Object( truth, null ) ;

    return new Bool_Object( truth, context.Obtain_Value( Name ) );  
    
     }

  }


  public class Number : Expression  {

   public string Value ;

   public Number( string value ) { Value= value ; }


   public override Bool_Object Validate( Context context, bool Validate_Only ) { 
    
    if( Validate_Only ) return new Bool_Object( true, null );
    
    double result;
    double.TryParse( Value, out result );
    return new Bool_Object( true, result ) ;
 
    }

  }

   
   public class Boolean : Expression {

    public bool Value ;
    public Boolean( string s ){

      bool value= false ;
      if( s=="true") value= true ;
      else value= false ;
      Value= value ;

    }

    public override Bool_Object Validate( Context context, bool Validate_Only ) {

      if( Validate_Only ) return new Bool_Object( true, null ) ;
      return new Bool_Object( true, Value ) ;

    }

   }
   

  public class String : Expression {

  public string Value ;
  public String ( string value ) {

    Value= value;
  }
  
  public override Bool_Object Validate( Context context, bool Validate_Only ) {

  if( Validate_Only ) return new Bool_Object( true, null );
  return new Bool_Object( true, Value );
  
  }

  }


  public class Let_In : Statement { 

   public List <Assignment> assignments ;
   public Expr_Or_Stat Body ; 

   public Let_In( List<Assignment> assignments, Expr_Or_Stat body ) {

   this.assignments= assignments ;
   Body= body ;

   }

   public override Bool_Object Validate( Context context, bool Validate_Only ) { 

    Context chield= context.Create_Chield();
   
    for( int i= 0; i< assignments.Count; i++)
     if( !assignments[i].Validate( chield, Validate_Only).Bool ) return new Bool_Object( false, null ) ;

    return Body.Validate( chield, Validate_Only ) ;  
    
    }
   
  }

