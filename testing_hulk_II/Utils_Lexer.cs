
 public class Token {

   public string Class { get; set; }
   public string Chain { get; set; }
   public Token( string status, string chain ) {
    Class= status;
    Chain= chain;
   }

  }

  public static class CharExtensions {

    public static bool IsDigit( this char s ) {
      return ( s=='0' || s=='1' || s=='2' || s=='3' || s=='4' || s=='5' || s=='6' || s=='7' || s=='8' || s=='9' ) ;
      
    }

   public static bool IsOpComp( this char s ) {

    return ( s=='<' || s=='>' || s=='=' ) ;
   }

   public static bool IsOpAritm( this char s ) {

    return ( s=='+' || s=='-' || s=='*' || s=='/' || s=='^') ;
   }

   public static bool IsSintax( this char s ) {

    return ( s=='(' || s==')' || s==';' || s=='.' || s==',' ); 
   }

   public static bool IsLetter( this char s ) {

    return char.IsLetter(s);
   }

   public static bool IsOpBool( this char s ) {

    return s=='|' || s=='&' ;
   }

  }