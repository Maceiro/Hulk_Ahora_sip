public class Lexer {
  
  public static List<Token> Tokenization( string chain ) {
   
   var tokens= new List<Token>();
   var s= new Automata(); 

   for( int i= 0; i< chain.Length; i++ ) {
    
    s.Value= chain[i];
    if( s.Value==' ' &&  s.Inicial ) continue;
   
    if( s.Inicial) s.Begin( tokens );
    else s.Intermedio();
    
    if( s.Final) {
      //Console.WriteLine( s.Status );
     // Console.WriteLine( s.Chain );
      tokens.Add( new Token( s.Status, s.Chain ));
      s.Final= false;
      s.Chain= "";
      
      if( !( s.Value==' ' || (s.Value=='=' && s.IsOp() ) || ( s.Value=='"' && s.Status=="string" ) ) ) s.Begin( tokens ) ;
    }

   }

     Verification( tokens ) ; 
     tokens.Add( new Token( "$", "$") );
    
     //for( int i= 0; i< tokens.Count; i++) 
     //Console.WriteLine( "{0}  :   {1}", tokens[i].Class, tokens[i].Chain ) ;

     return tokens;

   }

   
   public static void Verification( List<Token> tokens ) {
     
    for( int i=0; i< tokens.Count; i++ ) {

     if( tokens[i].Class== "ID" || tokens[i].Class=="Op" ) 
      switch( tokens[i].Chain ) {
        case "print": case "let": case "in": case "if": case "else": case "function": case "<": case ">": case "<=": case ">=": case "==": case "+": case "-": case "*": case "/" : case "^" : case "=": case "|" : case "&" :
        tokens[i].Class= tokens[i].Chain ;
        break;
        default:
        break;
      }

      if( tokens[i].Class== "ID" && ( tokens[i].Chain=="true" || tokens[i].Chain=="false" ) )  { tokens[i].Class= "boolean" ;  }

      if( tokens[i].Class=="string") { tokens[i].Chain= tokens[i].Chain.Substring(1) ;  }

      if( i>0 && tokens[i].Class==">" && tokens[i-1].Class=="=" ) {
        tokens[i-1].Class= "=>";
        tokens[i-1].Chain= "=>";
        tokens.RemoveAt(i);
      }

       if( tokens[i].Class== "ID" || tokens[i].Class=="Op" ) 
      switch( tokens[i].Chain ) {
        case "print": case "let": case "in": case "if": case "else": case "function": case "<": case ">": case "<=": case ">=": case "==": case "+": case "-": case "*": case "/" : case "^" : case "=":
        tokens[i].Class= tokens[i].Chain ;
        break;
        default:
        break;
      }
      
     }

    }


  }
