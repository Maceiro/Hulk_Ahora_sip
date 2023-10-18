
public static class Data {

   public static Gramatik gramatik;
   public static Symbol EOF;
   public static Symbol Epsilon;
   public static void Create_Gramatik() {
   
   var boolean_op= new Symbol("boolean_op") ;
   var condition= new Symbol("condition" );
   var aux_boolean_op= new Symbol("aux_boolean_op") ;
   var line= new Symbol("line");
   var op= new Symbol("op" );
   var statement= new Symbol("statement" );
   var let_in= new Symbol("let_in" );
   var print_stat= new Symbol("print_stat");
   var def_func= new Symbol("def_func" );
   var if_else= new Symbol("if_else" );
   var expr= new Symbol("expr" );
   var term= new Symbol("term" );
   var factor= new Symbol("factor" );
   var mol= new Symbol("mol");
   var atom= new Symbol("atom");
   var aux_expr= new Symbol("aux_expr" );
   var aux_factor= new Symbol("aux_factor" );
   var aux_term= new Symbol("aux_term" );
   var aux_atom= new Symbol("aux_atom");
   var list_expr= new Symbol("list_expr" );
   var aux_list_expr= new Symbol("aux_list_expr");
   var list_arg= new Symbol( "list_arg");
   var aux_list_arg= new Symbol( "aux_list_arg");
   var list_assignments= new Symbol("list_assignments");
   var assignment= new Symbol("assignment");
   var aux_list_assignments= new Symbol("aux_list_assignments");
   var id= new Symbol("ID" ); 
   var number= new Symbol("Number" );
   var strings= new Symbol("string" );
   var boolean= new Symbol("boolean");
   var let= new Symbol("let" );
   var inn= new Symbol("in" );
   var print= new Symbol("print" );
   var function= new Symbol("function" ); 
   var def= new Symbol("=>" );
   var conditional_if= new Symbol("if" );
   var conditional_else= new Symbol("else" );
   var or_logic= new Symbol("|") ;
   var and_logic= new Symbol( "&") ;
   var plus= new Symbol("+" );
   var sub= new Symbol("-" );
   var mult= new Symbol("*" );
   var div= new Symbol("/" ); 
   var pow= new Symbol("^" ); 
   var eq= new Symbol("=" ); 
   var open= new Symbol("(" ); 
   var close= new Symbol(")" ); 
   var M= new Symbol(">" );
   var m= new Symbol("<" ); 
   var Meq= new Symbol(">=" );
   var meq= new Symbol("<=" );
   var eqeq= new Symbol("==" );
   var coma= new Symbol(",");
   var punto= new Symbol( ".");
   var epsilon= new Symbol("");
   var eof= new Symbol("$");
   
   Symbol[]no_terminals= { boolean_op, condition, aux_boolean_op, op, line, statement, let_in, print_stat, def_func, if_else, expr, term, factor, mol, atom, aux_expr, aux_factor, aux_term, aux_atom, list_expr, list_arg, aux_list_expr, aux_list_arg, assignment, list_assignments, aux_list_assignments };
   Symbol[]terminals= { id, number, strings, boolean, let, inn, print, function, def, conditional_if, conditional_else, or_logic, and_logic, plus, sub, mult, div, pow, eq, open, close, M, m, Meq, meq, eqeq, coma, punto, epsilon, eof } ;
   

   var p_boolean_op= new Production( boolean_op, condition, aux_boolean_op ) ;
   var p_condition = new Production( condition, line, op );
   var p_aux_boolean_op1= new Production( aux_boolean_op, or_logic, boolean_op ) ;
   var p_aux_boolean_op2= new Production( aux_boolean_op, and_logic, boolean_op ) ;
   var p_aux_boolean_op3= new Production( aux_boolean_op, epsilon ) ;
   var p_op1 = new Production( op, eqeq, condition );
   var p_op2 = new Production( op, M, line );
   var p_op3 = new Production( op, m, line );
   var p_op4 = new Production( op, Meq, line );
   var p_op5 = new Production( op, meq, line );
   var p_op6 = new Production( op, epsilon );
   var p_line1= new Production( line, statement );
   var p_line2= new Production( line, expr );
   var p_stat1 = new Production( statement, let_in );
   var p_stat2 = new Production( statement, print_stat );
   var p_stat3 = new Production( statement, def_func );
   var p_stat4 = new Production( statement, if_else );
   var p_print_stat = new Production( print_stat, print, open, boolean_op, close );
   var p_let_in = new Production( let_in, let, list_assignments, inn, open, boolean_op, close );
   var p_def_func = new Production( def_func, function, id, open, list_arg, close, def, line );
   var p_if_else = new Production( if_else, conditional_if, open, boolean_op, close, boolean_op, conditional_else, boolean_op );
   var p_expr = new Production( expr, term, aux_expr ); 
   var p_aux_expr1 = new Production( aux_expr, plus, expr );
   var p_aux_expr2 = new Production( aux_expr, sub, expr );
   var p_aux_expr3 = new Production( aux_expr, epsilon );
   var p_term= new Production( term, factor, aux_term ) ;           
   var p_aux_term1= new Production( aux_term, mult, term );
   var p_aux_term2= new Production( aux_term, div, term );
   var p_aux_term3= new Production( aux_term, epsilon);
   var p_factor = new Production( factor, mol, aux_factor );                      
   var p_aux_factor1 = new Production( aux_factor, pow, mol );
   var p_aux_factor2 = new Production( aux_factor, epsilon );
   var p_mol1 = new Production( mol, atom );
   var p_mol2 = new Production( mol, open, boolean_op, close );
   var p_atom1 = new Production( atom, id, aux_atom );
   var p_atom2 = new Production( atom, number );
   var p_atom3 = new Production( atom, strings );
   var p_atom4= new Production( atom, boolean );
   var p_aux_atom1= new Production( aux_atom, open, list_expr, close );
   var p_aux_atom2= new Production( aux_atom, epsilon );
   var p_list_expr1 = new Production( list_expr, boolean_op, aux_list_expr );
   var p_list_expr2 = new Production( list_expr, epsilon );
   var p_aux_list_expr1 = new Production( aux_list_expr, coma, list_expr );
   var p_aux_list_expr2 = new Production( aux_list_expr, epsilon );
   var p_list_arg1= new Production( list_arg, id, aux_list_arg );
   var p_list_arg2= new Production( list_arg, epsilon );
   var p_aux_list_arg1 = new Production( aux_list_arg, coma, list_arg );
   var p_aux_list_arg2 = new Production( aux_list_arg, epsilon ); 
   var p_assignment= new Production( assignment, id, eq, boolean_op );
   var p_list_assignments= new Production( list_assignments, assignment, aux_list_assignments );
   var p_aux_list_assignments1= new Production( aux_list_assignments, coma, list_assignments );
   var p_aux_list_assignments2= new Production( aux_list_assignments, epsilon );
   

   Production[]productions = { p_boolean_op, p_aux_boolean_op1, p_aux_boolean_op2, p_aux_boolean_op3, p_condition, p_op1, p_op2, p_op3, p_op4, p_op5, p_op6, p_line1, p_line2, p_stat1, p_stat2, p_stat3, p_stat4, p_let_in, p_print_stat, p_def_func, p_if_else, p_expr, p_term, p_factor, p_mol1, p_mol2, p_atom1, p_atom2, p_atom3, p_atom4, p_aux_atom1, p_aux_atom2, p_aux_expr1, p_aux_expr2, p_aux_expr3, p_aux_factor1, p_aux_factor2, p_aux_term1, p_aux_term2, p_aux_term3, p_list_expr1, p_list_expr2, p_list_arg1, p_list_arg2, p_aux_list_arg1, p_aux_list_arg2, p_aux_list_expr1, p_aux_list_expr2, p_assignment, p_list_assignments, p_aux_list_assignments1, p_aux_list_assignments2 };
   var aux_productions= new List<Production>();
   var aux_no_terminals= new List<Symbol>();
   var aux_terminals= new List<Symbol>();

   for( int i= 0; i< productions.Length; i++ ) 
     aux_productions.Add( productions[i]);
   
   for( int i= 0; i< no_terminals.Length; i++ ) 
     aux_no_terminals.Add( no_terminals[i]);
   
   for( int i= 0; i< terminals.Length; i++ ) 
     aux_terminals.Add( terminals[i]);
   
   EOF = eof ;
   Epsilon= epsilon;
   gramatik= new Gramatik { Productions= aux_productions, No_Terminals= aux_no_terminals, Terminals= aux_terminals, Initial= line };

   }

  }

