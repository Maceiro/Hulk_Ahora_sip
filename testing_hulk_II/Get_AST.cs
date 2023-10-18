
 public class Semantik_Analysis {

   public static Semantik_Node AST ;
   public static Context Context ;

   public static Expr_Or_Stat To_AST( Node node ) {

  if( node.Symbol== "print_stat")  return new Print( To_AST( node.Children[2] ) );
  if( node.Symbol== "if_else")  return new If_Else( ( (Boolean_Expression)To_AST( node.Children[2]) ), To_AST( node.Children[4]), To_AST( node.Children[6]) );
  if( node.Symbol== "def_func") return new Def_Func( (ID)To_AST( node.Children[1]), ((List_Node<ID>)To_AST( node.Children[3])).Descompress() , To_AST( node.Children[6]) ) ;
  if( node.Symbol== "statement" || node.Symbol=="line" ) return To_AST( node.Children[0] ) ;
  if( node.Symbol=="Number") return new Number( node.Chain );
  if( node.Symbol=="string") return new String( node.Chain );
  if( node.Symbol=="boolean") return new Boolean( node.Chain );
  if( node.Symbol=="ID") return new ID( node.Chain );
  if( node.Symbol=="let_in") return new Let_In( ((List_Node<Assignment>)To_AST( node.Children[1])).Descompress(), To_AST( node.Children[4]) );


  if( node.Symbol== "expr" || node.Symbol=="term" || node.Symbol=="factor" ) {

    if( node.Children[1].Children.Count== 1) return To_AST( node.Children[0] ) ;
    else return new Binary_Operation( To_AST( node.Children[0]), node.Children[1].Children[0].Symbol, To_AST( node.Children[1].Children[1])  ) ;
  }

  if( node.Symbol== "boolean_op" ) {

    if( node.Children[1].Children.Count== 1) return To_AST( node.Children[0] ) ;
    else return new Boolean_Expression( (Boolean_Expression)To_AST( node.Children[0]), node.Children[1].Children[0].Symbol, (Boolean_Expression)To_AST( node.Children[1].Children[1])  ) ;
  }

  if( node.Symbol=="atom" ) {

    if( node.Children.Count==1 ||  node.Children[1].Children.Count== 1) return To_AST( node.Children[0] ) ;
    else return new Func_Call( (ID)To_AST( node.Children[0]), ((List_Node<Expr_Or_Stat>)To_AST( node.Children[1].Children[1])).Descompress() )  ;
  }

  if( node.Symbol=="list_expr") {

    if( node.Children.Count==1 ) return new List_Node<Expr_Or_Stat>();
    if( node.Children[1].Children.Count== 1) return new List_Node<Expr_Or_Stat>( (Expr_Or_Stat)To_AST( node.Children[0] ) ) ;
    else {
     List_Node<Expr_Or_Stat> aux_list1= ((List_Node<Expr_Or_Stat>)To_AST( node.Children[1].Children[1])) ;
     aux_list1.Add( (Expr_Or_Stat)To_AST( node.Children[0]) ) ;
     return aux_list1 ;
    }
  }

  if( node.Symbol=="list_arg") {
    
    if( node.Children.Count==1 ) return new List_Node<ID>();
    if( node.Children[1].Children.Count== 1) return new List_Node<ID>( (ID)To_AST( node.Children[0] ) ) ;
    else {
     List_Node<ID> aux_list2= ((List_Node<ID>)To_AST( node.Children[1].Children[1])) ;
     aux_list2.Add( (ID)To_AST( node.Children[0]) ) ;
     return aux_list2 ;
    }
  }

  if( node.Symbol=="condition") {
  
   if( node.Children[1].Children.Count== 1 ) return (Expr_Or_Stat)To_AST( node.Children[0]) ;
   else return new Condition( (Expr_Or_Stat)To_AST( node.Children[0]), node.Children[1].Children[0].Symbol, (Expr_Or_Stat)To_AST( node.Children[1].Children[1] ) ) ;
    
  }


  if( node.Symbol=="mol" ) {

   if( node.Children.Count== 1) return To_AST( node.Children[0] ) ;
   else return To_AST( node.Children[1] ) ;
  }

  if( node.Symbol=="assignment" ) return new Assignment( (ID)To_AST( node.Children[0]), (Expr_Or_Stat)To_AST( node.Children[2]) ) ; 
  
   if( node.Symbol=="list_assignments") {

    if( node.Children[1].Children.Count== 1) return new List_Node<Assignment>( (Assignment)To_AST( node.Children[0] ) ) ;
    else {
    List_Node<Assignment> aux_list3= ((List_Node<Assignment>)To_AST( node.Children[1].Children[1])) ;
    aux_list3.Add( (Assignment)To_AST( node.Children[0]) ) ;
    return aux_list3 ;
    }
   }

   return null ;
  }

 }


 