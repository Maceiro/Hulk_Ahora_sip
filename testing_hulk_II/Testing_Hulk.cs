
 public class Program {

  public static void Main() {

  Operation_System.Interface() ;
  
  }

  public static void Aux() {

  foreach( var pair in Parser.Follow.dicc )  {
   Console.WriteLine( "Key" + "  " + pair.Key );
   foreach( var s in pair.Value )
    if( s.IsEpsilon ) Console.WriteLine( "epsilon" + "  ");
    else Console.Write( s.Class + "  " );

    Console.WriteLine( "\n");
  }

  }


   public static void Aux_Table() {

     foreach( var n in Data.gramatik.No_Terminals ) 
      foreach( var t in Data.gramatik.Terminals ) {
       Console.Write( "Si tenemos "+ n.Class + " y "+ t.Class + " entonces aplicamos :    ");
       var p= Parser.table.Search( n, t);
       if( p== null ) Console.Write( "null");
       else {
       Console.Write( p.Left.Class + " => ");
       foreach( var s in p.Right )
       Console.Write( s.Class + "");      
       }

       Console.Write( "\n");

      }
   }


   public static void Print( Node node, int height ) {

    Console.WriteLine( node.Symbol + "   " + height );
    foreach( var tree in node.Children )
    Print( tree, height+1 );

   }

  

 }


 //instructions ..
 // let x= 2<3 & ( let x1=4 in (x1+2)< 5 | 3<5) in (x==true) ;
 // let x=1, y=2 in ( if((Sum(x,y)>6 & 5>x) | "lala"=="land" ) (let x1=3 in (x1/x)) + (print(2))*2 else true ) ;   
 // ( let x="lala" in ( x+"land")) / (print("_movie")) ;
 // if( (Fib(3)<Fib(4) & Fib(5)>Fib(6) ) | "movie"=="movi" ) 5 else 2<3 & 4>5 ;